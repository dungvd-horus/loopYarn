# Hệ Thống Camera

## Mục Lục
1. [Tổng Quan](#tổng-quan)
2. [Các Thành Phần Chính](#các-thành-phần-chính)
3. [Cấu Hình](#cấu-hình)
4. [Điều Khiển Camera](#điều-khiển-camera)
   - [Xoay Camera](#1-xoay-camera)
   - [Phóng To/Thu Nhỏ](#2-phóng-tothu-nhỏ)
   - [Tự Động Xoay](#3-tự-động-xoay)
5. [Xử Lý Đầu Vào](#xử-lý-đầu-vào)
6. [Tích Hợp Với Gameplay](#tích-hợp-với-gameplay)
7. [Tối Ưu Hóa](#tối-ưu-hóa)
8. [Mở Rộng](#mở-rộng)

## Tổng Quan

Hệ thống camera trong game được thiết kế để cung cấp trải nghiệm xem và tương tác mượt mà với các đối tượng 3D. Hệ thống hỗ trợ nhiều chế độ điều khiển khác nhau và tự động điều chỉnh để phù hợp với từng trạng thái game.

## Các Thành Phần Chính

### 1. Lớp Chính: `CameraController`
- Kế thừa từ `SingletonBase<CameraController>`
- Quản lý toàn bộ trạng thái và hành vi của camera
- Xử lý các sự kiện đầu vào từ người dùng

### 2. Các Đối Tượng Quan Trọng
- `_mainCamera`: Camera chính của game
- `_fakeUICamera`: Camera phụ cho giao diện người dùng
- `modelTransfrom`: Transform của đối tượng 3D đang được điều khiển
- `targetRotation`: Góc quay mục tiêu của đối tượng

## Cấu Hình

### 1. Vị Trí Và Góc Quay Mặc Định
```csharp
// Menu chính
_cameraPosMainMenuDefault = new Vector3(0, 1.8f, -11);
_cameraRoteMainMenuDefault = new Vector3(15f, 0, 0);

// Gameplay
_cameraPosGamePlayDefault = new Vector3(0, 1, -10);
_cameraRoteGamePlayDefault = new Vector3(6, 0, 0);
```

### 2. Thông Số Điều Chỉnh
```csharp
// Độ nhạy xoay
public Vector2 RotationSensitivity = new(1f, 1f);

// Phạm vi gia tốc
public Vector2 AccelerationRange = new(0.1f, 1f);

// Tốc độ xoay
public float RotationSpeed = 5f;

// Thời gian chờ trước khi tự động xoay
public float TimeAFKToAutoRotation = 10f;
```

## Điều Khiển Camera

### 1. Xoay Camera

#### Thủ Công
- Kéo ngón tay trên màn hình để xoay đối tượng
- Hỗ trợ cảm ứng đa điểm

#### Tự Động
- Khi không có tương tác, camera tự động xoay chậm
- Có thể tùy chỉnh tốc độ và hướng xoay

### 2. Phóng To/Thu Nhỏ

#### Cử Chỉ Thu Phóng
- Dùng 2 ngón tay chụm/xòe để phóng to/thu nhỏ
- Giới hạn FOV tối thiểu và tối đa

#### Code Xử Lý
```csharp
private bool OnZoomCameraSmoothly()
{
    if (Input.touchCount != 2) return false;
    
    Touch touch1 = Input.GetTouch(0);
    Touch touch2 = Input.GetTouch(1);
    
    // Tính khoảng cách giữa 2 ngón tay
    float currentDistance = Vector2.Distance(touch1.position, touch2.position);
    
    // Cập nhật FOV
    targetFOV = Mathf.Clamp(
        _mainCamera.fieldOfView + delta * ZoomCameraData.ZoomSpeed,
        ZoomCameraData.MinFOV,
        ZoomCameraData.MaxFOV
    );
    
    return true;
}
```

### 3. Tự Động Xoay
- Kích hoạt khi không có tương tác trong `TimeAFKToAutoRotation` giây
- Tốc độ xoay có thể điều chỉnh thông qua `RotationAutoSpeed`

## Xử Lý Đầu Vào

### 1. Chạm (Tap)
- Phát hiện va chạm với đối tượng 3D
- Kích hoạt sự kiện tương tác với đối tượng

### 2. Giữ (Hold)
- Nhấn và giữ để tương tác lâu với đối tượng
- Có thể sử dụng để chọn hoặc tương tác đặc biệt

### 3. Kéo (Drag)
- Xoay đối tượng theo chuyển động của ngón tay
- Hỗ trợ kéo mượt mà với `SmoothFactor`

## Tích Hợp Với Gameplay

### 1. Thay Đổi Trạng Thái Game
```csharp
private void SetActiveInteractable(GameState state)
{
    switch (state)
    {
        case GameState.MainMenu:
            _isActive = true;
            _isRotateObjectInMainMenu = true;
            break;
        case GameState.GamePlay:
            _isActive = true;
            _isRotateObjectInMainMenu = false;
            break;
        default:
            _isActive = false;
            break;
    }
}
```

### 2. Sự Kiện
- `OnHandleTapWoolAction`: Khi người chơi chạm vào wool
- `OnHandleHoldWoolAction`: Khi người chơi giữ lâu vào wool
- `OnHandleDragWoolAction`: Khi người chơi kéo để xoay

## Tối Ưu Hóa

1. **Singleton Pattern**: Đảm bảo chỉ có một instance của CameraController
2. **Tính Toán Hiệu Suất**: Chỉ cập nhật khi cần thiết
3. **Hệ Thống Sự Kiện**: Giảm sự phụ thuộc giữa các thành phần
4. **Object Pooling**: Tái sử dụng đối tượng

## Mở Rộng

### Thêm Chức Năng Mới
1. Tạo phương thức mới trong `CameraController`
2. Đăng ký sự kiện đầu vào nếu cần
3. Thêm các thông số cấu hình vào `ZoomCameraData` nếu cần

### Tùy Chỉnh Hành Vi
1. Điều chỉnh các thông số trong Inspector
2. Ghi đè các phương thức ảo nếu cần thay đổi hành vi mặc định

## Lưu Ý Khi Phát Triển
- Luôn kiểm tra `_isActive` trước khi xử lý đầu vào
- Sử dụng `Time.deltaTime` cho các phép tính liên quan đến thời gian
- Đảm bảo xử lý đúng các sự kiện để tránh rò rỉ bộ nhớ
