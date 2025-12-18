# Phân Tích Hệ Thống Gameplay

## Mục Lục
1. [Giới Thiệu Tổng Quan](#giới-thiệu-tổng-quan)
2. [Các Thành Phần Chính](#các-thành-phần-chính)
3. [Phân Tích Chi Tiết Các Lớp](#phân-tích-chi-tiết-các-lớp)
4. [Luồng Điều Khiển Chính](#luồng-điều-khiển-chính)
5. [Điều Kiện Thắng Game](#điều-kiện-thắng-game)
6. [Tương Tác Giữa Các Thành Phần](#tương-tác-giữa-các-thành-phần)
7. [Mở Rộng Và Tùy Chỉnh](#mở-rộng-và-tùy-chỉnh)

## Giới Thiệu Tổng Quan

Hệ thống gameplay của game được xây dựng dựa trên kiến trúc module, cho phép dễ dàng mở rộng và bảo trì. Tài liệu này sẽ đi sâu vào phân tích chi tiết các thành phần chính của hệ thống.

## Các Thành Phần Chính

### 1. Lớp Điều Khiển Chính
- `GamePlayController`: Điều phối chính của gameplay
- `GamePlayUIManager`: Quản lý giao diện người dùng
- `DataManager`: Quản lý dữ liệu game

### 2. Hệ Thống Tương Tác
- `Interactable`: Lớp cơ sở cho các đối tượng tương tác
- `CameraController`: Xử lý đầu vào và điều khiển camera
- `WoolControl`: Quản lý đối tượng wool

### 3. Hệ Thống Mục Tiêu
- `CubeTargetControl`: Quản lý khối mục tiêu
- `QueueTargetControl`: Quản lý hàng đợi mục tiêu
- `TargetBoxAnimator`: Xử lý hiệu ứng hộp mục tiêu

## Phân Tích Chi Tiết Các Lớp

### 1. GamePlayController
Lớp trung tâm điều khiển toàn bộ luồng chơi.

#### Các Phương Thức Chính:
```csharp
public class GamePlayController : MonoBehaviour
{
    // Khởi tạo game
    private void InitializeGame()
    
    // Bắt đầu màn chơi mới
    public void StartNewLevel()
    
    // Tạm dừng game
    public void PauseGame()
    
    // Tiếp tục game
    public void ResumeGame()
    
    // Kết thúc game
    public void EndGame(bool isWin)
    
    // Kiểm tra điều kiện thắng/thua
    private void CheckWinCondition()
    
    // Cập nhật trạng thái game
    private void UpdateGameState(GameState newState)
}
```

### 2. WoolControl
Quản lý đối tượng wool và các tương tác của nó.

#### Các Phương Thức Chính:
```csharp
public class WoolControl : MonoBehaviour
{
    // Khởi tạo wool
    public void InitializeWool()
    
    // Xử lý khi người chơi tương tác với wool
    public void OnInteract()
    
    // Thay đổi màu sắc của wool
    private void ChangeColor(string newColor)
    
    // Cập nhật trạng thái hiển thị
    private void UpdateVisualState()
    
    // Xử lý animation xoay
    private IEnumerator RotateAnimation()
}
```

### 3. CameraController
Điều khiển camera và xử lý đầu vào người dùng.

#### Các Phương Thức Chính:
```csharp
public class CameraController : MonoBehaviour
{
    // Xử lý đầu vào chạm
    private void HandleTouchInput()
    
    // Di chuyển camera
    private void MoveCamera(Vector2 delta)
    
    // Phóng to/thu nhỏ
    private void Zoom(float amount)
    
    // Tìm đối tượng được chọn
    private GameObject FindSelectedObject(Vector2 screenPosition)
}
```

## Luồng Điều Khiển Chính

1. **Khởi Tạo Game**:
   - `GamePlayController.InitializeGame()`
   - Tải dữ liệu người chơi
   - Khởi tạo camera và UI
   - Tạo level

2. **Vòng Lặp Gameplay**:
   - Xử lý đầu vào người dùng
   - Cập nhật trạng thái game
   - Kiểm tra điều kiện thắng/thua
   - Cập nhật UI

3. **Kết Thúc Màn Chơi**:
   - Dừng các hệ thống con
   - Hiển thị kết quả
   - Lưu tiến trình

## Điều Kiện Thắng Game

### 1. Điều Kiện Thắng
Người chơi chiến thắng khi **đồng thời** thỏa mãn 2 điều kiện:

1. **Thu thập đủ màu yêu cầu**:
   - `_currentColorCollected == _meshController.TotalColor`
   - `_currentColorCollected`: Số lượng màu đã thu thập
   - `_meshController.TotalColor`: Tổng số màu cần thu thập

2. **Hoàn thành tất cả các mục tiêu**:
   - `CubeReadyCount == TotalCubeActive`
   - `CubeReadyCount`: Số lượng mục tiêu đã hoàn thành
   - `TotalCubeActive`: Tổng số mục tiêu cần hoàn thành

### 2. Kiểm Tra Điều Kiện Thắng
```csharp
public void CheckEndGame()
{
    // Điều kiện thắng
    if (_currentColorCollected == _meshController.TotalColor && CubeReadyCount == TotalCubeActive)
    {
        OnEndGameAction(true); // Gọi khi thắng
    }
    
    // Điều kiện thua
    if (_queueCount >= CurrentQueueTargets.Count && TotalCubeActive == CubeReadyCount)
    {
        OnEndGameAction(false); // Gọi khi thua
    }
}
```

### 3. Xử Lý Khi Thắng Game
```csharp
private async UniTask OnEndGameAction(bool isWin)
{
    if(_isUseBroomBooster) return;
    IsEndGame = true;
    
    if (isWin)
    {
        if (!_isWinGame)
        {
            _isWinGame = true;
            GameEventManager.OnEndGameAction?.Invoke(true);
        }
    }
    // ... (xử lý thua game)
}
```

### 4. Giải Thích Chi Tiết

1. **Kiểm tra thắng game**:
   - Hệ thống liên tục kiểm tra điều kiện thắng thông qua `CheckEndGame()`
   - Khi đủ điều kiện, gọi `OnEndGameAction(true)`

2. **Xử lý khi thắng**:
   - Kiểm tra không sử dụng booster chổi quét (`_isUseBroomBooster`)
   - Đánh dấu trạng thái kết thúc game (`IsEndGame = true`)
   - Kích hoạt sự kiện `GameEventManager.OnEndGameAction` để thông báo cho các hệ thống khác

3. **Điều kiện thua**:
   - Khi đã sử dụng hết lượt trong hàng đợi (`_queueCount >= CurrentQueueTargets.Count`)
   - Và đã hoàn thành tất cả mục tiêu hiện có (`TotalCubeActive == CubeReadyCount`)

## Tương Tác Giữa Các Thành Phần

### 1. Tương Tác Với Hệ Thống UI
- `GamePlayUIManager` nhận sự kiện từ `GamePlayController`
- Cập nhật thông tin hiển thị (điểm số, thời gian, v.v.)

### 2. Tương Tác Với Hệ Thống Booster

#### 2.1. Broom Booster
- **Kích hoạt**: Thông qua sự kiện `GameEventManager.OnUseBroomBooster`
- **Xử lý**:
  ```csharp
  private void OnUseBroomBooster()
  {
      if (DataManager.PlayerInfoData.BroomAmount <= 0) return;
      _isUseBroomBooster = true;
      // Xử lý hiệu ứng và logic xóa mục tiêu
  }
  ```

#### 2.2. Rainbow Box Booster
- **Kích hoạt**: Thông qua sự kiện `GameEventManager.OnUseRainbowBooster`
- **Xử lý**:
  ```csharp
  private void OnUseRainbowBooster()
  {
      if (_isUsingRainBowBooster) return;
      _isUsingRainBowBooster = true;
      // Xử lý logic tạo hộp cầu vồng
  }
  ```

#### 2.3. AddHold Booster
- **Kích hoạt**: Thông qua sự kiện `GameEventManager.OnUseAddHoldBooster`
- **Xử lý**:
  ```csharp
  private void OnUseAddHoldBooster()
  {
      if (TotalCubeActive >= maxCubeTarget) return;
      // Xử lý thêm ô chứa mục tiêu
  }
  ```

## Chi Tiết Các Thành Phần Trong Màn Chơi

### 1. Khối Len (Wool)
- **Lớp chính**: `WoolControl`
- **Thuộc tính**:
  - `ColorStack`: Danh sách các lớp màu
  - `CurrentColor`: Màu sắc hiện tại
  - `IsRotating`: Trạng thái đang xoay

### 2. Hệ Thống Mục Tiêu

#### 2.1. Cube Targets
- **Lớp chính**: `CubeTargetControl`
- **Thuộc tính**:
  - `TargetColor`: Màu sắc mục tiêu
  - `IsCompleted`: Trạng thái hoàn thành
  - `CompleteAnimation()`: Phát hiệu ứng hoàn thành

#### 2.2. Queue Targets
- **Lớp chính**: `QueueTargetControl`
- **Chức năng**:
  - Quản lý hàng đợi mục tiêu tiếp theo
  - Hỗ trợ hiệu ứng chuyển tiếp giữa các mục tiêu

### 3. Hệ Thống Xử Lý Đầu Vào

#### 3.1. Camera Controller
- **Chức năng chính**:
  - Xử lý cử chỉ chạm
  - Điều khiển góc nhìn camera
  - Hỗ trợ thao tác phóng to/thu nhỏ

#### 3.2. Input Manager
- **Xử lý**:
  - Nhận diện cử chỉ vuốt
  - Phân biệt giữa chạm đơn và chạm đa điểm
  - Chuyển đổi tọa độ màn hình sang thế giới game

## Tối Ưu Hóa Hiệu Suất

### 1. Quản Lý Bộ Nhớ
- Sử dụng Object Pooling cho các đối tượng thường xuyên được tạo/hủy
- Giải phóng tài nguyên không cần thiết khi chuyển cảnh

### 2. Xử Lý Đồ Họa
- Sử dụng GPU Instancing cho các đối tượng giống nhau
- Tối ưu hóa shader và material

### 3. Xử Lý Vật Lý
- Sử dụng layer mask để giới hạn phạm vi kiểm tra va chạm
- Tối ưu hóa các collider

## Mở Rộng Và Tùy Chỉnh

### 1. Thêm Loại Booster Mới
1. Tạo class mới kế thừa từ `BaseBooster`
2. Đăng ký sự kiện tương ứng
3. Thêm logic xử lý riêng

### 2. Tùy Chỉnh Màn Chơi
- Chỉnh sửa `LevelConfigSO` để thay đổi cấu hình
- Thêm các điều kiện chiến thắng đặc biệt

### 3. Tích Hợp Hệ Thống Mới
1. Tạo interface mới nếu cần
2. Đăng ký sự kiện tương ứng
3. Xử lý logic trong `GamePlayManager`
- Hiển thị thông báo và hướng dẫn

### 2. Tương Tác Với Hệ Thống Lưu Trữ
- `DataManager` xử lý việc lưu/đọc dữ liệu
- Lưu trạng thái game khi cần thiết
- Đọc cấu hình level từ file

### 3. Tương Tác Với Hệ Thống Âm Thanh
- Phát âm thanh khi có sự kiện (tương tác, hoàn thành mục tiêu, v.v.)
- Điều chỉnh âm lượng và hiệu ứng âm thanh

## Hệ Thống Bóc Tách Lớp Len Và Quản Lý Hàng Đợi

### 1. Tổng Quan Về Bóc Tách Lớp Len

#### 1.1 Nguyên Lý Hoạt Động
- Mỗi cuộn len (`WoolControl`) chứa nhiều lớp màu xếp chồng lên nhau
- Người chơi tương tác để bóc tách từng lớp màu
- Mỗi lần bóc tách sẽ lộ ra lớp màu bên dưới

#### 1.2 Quy Trình Xử Lý
1. Người chơi chạm vào cuộn len
2. Hệ thống kiểm tra lớp màu hiện tại
3. Thực hiện animation bóc tách lớp
4. Cập nhật trạng thái hiển thị

### 2. Hệ Thống Hàng Đợi (Queue System)

#### 2.1 Đặc Điểm
- Là nơi lưu trữ tạm thời các lớp len không phù hợp
- Có giới hạn số lượng tối đa
- Khi đầy sẽ dẫn đến thua cuộc

#### 2.2 Các Thành Phần Chính
```csharp
public List<QueueTargetControl> CurrentQueueTargets // Danh sách các ô hàng đợi
private int _queueCount // Số lượng ô đã sử dụng
```

#### 2.3 Điều Kiện Kiểm Tra
- **Thêm vào hàng đợi**: Khi không có hộp đích phù hợp
- **Cảnh báo**: Khi chỉ còn 1 ô trống
- **Thua cuộc**: Khi hàng đợi đầy và không còn nước đi hợp lệ

### 3. Hệ Thống Hộp Len (Cube Target)

#### 3.1 Đặc Điểm
- Mỗi hộp chứa tối đa 3 lớp len cùng màu
- Khi đủ 3 lớp, hộp được coi là hoàn thành
- Màu sắc hộp thay đổi theo màu lớp len

#### 3.2 Các Trạng Thái Chính
- **Chưa kích hoạt**: Chưa sẵn sàng nhận len
- **Đang hoạt động**: Đang nhận len
- **Đã hoàn thành**: Đã đủ 3 lớp len

#### 3.3 Phương Thức Quan Trọng
```csharp
public void AddChild(int indexCube, out Transform child) // Thêm len vào hộp
public bool CheckColor(string color) // Kiểm tra màu sắc phù hợp
private async UniTask WaitingAnim(int indexCube) // Xử lý khi hoàn thành hộp
```

### 4. Tương Tác Giữa Các Thành Phần

#### 4.1 Luồng Xử Lý Khi Bóc Tách Len
1. Kiểm tra màu có khớp với hộp đích nào không
2. Nếu khớp, thêm vào hộp đích tương ứng
3. Nếu không khớp, thêm vào hàng đợi
4. Kiểm tra điều kiện thua khi thêm vào hàng đợi

#### 4.2 Kiểm Tra Điều Kiện Thua
```csharp
// Trong CheckChosenColorInQueueTarget
if (_queueCount >= CurrentQueueTargets.Count && CubeReadyCount == TotalCubeActive)
{
    OnEndGameAction(false); // Kết thúc game với trạng thái thua
}
```

### 5. Tối Ưu Hóa Và Hiệu Ứng

#### 5.1 Hiệu Ứng Hình Ảnh
- Hiệu ứng bóc tách mượt mà
- Đường dẫn chuyển động khi di chuyển giữa các vị trí
- Hiệu ứng hoàn thành hộp

#### 5.2 Tối Ưu Hiệu Năng
- Sử dụng object pooling cho các đối tượng len
- Tái sử dụng đối tượng thay vì khởi tạo mới
- Tối ưu hóa animation và hiệu ứng

## Mở Rộng Và Tùy Chỉnh

### 1. Thêm Màu Sắc Mới
1. Bổ sung vào `colorPalleteData`
2. Cập nhật hệ thống kiểm tra màu
3. Thêm tài nguyên hình ảnh tương ứng

### 2. Thay Đổi Kích Thước Hàng Đợi
- Điều chỉnh `CurrentQueueTargets` trong quá trình khởi tạo level
- Cập nhật giao diện người dùng tương ứng

### 3. Thêm Cơ Chế Đặc Biệt
- Hộp đa sắc: Có thể nhận nhiều màu
- Len đặc biệt: Có khả năng đặc biệt khi bóc tách
- Vật phẩm tăng kích thước hàng đợi tạm thởi

### 4. Thêm Đối Tượng Tương Tác Mới
1. Tạo script mới kế thừa từ `Interactable`
2. Đăng ký với `GamePlayController`
3. Thêm vào hệ thống quản lý tương ứng

### 5. Tùy Chỉnh Gameplay
- Điều chỉnh thông số trong `DataManager`
- Tạo mức chơi mới với các thông số khác nhau
- Thêm loại mục tiêu mới

### 3. Tối Ưu Hóa
- Sử dụng object pooling cho các đối tượng thường xuyên được tạo/hủy
- Tối ưu hóa draw call
- Giảm tải bộ nhớ bằng cách quản lý tài nguyên hiệu quả

## Kết Luận

Hệ thống gameplay được thiết kế linh hoạt, dễ mở rộng và bảo trì. Với kiến trúc module rõ ràng, việc thêm tính năng mới hoặc điều chỉnh luồng chơi hiện có đều có thể thực hiện dễ dàng mà không ảnh hưởng đến các thành phần khác của hệ thống.
