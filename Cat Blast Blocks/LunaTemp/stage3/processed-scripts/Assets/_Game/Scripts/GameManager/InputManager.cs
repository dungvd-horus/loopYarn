using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float clickCooldown; // Thời gian cooldown giữa các lần click (giây)
    private float lastClickTime = -10f;
    [SerializeField] private Camera gamePlayCamera;

    [Header("INPUT CONDITION(s)")]
    public bool BlockInput = false;
    public bool IsChoosingBlock = false;
    public bool IsFreePicking = false;

    [Header("LAYER SETUP")]
    public LayerMask CubeLayermask;

    // Cache các biến để tối ưu hiệu năng
    private Ray cachedRay;
    private Vector3 cachedMousePosition;
    private int collectorLayerMask;
    private RaycastHit hitInfo;

    private void Awake()
    {
        // Cache layer mask để tránh tính toán mỗi frame
        collectorLayerMask = 1 << 12;
        UnBlockGameplayInput();
        GameplayEventsManager.OnBlockGamePlayInput += (block) => BlockInput = block;
    }
    void OnDestroy()
    {
        GameplayEventsManager.OnBlockGamePlayInput -= (block) => BlockInput = block;
    }
    void Update()
    {
        if (IsChoosingBlock) HandleSuperRabbitBoosterInput();

        if (!BlockInput) HandleCollectorChoosenInput();

        if (IsFreePicking) HandleBoosterCollectorChoosenInput();
    }

    // Tối ưu hóa: Trung tâm hóa logic cooldown để tránh lặp lại
    private bool CanProcessInput()
    {
        if (BlockInput) return false;
        if (Time.time - lastClickTime < clickCooldown) return false;

        lastClickTime = Time.time;
        return true;
    }

    // Tối ưu hóa: Cache raycast để tái sử dụng
    private RaycastHit? PerformRaycast([Bridge.Ref] Vector3 screenPoint, [Bridge.Ref] LayerMask mask)
    {
        cachedRay = gamePlayCamera.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(cachedRay, out hitInfo, 100f, mask))
        {
            return hitInfo;
        }
        return null;
    }

    public void HandleCollectorChoosenInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CanProcessInput()) return;

            Vector3 inputPos = Input.mousePosition;
            var hit = PerformRaycast(inputPos, collectorLayerMask);

            if (hit.HasValue)
            {
                var collector = hit.Value.collider.GetComponent<CollectorController>();
                if (collector != null)
                {
                    CollectorGameManager.Instance.RequestMoveCollector(collector);
                }
            }
        }
    }

    public void HandleBoosterCollectorChoosenInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CanProcessInput()) return;

            Vector3 inputPos = Input.mousePosition;
            var hit = PerformRaycast(inputPos, collectorLayerMask);

            if (hit.HasValue)
            {
                var collector = hit.Value.collider.GetComponent<CollectorController>();
                if (collector != null)
                {
                    CollectorGameManager.Instance.ForceMoveCollector(collector);
                    GameplayEventsManager.OnFreePickACollector?.Invoke(collector);
                    return;
                }
            }
        }
    }

    public void HandleSuperRabbitBoosterInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 inputPos = Input.mousePosition;
            var hit = PerformRaycast(inputPos, CubeLayermask);

            if (hit.HasValue)
            {
                var paintingPixel = hit.Value.collider.GetComponent<PaintingPixelComponent>();
                if (paintingPixel != null)
                {
                    GameplayEventsManager.OnSuperRabbitBlockSelected?.Invoke(paintingPixel.PixelData.colorCode);
                    return;
                }

                var wallObject = hit.Value.collider.transform.parent.GetComponent<WallObject>();
                if (wallObject != null)
                {
                    GameplayEventsManager.OnSuperRabbitBlockSelected?.Invoke(wallObject.ColorCode);
                    return;
                }
            }
        }
    }

    public void UpdateLastClickTime()
    {
        lastClickTime = Time.time;
    }

    #region SUPPORTIVE
    public void BlockGameplayInput() => BlockInput = true;
    public void UnBlockGameplayInput() => BlockInput = false;

    public void StartAllowPlayerToChooseBlock() => IsChoosingBlock = true;
    public void StopAllowPlayerToChooseBlock() => IsChoosingBlock = false;

    public void StartAllowPlayerToFreelyPick() => IsFreePicking = true;
    public void StopAllowPlayerToFreePick() => IsFreePicking = false;
    #endregion
}
