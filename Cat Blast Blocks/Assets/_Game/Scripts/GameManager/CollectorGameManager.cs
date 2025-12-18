using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
public class CollectorGameManager : MonoBehaviour
{
    public static CollectorGameManager Instance { get; private set; }

    [Header("Controllers")]
    public CollectorQueueManager queueManager;
    public CollectorMoveLimiter moveLimiter;
    public InputManager inputManager;
    public GamePlaySound gamePlaySound;
    public GameplayManager gameplayManager;

    [Header("Settings")]
    public float distanceTf = 0.06f;
    public float pendingStartInterval = 0.2f;

    // Sử dụng HashSet cho kiểm tra.Contains() nhanh hơn
    private readonly HashSet<CollectorController> activeMovingControllers = new HashSet<CollectorController>();
    public HashSet<CollectorController> ActiveMovingControllers => activeMovingControllers;

    private readonly Queue<CollectorController> pendingStartQueue = new Queue<CollectorController>();
    private bool isProcessingPending = false;

    public List<CollectorController> collectorOnDead;

    private bool hasShowPrelose = false;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GameplayEventsManager.OnACollectorFinishMove += OnCollectorFinished;
        GameplayEventsManager.CompleteColor += OnCompleteColor;
        GameplayEventsManager.ForceRemoveCollectorActive += ForceRemoveCollector;
        GameplayEventsManager.PauseGame += PauseGame;
    }

    private void OnDestroy()
    {
        GameplayEventsManager.OnACollectorFinishMove -= OnCollectorFinished;
        GameplayEventsManager.ForceRemoveCollectorActive -= ForceRemoveCollector;
        GameplayEventsManager.CompleteColor -= OnCompleteColor;
        GameplayEventsManager.PauseGame -= PauseGame;
    }

    #region === MOVE COLLECTOR ===
    [Header("Count click to move Store")]
    [SerializeField] private int countClickToMoveStore = 5;
    [SerializeField] private bool canMoveStoreEqualCount = true;
    public void RequestMoveCollector(CollectorController controller)
    {
        // 1. Check đường đi
        if (!CanMoveCollectorPath(controller))
        {
            GameplayEventsManager.OnClickACollector?.Invoke(controller, false);
            if (moveLimiter.IsFullSlots()) moveLimiter.TextAnimShakeOnFull();
            return;
        }

        // 2. Tìm Partner
        CollectorController partner = null;
        if (controller.collectorConnect != null && controller.collectorConnect.Count > 1)
        {
            partner = controller.collectorConnect.FirstOrDefault(c => c != controller);
        }

        // 3. Xin phép Limiter
        bool moveGranted = moveLimiter.TryStartMove(controller, partner);

        if (moveGranted)
        {
            // --- XỬ LÝ LOGIC TRƯỚC KHI DI CHUYỂN ---

            bool moveGrante = moveLimiter.TryStartMove(controller, partner);

            if (moveGranted)
            {
                // --- CLICK TO STORE (LUNA) ---
                if (canMoveStoreEqualCount)
                {
                    bool blockedByFirstLevelLimit = false;

                    // Nếu GameplayManager bật limit & đang ở level đầu (mà không phải level cuối) -> KHÔNG đếm
                    if (gameplayManager != null &&
                        gameplayManager.IsStoreLimitForFirstLevelEnabled &&
                        gameplayManager.IsFirstLevelAndNotLast())
                    {
                        blockedByFirstLevelLimit = true;
                    }

                    if (!blockedByFirstLevelLimit)
                    {
                        countClickToMoveStore--;

                        // countClickToMoveStore = số lần click THỰC SỰ (5 nghĩa là 5 lần)
                        if (countClickToMoveStore <= 0)
                        {
                            canMoveStoreEqualCount = false;

                            // Gọi Luna trực tiếp, không qua Tutorial
                            Luna.Unity.LifeCycle.GameEnded();
                            Luna.Unity.Playable.InstallFullGame();
                        }
                    }
                }

                // --- BƯỚC QUAN TRỌNG: FIX LỖI NHẢY ---

                // Tạo danh sách để xử lý xóa khỏi hàng đợi
                var groupToMove = controller.collectorConnect;
                var listToRemove = new List<CollectorController>(groupToMove);

                // SẮP XẾP NGƯỢC: Xóa con đứng vị trí index cao (đứng sau) trước
                // Để tránh việc con đứng trước rời đi làm con đứng sau bị trượt lên
                if (queueManager != null && queueManager.queueArray != null)
                {
                    listToRemove.Sort((a, b) =>
                    {
                        int indexA = System.Array.IndexOf(queueManager.queueArray, a);
                        int indexB = System.Array.IndexOf(queueManager.queueArray, b);
                        return indexB.CompareTo(indexA); // Giảm dần (Index lớn xếp trước)
                    });
                }

                // Xóa khỏi dữ liệu hàng đợi & NGẮT CHUYỂN ĐỘNG CŨ
                foreach (var c in listToRemove)
                {
                    // Kill ngay lập tức mọi tween đang chạy (ví dụ: đang trượt dồn hàng)
                    // Đảm bảo nó đứng im để chuẩn bị Nhảy
                    c.transform.DOKill();

                    // Xóa khỏi logic quản lý của Queue (để Queue không điều khiển nó nữa)
                    HandleStartMoveCollector(c);
                }

                // --- BẮT ĐẦU DI CHUYỂN ---

                // Lúc này cả 2 con đã tự do, gọi lệnh Nhảy
                StartCoroutine(MoveCollectorCoroutine(groupToMove));

                // Kích hoạt hiệu ứng/Event click cho cả nhóm
                foreach (var c in groupToMove)
                {
                    GameplayEventsManager.OnClickACollector?.Invoke(c, true);
                }

                gamePlaySound?.PlayClickCat();
            }
            else
            {
                GameplayEventsManager.OnClickACollector?.Invoke(controller, false);
            }
        }
    }


    public void ForceMoveCollector(CollectorController controller)
    {
        if (controller.ColorCollector.IsHidden)
            controller.ColorCollector.Reveal();

        // Tìm partner để update visual limiter (dù force move thì vẫn nên báo cho limiter biết)
        CollectorController partner = controller.collectorConnect.FirstOrDefault(c => c != controller);
        moveLimiter.TryStartMove(controller, partner);

        StartCoroutine(MoveCollectorCoroutine(controller.collectorConnect));

        // FIX LỖI TƯƠNG TỰ CHO FORCE MOVE
        foreach (var connectCollector in controller.collectorConnect)
        {
            HandleStartMoveCollector(connectCollector);

            // Gọi event cho tất cả
            GameplayEventsManager.OnClickACollector?.Invoke(connectCollector, true);
        }
    }

    private IEnumerator MoveCollectorCoroutine(List<CollectorController> controllers)
    {
        if (controllers == null || controllers.Count == 0) yield break;

        foreach (var controller in controllers)
        {
            pendingStartQueue.Enqueue(controller);
            GameplayEventsManager.OnCollectorMoveToConveyor?.Invoke(controller);

            // Safety: Kill lần nữa để chắc chắn không còn quán tính cũ
            controller.transform.DOKill();

            // Gọi lệnh nhảy theo đường cong (Jump)
            controller.MoveToPos(controller.GetPositionByTF(0f), CollectorAnimState.MoveToConveyorBelt);

            GameplayEventsManager.OnCollectorStartMove?.Invoke(controller);

            if (!activeMovingControllers.Contains(controller))
                activeMovingControllers.Add(controller);
        }

        if (!isProcessingPending)
            StartCoroutine(ProcessPendingQueueCoroutine());
    }

    private IEnumerator ProcessPendingQueueCoroutine()
    {
        isProcessingPending = true;

        //float timer = 0f;
        while (pendingStartQueue.Count > 0)
        {
            // Wait between each collector start
            yield return HelperCoroutine.WaitSeconds(pendingStartInterval);

            if (pendingStartQueue.Count == 0) break;

            var controller = pendingStartQueue.Dequeue();
            if (controller == null) continue;

            controller.StartMovement(0f);
        }

        isProcessingPending = false;
    }

    #endregion
    private void ForceRemoveCollector(CollectorController collector)
    {
        if (activeMovingControllers.Contains(collector))
        {
            collector.IsCompleteColor = true;
            activeMovingControllers.Remove(collector);
            moveLimiter.OnCollectorMoveFinished(collector);
        }
    }
    #region === STATE MANAGEMENT ===

    private void HandleStartMoveCollector(CollectorController controller)
    {
        moveLimiter.TryStartMove(controller);

        if (controller.State == CollectorState.InQueue)
            queueManager.RemoveCollectorFromQueue(controller);

        if (collectorOnDead != null && collectorOnDead.Contains(controller))
            collectorOnDead.Remove(controller);
    }

    // Tiền cấp phát danh sách tạm thời để tránh allocation trong các hàm thường gọi
    private static readonly List<CollectorController> tempControllers = new List<CollectorController>();


    private bool CanMoveCollectorPath(CollectorController controller)
    {
        if (controller == null) return false;

        var group = controller.collectorConnect;
        // Nếu không có nhóm hoặc ở trong hàng đợi thì check đơn giản
        if (group == null || group.Count == 0)
        {
            if (controller.State != CollectorState.InQueue && !controller.CanMove()) return false;
            return controller.ColorCollector != null && controller.ColorCollector.BulletLeft > 0;
        }

        // 1. Check Đạn: Chỉ cần 1 con trong cả nhóm còn đạn là OK
        bool groupHasAmmo = false;
        foreach (var c in group)
        {
            if (c != null && c.ColorCollector != null && c.ColorCollector.BulletLeft > 0)
            {
                groupHasAmmo = true;
                break; // Tìm thấy 1 viên là đủ điều kiện đạn
            }
        }

        if (!groupHasAmmo) return false; // Hết đạn cả lũ -> Nghỉ

        // 2. Check Đường đi (Vật lý): Xử lý lỗi cùng cột
        // Logic: Nhóm các con theo Cột. Trong mỗi cột, chỉ cần 1 con đi được là cả cột đó đi được (do con đi được sẽ kéo con bị chặn đi theo).

        // Dùng HashSet để lấy danh sách các cột duy nhất mà nhóm này đang chiếm giữ
        HashSet<int> uniqueColumns = new HashSet<int>();
        foreach (var c in group)
        {
            if (c != null) uniqueColumns.Add(c.ColumnIndex);
        }

        // Duyệt qua từng cột có mặt trong nhóm
        foreach (int colIndex in uniqueColumns)
        {
            bool canMoveInThisColumn = false;

            // Kiểm tra tất cả các con thuộc cột này
            foreach (var c in group)
            {
                if (c == null) continue;

                // Nếu con này thuộc cột đang xét
                if (c.ColumnIndex == colIndex)
                {
                    // Nếu đang ở hàng đợi thì auto đi được
                    if (c.State == CollectorState.InQueue)
                    {
                        canMoveInThisColumn = true;
                        break;
                    }

                    // Nếu có ít nhất 1 con trong cột này check Raycast thành công (không bị chặn)
                    // Thì coi như cột này thông thoáng (con dẫn đầu sẽ mở đường)
                    if (c.CanMove())
                    {
                        canMoveInThisColumn = true;
                        break;
                    }
                }
            }

            // Nếu sau khi check hết các con trong cột này mà KHÔNG con nào đi được (tất cả đều bị chặn bởi vật lạ)
            // -> Cả nhóm bị kẹt.
            if (!canMoveInThisColumn)
            {
                return false;
            }
        }

        // Nếu tất cả các cột liên quan đều có người dẫn đầu -> Đi tốt
        return true;
    }

    public void OnCollectorFinished(CollectorController collector)
    {
        if (queueManager.IsQueueFull())
        {
            if (hasShowPrelose)
            {
                GameplayEventsManager.OnEndGame?.Invoke(false);
                return;
            }

            hasShowPrelose = true;
            //LayerManager.Instance.ShowPopupRevive();
            Debug.Log("Game Over: Queue full!");
            UiEndGame.Instance.ShowEndGameLose();

            // LUNA: báo end game + mở Store khi thua
            Luna.Unity.LifeCycle.GameEnded();
            Luna.Unity.Playable.InstallFullGame();

            inputManager.BlockGameplayInput();
            return;
        }

        moveLimiter.OnCollectorMoveFinished(collector);

        bool canEnqueue = collector != null && collector.ColorCollector != null &&
            collector.collectorConnect.Any(x => x.ColorCollector != null && x.ColorCollector.BulletLeft > 0);

        if (canEnqueue)
            queueManager.EnqueueCollector(collector);

        activeMovingControllers.Remove(collector);
    }

    public void OnCompleteColor(CollectorController collector)
    {
        moveLimiter.OnCollectorMoveFinished(collector);
        activeMovingControllers.Remove(collector);
    }
    [ContextMenu("OnRevive")]
    // public void OnRevive()
    // {
    //     collectorOnDead = new List<CollectorController>();

    //     var lastInQueue = queueManager.GetLastCollectorInQueue();
    //     if (lastInQueue != null)
    //     {
    //         collectorOnDead.Add(lastInQueue);
    //         queueManager.RemoveCollectorFromQueue(lastInQueue);
    //     }

    //     // Chuyển HashSet sang mảng để có thể truy cập theo chỉ số
    //     var activeMovingArray = activeMovingControllers.ToArray();
    //     for (int i = 0; i < queueManager.GetQueuePositionsOnDeadLength(); i++)
    //     {
    //         if (i >= activeMovingArray.Length) break;
    //         var collector = activeMovingArray[i];
    //         if (collector != null && !collectorOnDead.Contains(collector))
    //             collectorOnDead.Add(collector);
    //     }

    //     Debug.Log($"OnRevive: moving {collectorOnDead.Count} collectors to dead positions");

    //     for (int i = 0; i < collectorOnDead.Count; i++)
    //     {
    //         var collector = collectorOnDead[i];
    //         if (collector == null) continue;

    //         StartCoroutine(StopAndMoveToDeadCoroutine(collector, i));
    //     }

    //     inputManager.UnBlockGameplayInput();
    // }

    // private IEnumerator StopAndMoveToDeadCoroutine(CollectorController collector, int slotIndex)
    // {
    //     yield return collector.StopMovementAtCurrentPosition();

    //     queueManager.MoveCollectorToDeadPosition(collector, slotIndex, () =>
    //             {
    //                 activeMovingControllers.Remove(collector);
    //                 moveLimiter.OnCollectorMoveFinished(collector);
    //             }
    //         );
    // }

    #endregion

    #region === PAUSE / RESUME ===

    private void PauseGame(bool isPause)
    {
        if (isPause) OnPauseGame();
        else OnResumeGame();
    }

    private void OnPauseGame()
    {
        foreach (var controller in activeMovingControllers)
        {
            controller?.PauseMovement();
        }
    }

    private void OnResumeGame()
    {
        foreach (var controller in activeMovingControllers)
        {
            controller?.ResumeMovement();
        }
    }

    #endregion
}
