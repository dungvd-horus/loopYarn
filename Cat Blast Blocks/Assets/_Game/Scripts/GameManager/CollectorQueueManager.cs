using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CollectorQueueManager : MonoBehaviour
{
    // Danh sách động quản lý các collector dead
    public List<CollectorController> deadQueue = new List<CollectorController>();
    public int maxSlot = 5;
    public Transform[] queuePositions; // Đặt sẵn các vị trí hàng đợi trong scene
    // Vị trí mặc định cho collector dead
    public Transform defaultDeadPosition;
    // Khoảng cách dịch sang phải cho mỗi collector dead
    public Vector3 deadOffset = new Vector3(1f, 0f, 0f);

    public CollectorController[] queueArray; //in queue

    public AlertFullSlotAnim alertFullSlotAnim;

    private void Awake()
    {
        queueArray = new CollectorController[maxSlot];

    }

    public void AddMaxSlot(int increment = 1)
    {
        SetMaxSlot(maxSlot + increment);
    }

    // Thay đổi số lượng slot hàng đợi động (ví dụ khi dùng booster)
    public void SetMaxSlot(int newMaxSlot)
    {
        if (newMaxSlot == maxSlot) return;
        var newArray = new CollectorController[newMaxSlot];
        int copyCount = Mathf.Min(queueArray != null ? queueArray.Length : 0, newMaxSlot);
        for (int i = 0; i < copyCount; i++)
        {
            newArray[i] = queueArray[i];
        }
        queueArray = newArray;
        maxSlot = newMaxSlot;
    }

    public bool IsQueueFull()
    {
        for (int i = 0; i < maxSlot; i++)
        {
            if (queueArray[i] == null)
                return false;
        }
        return true;
    }

    // Thêm collector vào slot trống đầu tiên
    public void EnqueueCollector(CollectorController collector)
    {
        if (IsQueueFull())
        {
            Debug.Log("Queue is full, cannot enqueue collector.");
            return;
        }

        for (int i = 0; i < maxSlot; i++)
        {
            if (queueArray[i] == null)
            {
                queueArray[i] = collector;
                StartCoroutine(MoveCollectorToQueuePosition(collector, i));

                if (alertFullSlotAnim != null && IsQueueFull())
                {
                    alertFullSlotAnim.PlayAlertAnim();
                }
                //Debug.Log($"[CollectorQueueManager] Enqueued collector {collector.ColorCollector.ID} to slot {i}");
                return;
            }
        }

        return;
    }

    public void RemoveCollectorFromDeadQueue(CollectorController collector)
    {
        int index = deadQueue.IndexOf(collector);
        if (index == -1) return;
        deadQueue.RemoveAt(index);
        // Cập nhật lại vị trí cho các collector phía sau
        for (int i = index; i < deadQueue.Count; i++)
        {
            MoveCollectorToDeadPosition(deadQueue[i]);
        }
    }

    // Xóa collector khỏi queue nếu có, chỉ xóa khỏi mảng
    public void RemoveCollectorFromQueue(CollectorController collector)
    {
        for (int i = 0; i < maxSlot; i++)
        {
            if (queueArray[i] == collector)
            {
                // Dịch các collector phía sau lên một slot
                for (int j = i; j < maxSlot - 1; j++)
                {
                    queueArray[j] = queueArray[j + 1];
                    if (queueArray[j] != null)
                    {
                        PushForwardCollectorOnQueue(queueArray[j], j);
                    }
                }
                //Debug.Log($"[CollectorQueueManager] Removed collector {collector.ColorCollector.ID} from slot {i}");
                queueArray[maxSlot - 1] = null;
                return;
            }
        }
        return;
    }

    // Kiểm tra collector có thuộc queue không
    public bool IsInQueue(CollectorController collector)
    {
        for (int i = 0; i < maxSlot; i++)
        {
            if (queueArray[i] != null && queueArray[i].ColorCollector.ID == collector.ColorCollector.ID)
                return true;
        }
        return false;
    }

    // Trả về CollectorController đang ở slot cuối cùng (slot có index cao nhất mà chứa collector).
    // Nếu không có collector nào trong queue hoặc mảng chưa khởi tạo, trả về null.
    public CollectorController GetLastCollectorInQueue()
    {
        if (queueArray == null || maxSlot <= 0)
            return null;

        // Duyệt từ cuối về đầu để tìm collector cuối cùng (index lớn nhất) có giá trị khác null
        for (int i = Mathf.Min(maxSlot, queueArray.Length) - 1; i >= 0; i--)
        {
            if (queueArray[i] != null)
                return queueArray[i];
        }

        return null;
    }

    // Đặt collector vào đúng vị trí hàng đợi
    private IEnumerator MoveCollectorToQueuePosition(CollectorController collector, int index)
    {
        //await UniTask.DelayFrame(1); // Đợi 1 frame để tránh lỗi transform
        GameplayEventsManager.OnACollectorMoveToQueue?.Invoke(collector);
        // Action action = () => 
        // {
        //     collector.DeUpdateText();
        // };
        if (queuePositions == null)
        {
            yield break;
        }
        collector?.MoveToPos(queuePositions[index].position, CollectorAnimState.MoveToQueueRotateBack, null);
    }

    public void MoveCollectorToDeadPosition(CollectorController collector, Action onComplete = null)
    {
        // Nếu collector chưa có trong deadQueue thì thêm vào cuối
        int realIndex = deadQueue.IndexOf(collector);
        if (realIndex == -1)
        {
            deadQueue.Add(collector);
            realIndex = deadQueue.Count - 1;
        }
        // Nếu đã có thì giữ nguyên vị trí hiện tại

        Vector3 targetPosition = defaultDeadPosition.position + deadOffset * realIndex;
        // Combine internal cleanup action với callback bên ngoài
        Action internalAction = () => collector.ColorCollector.RabbitRotateTransform.eulerAngles = Vector3.zero;
        Action combined = null;
        if (onComplete != null)
            combined = () => { internalAction(); onComplete(); };
        else
            combined = internalAction;

        collector.MoveToPos(targetPosition, CollectorAnimState.MoveToDeadPosition, combined);
    }

    private void PushForwardCollectorOnQueue(CollectorController collector, int index)
    {
        GameplayEventsManager.OnACollectorMoveToQueue?.Invoke(collector);
        collector.MoveToPos(queuePositions[index].position, CollectorAnimState.MoveToQueueRotateBack);
    }

    public bool HasEnoughSlotsFor(int _slot)
    {
        int availableSlots = 0;
        for (int i = 0; i < maxSlot; i++)
        {
            if (queueArray[i] == null)
                availableSlots++;
        }
        return availableSlots >= _slot;
    }
}
