using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;


public class PadController : MonoBehaviour
{
    public Transform meshTrans;
    public Transform startTrans;

    [Header("EFFECT(s)")]
    public ParticleSystem PadSmokeFX;

    private Tween padRotateTween;
    private Tween padMoveTween; // Lưu tween di chuyển để kill
    private CollectorController _pendingCollector;

    // Biến cờ quan trọng: Đánh dấu Pad này ĐANG BẬN, cấm Pool đụng vào
    public bool HasCollectorOnPad = false;

    private void Start()
    {
        if (PathTransformBasedCached.Instance != null && PathTransformBasedCached.Instance.PathPoints.Count > 0)
        {
            startTrans = PathTransformBasedCached.Instance.PathPoints[0];
        }
    }

    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        // Kill toàn bộ khi tắt để tránh ghost logic
        CancelInvoke(nameof(SetPositionAfterDelay));
        transform.DOKill();
    }

    public void OnCollectorEnterPad(CollectorController collector)
    {
        // 1. ĐÁNH DẤU BẬN NGAY LẬP TỨC
        // Để hàm PushForwardPad không thể tác động vào nữa
        HasCollectorOnPad = true;

        // 2. NGẮT MỌI CHUYỂN ĐỘNG CŨ (Rất quan trọng khi click nhanh)
        // Nếu nó đang trượt trong pool, bắt nó dừng ngay
        transform.DOKill();
        if (padMoveTween != null) padMoveTween.Kill();

        // 3. Đảm bảo bật
        if (!gameObject.activeSelf) gameObject.SetActive(true);

        // 4. Set vị trí cứng (Snap)
        // Không dùng Tween ở đây, snap ngay lập tức để tránh bị trôi
        if (startTrans == null)
            transform.position = collector.GetPositionByTF(0);
        else
            transform.position = startTrans.position;

        meshTrans.localScale = Vector3.one * 2.5f;

        // 5. Logic hẹn giờ gán cha
        _pendingCollector = collector;
        CancelInvoke(nameof(SetPositionAfterDelay));
        Invoke(nameof(SetPositionAfterDelay), 0.4f);

        // 6. Anim xoay (Visual)
        if (padRotateTween != null) padRotateTween.Kill();
        padRotateTween = transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, 0f), 0.2f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => { PadSmokeFX?.Play(); });
    }

    private void SetPositionAfterDelay()
    {
        // Check lại lần nữa xem có bị Reset trong lúc chờ không
        if (!HasCollectorOnPad || _pendingCollector == null || this == null) return;

        if (gameObject.activeInHierarchy)
        {
            transform.SetParent(_pendingCollector.transform);
            transform.localPosition = Vector3.zero;
        }
    }

    public void ResetToDefault([Bridge.Ref] Vector3 position, Transform parent = null)
    {
        // --- FIX LỖI TẮT GAME ---
        // Nếu Scene đang được unload (Tắt game), thì không làm gì cả.
        // Tránh lỗi cố gắng SetParent khi object cha đang bị hủy.
        if (!this.gameObject.scene.isLoaded) return;
        // ------------------------

        // 1. HỦY LOGIC CŨ
        CancelInvoke(nameof(SetPositionAfterDelay));
        _pendingCollector = null;

        // 2. MỞ KHÓA
        HasCollectorOnPad = false;

        // 3. KILL TWEEN
        transform.DOKill();
        if (padMoveTween != null) padMoveTween.Kill();
        if (padRotateTween != null) padRotateTween.Kill();

        // 4. THIẾT LẬP LẠI CẤU TRÚC
        // Kiểm tra nếu đang trong quá trình disable để tránh lỗi SetParent
        if (parent != null)
        {
            // Check if we're in the middle of activation/deactivation process
            // In which case Unity doesn't allow changing parents
            if (this.gameObject.activeInHierarchy)
            {
                try
                {
                    transform.SetParent(parent);
                }
                catch (System.Exception)
                {
                    // If we still get an error, defer the operation to next frame
                    StartCoroutine(DeferredSetParent(parent));
                }
            }
            else
            {
                // If this object is being deactivated, defer the parent change
                // But only start coroutine if the GameObject is active
                if (this.gameObject.activeInHierarchy)
                {
                    StartCoroutine(DeferredSetParent(parent));
                }
                else
                {
                    // If the GameObject is inactive, set the parent directly without coroutine
                    // This prevents the "Coroutine couldn't be started because the game object is inactive" error
                    try
                    {
                        transform.SetParent(parent);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogWarning($"Failed to set parent on inactive object: {ex.Message}");
                        // Fallback: set to root if we still can't set the parent
                        transform.SetParent(null);
                    }
                }
            }
        }

        transform.position = position;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        meshTrans.localScale = Vector3.one * 1.5f;
    }

    private System.Collections.IEnumerator DeferredSetParent(Transform parent)
    {
        // Wait one frame to ensure the activation/deactivation process is complete
        yield return null;
        try
        {
            transform.SetParent(parent);
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning($"Failed to set parent even after deferring: {ex.Message}");
            // Fallback: set to root if we still can't set the parent
            transform.SetParent(null);
        }
    }

    public void PushForwardPad([Bridge.Ref] Vector3 targetPos)
    {
        // CHỐT CHẶN: Nếu Pad này đang bận phục vụ Collector,
        // Tuyệt đối không cho phép PoolManager di chuyển nó nữa.
        if (HasCollectorOnPad) return;

        // Kill tween cũ để không bị conflict hướng di chuyển
        if (padMoveTween != null) padMoveTween.Kill();

        // Di chuyển mượt trong hồ chứa
        padMoveTween = transform.DOMove(targetPos, 0.15f).SetEase(Ease.OutCubic);
    }
}
