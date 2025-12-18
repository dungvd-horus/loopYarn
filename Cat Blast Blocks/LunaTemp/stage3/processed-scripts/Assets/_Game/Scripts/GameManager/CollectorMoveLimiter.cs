using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class CollectorMoveLimiter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PadController padController;
    [SerializeField] private int maxActiveMoving = 5;

    [Header("References")]
    [SerializeField] private Transform defaultPosition;
    [SerializeField] private Transform poolParent;
    [SerializeField] private TMP_Text limiterText;

    [Header("Layout Config")]
    [SerializeField] private float padSpacing = 0.5f;

    // Pool quản lý các Pad (Index 0 là vị trí ngoài cùng bên phải)
    private List<PadController> padPool = new List<PadController>();

    // Dictionary để quản lý chính xác ai đang giữ Pad nào
    private Dictionary<CollectorController, PadController> collectorToPad = new Dictionary<CollectorController, PadController>();

    // Variables cho UI Effect
    private Vector2 originalPos;
    private Tween textScaleTween;
    private Sequence seqShake;

    private void Awake()
    {
        if (limiterText != null)
        {
            originalPos = limiterText.rectTransform.anchoredPosition;
        }
        EnsurePoolSize();
        UpdateText();
    }

    // =================================================================================
    // CORE LOGIC (Đã sửa đổi)
    // =================================================================================

    /// <summary>
    /// Thử lấy Pad để di chuyển.
    /// Trả về TRUE: Thành công (được đi).
    /// Trả về FALSE: Thất bại (hết chỗ, phải đứng im).
    /// </summary>
    public bool TryStartMove(CollectorController main, CollectorController partner = null)
    {
        // Lấy toàn bộ các con mèo trong chuỗi kết nối để tính toán pad cần thiết
        List<CollectorController> allCollectorsInChain = GetAllCollectorsInChain(main, partner);
        
        // 1. Xác định trạng thái hiện tại và tính số lượng pad cần thiết
        int needed = 0;
        foreach (var collector in allCollectorsInChain)
        {
            if (!collectorToPad.ContainsKey(collector)) needed++;
        }

        // 2. Kiểm tra xem có đủ slot trống không
        int available = GetAvailableSlots();
        if (available < needed)
        {
            // Không đủ chỗ cho cả team -> Rung UI báo lỗi -> Trả về False
            TextAnimShakeOnFull();
            return false;
        }

        // 3. Cấp phát Pad (Transaction Phase)
        // Chúng ta cần lấy Pad ra, nếu lấy lỗi thì phải trả lại (Rollback)
        List<PadController> allocatedPads = new List<PadController>();
        List<CollectorController> collectorsToAllocate = new List<CollectorController>();

        // Xác định những con mèo cần cấp phát pad mới
        foreach (var collector in allCollectorsInChain)
        {
            if (!collectorToPad.ContainsKey(collector))
            {
                collectorsToAllocate.Add(collector);
            }
        }

        // Lấy pad cho từng con mèo cần cấp phát
        foreach (var collectorItem in collectorsToAllocate)
        {
            PadController padItem = GetAndRemoveFreePad();
            if (padItem == null)
            {
                // Thử fix pool nếu lỗi null
                EnsurePoolSize();
                padItem = GetAndRemoveFreePad();
                if (padItem == null)
                {
                    // Rollback: Trả lại tất cả pad đã lấy
                    foreach (var allocatedPad in allocatedPads)
                    {
                        ReturnPadToPool(allocatedPad);
                    }
                    return false; // Vẫn lỗi -> Thua
                }
            }
            allocatedPads.Add(padItem);
        }

        // 4. Cam kết (Commit Phase) - Chỉ chạy khi đã cầm chắc pad trong tay
        for (int i = 0; i < collectorsToAllocate.Count; i++)
        {
            var collector = collectorsToAllocate[i];
            var pad = allocatedPads[i];

            // --- FIX LỖI: BẬT OBJECT LÊN TRƯỚC KHI CHẠY COROUTINE ---
            pad.gameObject.SetActive(true);
            // ---------------------------------------------------------

            collectorToPad.Add(collector, pad);
            pad.OnCollectorEnterPad(collector);
        }

        // Cập nhật Visual
        UpdateText();
        UpdatePoolLayout();

        return true;
    }

    /// <summary>
    /// Lấy toàn bộ các con mèo trong chuỗi kết nối, bao gồm cả main và partner
    /// </summary>
    private List<CollectorController> GetAllCollectorsInChain(CollectorController main, CollectorController partner = null)
    {
        HashSet<CollectorController> allCollectors = new HashSet<CollectorController>();
        Queue<CollectorController> queue = new Queue<CollectorController>();

        // Bắt đầu với main và partner (nếu có)
        if (main != null)
        {
            allCollectors.Add(main);
            queue.Enqueue(main);
        }

        if (partner != null && !allCollectors.Contains(partner))
        {
            allCollectors.Add(partner);
            queue.Enqueue(partner);
        }

        // Duyệt qua toàn bộ chuỗi kết nối
        while (queue.Count > 0)
        {
            CollectorController current = queue.Dequeue();

            // Thêm các con mèo được kết nối với current vào danh sách
            if (current.collectorConnect != null)
            {
                foreach (var connectedCollector in current.collectorConnect)
                {
                    if (connectedCollector != null && !allCollectors.Contains(connectedCollector))
                    {
                        allCollectors.Add(connectedCollector);
                        queue.Enqueue(connectedCollector);
                    }
                }
            }
        }

        return new List<CollectorController>(allCollectors);
    }

    public void OnCollectorMoveFinished(CollectorController collector)
    {
        if (collectorToPad.TryGetValue(collector, out PadController returnedPad))
        {
            // Xóa khỏi danh sách quản lý
            collectorToPad.Remove(collector);

            // Trả Pad về hồ chứa
            ReturnPadToPool(returnedPad);

            // Cập nhật Visual
            UpdateText();
            UpdatePoolLayout();
        }
    }

    // =================================================================================
    // POOL & LAYOUT MANAGEMENT
    // =================================================================================

    private PadController GetAndRemoveFreePad()
    {
        PadController freePad = null;
        int index = -1;

        // Tìm Pad đầu tiên khả dụng trong Pool
        for (int i = 0; i < padPool.Count; i++)
        {
            if (padPool[i] != null)
            {
                freePad = padPool[i];
                index = i;
                padPool[i] = null; // Đánh dấu slot này trống
                break;
            }
        }

        if (freePad != null)
        {
            // Dồn hàng (Shift Left): Lấp đầy chỗ trống vừa tạo ra
            for (int i = index + 1; i < padPool.Count; i++)
            {
                padPool[i - 1] = padPool[i];
                padPool[i] = null;
            }
        }

        return freePad;
    }

    private void ReturnPadToPool(PadController pad)
    {
        Transform targetParent = poolParent != null ? poolParent : transform;
        pad.ResetToDefault(defaultPosition != null ? defaultPosition.position : Vector3.zero, targetParent);

        // Tìm slot trống đầu tiên để nhét Pad vào
        int insertIdx = -1;
        for (int i = 0; i < padPool.Count; i++)
        {
            if (padPool[i] == null)
            {
                insertIdx = i;
                break;
            }
        }

        if (insertIdx != -1)
        {
            padPool[insertIdx] = pad;
        }
        else
        {
            // Fallback: Nhét vào đầu nếu không tìm thấy slot (hiếm)
            padPool[0] = pad;
        }
    }

    private void EnsurePoolSize()
    {
        if (padController == null) return;

        // Đảm bảo list size = maxActiveMoving
        while (padPool.Count < maxActiveMoving) padPool.Add(null);
        while (padPool.Count > maxActiveMoving)
        {
            int last = padPool.Count - 1;
            if (padPool[last] != null) Destroy(padPool[last].gameObject);
            padPool.RemoveAt(last);
        }

        // Tính toán số lượng Pad cần spawn thêm
        int currentlyBusy = collectorToPad.Count;
        int currentlyFree = 0;
        foreach (var p in padPool) if (p != null) currentlyFree++;

        int needed = maxActiveMoving - (currentlyBusy + currentlyFree);

        if (needed > 0)
        {
            for (int k = 0; k < needed; k++)
            {
                // Tìm slot null
                int slot = -1;
                for (int i = 0; i < padPool.Count; i++)
                {
                    if (padPool[i] == null) { slot = i; break; }
                }

                if (slot != -1)
                {
                    var newPad = Instantiate(padController, poolParent != null ? poolParent : transform);
                    newPad.gameObject.name = $"{padController.name}_Pooled_{slot}";
                    if (defaultPosition != null) newPad.transform.position = defaultPosition.position;
                    newPad.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    padPool[slot] = newPad;
                }
            }
        }
        UpdatePoolLayout();
    }

    private void UpdatePoolLayout()
    {
        Transform parent = poolParent != null ? poolParent : transform;
        Vector3 basePos = defaultPosition != null ? defaultPosition.position : parent.position;
        Quaternion rot = Quaternion.Euler(0f, 0f, 90f);

        for (int i = 0; i < padPool.Count; i++)
        {
            var p = padPool[i];
            if (p == null) continue;

            // Bỏ qua nếu đang bị disable
            if (p.transform.parent != null && !p.transform.parent.gameObject.activeInHierarchy) continue;

            p.transform.SetParent(parent, worldPositionStays: true);

            // Tính vị trí dựa trên index (Index 0 ở phải, tăng dần sang trái)
            Vector3 targetPos = basePos + Vector3.left * (i * padSpacing);

            p.transform.rotation = rot;
            p.PushForwardPad(targetPos);
        }
    }

    // =================================================================================
    // GETTERS & SETTERS
    // =================================================================================

    public int GetAvailableSlots()
    {
        return Mathf.Max(0, maxActiveMoving - collectorToPad.Count);
    }

    public void SetMaxActiveMoving(int value)
    {
        maxActiveMoving = value;
        EnsurePoolSize();
        UpdateText();
    }

    public void AddMaxActiveMoving(int increment = 1)
    {
        maxActiveMoving += increment;
        EnsurePoolSize();
        UpdateText();
    }

    public bool IsFullSlots()
    {
        return collectorToPad.Count >= maxActiveMoving;
    }

    // =================================================================================
    // UI EFFECTS
    // =================================================================================

    private void UpdateText()
    {
        if (limiterText != null)
        {
            limiterText.text = $"{GetAvailableSlots()}/{maxActiveMoving}";
            TextAnimScale();
        }
    }

    private void TextAnimScale()
    {
        if (textScaleTween != null && textScaleTween.IsActive()) textScaleTween.Kill();
        limiterText.rectTransform.localScale = Vector3.one;

        textScaleTween = limiterText.rectTransform.DOScale(Vector3.one * 1.1f, 0.1f)
            .SetEase(Ease.OutSine)
            .OnComplete(() => { limiterText.rectTransform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InSine); });
    }

    public void TextAnimShakeOnFull()
    {
        if (limiterText == null) return;

        limiterText.color = Color.red;

        // Reset về vị trí gốc trước khi shake
        RectTransform target = limiterText.rectTransform;
        if (seqShake != null && seqShake.IsActive())
        {
            seqShake.Kill();
            target.anchoredPosition = originalPos;
            limiterText.color = Color.white;
        }

        // Logic shake nhẹ nhàng (Gốc)
        float strength = 0.15f;
        float duration = 0.1f;

        seqShake = DOTween.Sequence();

        // Rung sang phải
        seqShake.Append(target.DOAnchorPosX(originalPos.x + strength, duration * 0.5f).SetEase(Ease.OutSine));
        // Rung sang trái
        seqShake.Append(target.DOAnchorPosX(originalPos.x - strength, duration).SetEase(Ease.InOutSine));
        // Về giữa
        seqShake.Append(target.DOAnchorPosX(originalPos.x, duration * 0.5f).SetEase(Ease.InSine));

        float strength2 = strength / 2f;
        // Rung nhẹ thêm nhịp nữa
        seqShake.Append(target.DOAnchorPosX(originalPos.x + strength2, duration * 0.5f).SetEase(Ease.OutSine));
        seqShake.Append(target.DOAnchorPosX(originalPos.x - strength2, duration).SetEase(Ease.InOutSine));
        seqShake.Append(target.DOAnchorPosX(originalPos.x, duration * 0.5f).SetEase(Ease.InSine));

        // Đổi màu về trắng
        seqShake.Join(limiterText.DOColor(Color.white, duration * 0.5f));
    }
    // Thêm hàm này vào cuối file hoặc vùng Getters/Setters
    public int GetMaxActiveMoving()
    {
        return maxActiveMoving;
    }
}
