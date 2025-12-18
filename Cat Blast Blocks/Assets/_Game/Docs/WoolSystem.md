# Tài Liệu Hệ Thống Wool

## Mục Lục
1. [Tổng Quan](#tổng-quan)
2. [Kiến Trúc Hệ Thống](#kiến-trúc-hệ-thống)
3. [Thành Phần Chính](#thành-phần-chính)
4. [Cơ Chế Hoạt Động](#cơ-chế-hoạt-động)
5. [Tích Hợp Với Hệ Thống Khác](#tích-hợp-với-hệ-thống-khác)
6. [Hướng Dẫn Sử Dụng](#hướng-dẫn-sử-dụng)
7. [Tối Ưu Hóa](#tối-ưu-hóa)
8. [Mở Rộng Và Tùy Chỉnh](#mở-rộng-và-tùy-chỉnh)

## Tổng Quan
Hệ thống Wool là trái tim của game, quản lý các đối tượng wool mà người chơi tương tác. Mỗi đối tượng wool có thể có nhiều lớp màu (tối đa 5 lớp), mỗi lớp có thể có màu sắc và thuộc tính riêng biệt, hỗ trợ các thao tác xoay, đổi màu và tương tác đa dạng.

## Kiến Trúc Hệ Thống

### Lớp Chính: `WoolControl`
- **Vai trò**: Điều khiển chính của đối tượng wool
- **Kế thừa**: `MonoBehaviour`, `IDebugUtilities`
- **Vị trí**: `Assets/_Game/Scripts/GamePlay/WoolControl.cs`

### Các Lớp Hỗ Trợ
1. **MeshObjectData**
   - Lưu trữ thông tin mesh
   - Quản lý stack màu sắc

2. **ColorPalleteData**
   - Chứa bảng màu game
   - Ánh xạ tên màu - giá trị

3. **WoolAnimationData**
   - Dữ liệu animation
   - Điều khiển hiệu ứng chuyển động

## Thành Phần Chính

### 1. Hiển Thị
- **TopMeshRenderer**: Renderer chính hiển thị lớp màu trên cùng
- **HideMeshRenderer**: Renderer phụ cho hiệu ứng trong suốt
- **BoxCollider**: Xử lý va chạm

### 2. Dữ Liệu
```csharp
public class MeshObjectData 
{
    public List<string> ColorStack;  // Stack lưu trữ các lớp màu
    public string HightestColor;     // Màu hiện tại ở đỉnh stack
    public int TotalLayer;           // Tổng số lớp màu tối đa (mặc định: 5)
    public int CurrentLayerIndex;    // Chỉ số lớp hiện tại đang hiển thị
    public List<WoolLayer> Layers;   // Danh sách các lớp wool
}

[Serializable]
public class WoolLayer
{
    public string ColorID;           // Định danh màu sắc
    public float Thickness;          // Độ dày của lớp
    public bool IsVisible;           // Trạng thái hiển thị
    public List<WoolDecoration> Decorations; // Các vật trang trí trên lớp
}
```

### 3. Tương Tác
- **Interactable**: Xử lý đầu vào từ người dùng
- **WoolRotation**: Điều khiển xoay wool
- **PushColor**: Thêm màu mới vào stack

## Các Lớp Wool Và Tương Tác

### 1. Cấu Trúc Lớp Wool
Mỗi đối tượng wool có thể có tối đa 5 lớp chồng lên nhau:

1. **Lớp Ngoài Cùng (Layer 0)**
   - Hiển thị trực quan
   - Tương tác trực tiếp với người chơi
   - Có thể có các hiệu ứng đặc biệt

2. **Các Lớp Bên Trong (Layer 1-3)**
   - Ẩn dưới các lớp bên ngoài
   - Hiển thị khi lớp bên ngoài bị loại bỏ
   - Có thể có thuộc tính đặc biệt

3. **Lớp Trong Cùng (Layer 4)**
   - Lớp cơ bản nhất
   - Thường có màu đặc trưng
   - Không thể bị loại bỏ

### 2. Tương Tác Giữa Các Lớp
- **Chuyển Lớp**: Khi loại bỏ một lớp, lớp bên dưới sẽ hiển thị
- **Kết Hợp Màu**: Một số tương tác đặc biệt có thể kết hợp màu giữa các lớp
- **Hiệu Ứng Chuyển Tiếp**: Hiệu ứng mượt mà khi chuyển giữa các lớp

## Cơ Chế Hoạt Động

### 1. Khởi Tạo Và Thiết Lập Lớp

#### a. Khởi Tạo Các Lớp
```csharp
private void InitializeLayers()
{
    if (MeshObjectData.Layers == null || MeshObjectData.Layers.Count == 0)
    {
        MeshObjectData.Layers = new List<WoolLayer>();
        for (int i = 0; i < MeshObjectData.TotalLayer; i++)
        {
            var layer = new WoolLayer
            {
                ColorID = i == 0 ? MeshObjectData.HightestColor : "default",
                Thickness = 1.0f / MeshObjectData.TotalLayer,
                IsVisible = i == 0,
                Decorations = new List<WoolDecoration>()
            };
            MeshObjectData.Layers.Add(layer);
        }
    }
}
```

#### b. Khởi Tạo Wool
```csharp
public void InitMesh()
{
    MeshObjectData.ColorStack.Clear();
    _indexLayer = 0;
    BoxCollider.enabled = true;
    _currentColor = MeshObjectData.HightestColor;
    PushColor(MeshObjectData.HightestColor);
}
```

### 2. Quản Lý Lớp Màu

#### a. Thêm Lớp Màu Mới
```csharp
public bool AddLayer(string colorID, float thickness = 0.2f)
{
    if (MeshObjectData.Layers.Count >= MeshObjectData.TotalLayer)
        return false;

    var newLayer = new WoolLayer
    {
        ColorID = colorID,
        Thickness = thickness,
        IsVisible = false,
        Decorations = new List<WoolDecoration>()
    };

    MeshObjectData.Layers.Insert(0, newLayer);
    UpdateLayerVisibility();
    return true;
}
```

#### b. Xóa Lớp Trên Cùng
```csharp
public bool RemoveTopLayer()
{
    if (MeshObjectData.Layers.Count <= 1) // Giữ lại ít nhất 1 lớp
        return false;

    MeshObjectData.Layers.RemoveAt(0);
    UpdateLayerVisibility();
    return true;
}

private void UpdateLayerVisibility()
{
    for (int i = 0; i < MeshObjectData.Layers.Count; i++)
    {
        MeshObjectData.Layers[i].IsVisible = (i == 0);
    }
    UpdateVisuals();
}
```

#### c. Thêm Màu Mới
```csharp
public bool PushColor(string color)
{
    if (MeshObjectData == null || _indexLayer >= MeshObjectData.TotalLayer) 
        return false;

    MeshObjectData.ColorStack.Add(color);
    _indexLayer++;
    // Cập nhật hiển thị...
    return true;
}
```

### 3. Xử Lý Xoay
```csharp
public void WoolRotation()
{
    if (CoreGameplayManager.Instance.CurrentGameState != GameState.InGame) 
        return;
        
    cancellationToken = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);
    AsyncWoolRotation(cancellationToken.Token).Forget();
}
```

## Hiệu Ứng Đặc Biệt Theo Lớp

### 1. Hiệu Ứng Chuyển Tiếp
- **Morphing**: Chuyển đổi mượt mà giữa các lớp
- **Particle Effects**: Hiệu ứng hạt khi thay đổi lớp
- **Âm Thanh**: Âm thanh đặc trưng cho mỗi lớp

### 2. Vật Trang Trí (Decorations)
Mỗi lớp có thể chứa các vật trang trí:
- Hoa văn
- Hạt kim tuyến
- Các chi tiết đặc biệt

### 3. Tương Tác Đa Lớp
- **Kết Hợp Màu**: Pha trộn màu giữa các lớp
- **Hiệu Ứng Xuyên Thấu**: Nhìn thấy một phần lớp bên dưới

## Tích Hợp Với Hệ Thống Khác

### 1. Tương Tác Với Camera
- **CameraController** xử lý theo dõi và tương tác với wool
- Hỗ trợ phát hiện va chạm thông qua raycast
- Tự động điều chỉnh góc nhìn và zoom cho phù hợp

### 2. Tích Hợp Vật Lý
- Sử dụng BoxCollider 2D cho phát hiện va chạm
- Hỗ trợ hiệu ứng vật lý khi tương tác
- Tích hợp với hệ thống particle cho hiệu ứng đẹp mắt

### 3. Quản Lý Màu Sắc
- Sử dụng ColorPalleteData để quản lý bảng màu thống nhất
- Hỗ trợ chuyển đổi màu sắc mượt mà giữa các lớp
- Tích hợp với shader để hiệu ứng hình ảnh đẹp mắt

### 4. Hệ Thống Âm Thanh
- Phát âm thanh khi tương tác với wool
- Hỗ trợ rung trên thiết bị di động
- Tích hợp với AudioManager để quản lý âm thanh tập trung

### 1. CoreGameplayManager
- Quản lý trạng thái game
- Điều phối tương tác giữa các wool

### 2. UI System
- Hiển thị thông tin wool
- Cập nhật giao diện theo trạng thái

### 3. Vật Lý
- Xử lý va chạm
- Tương tác vật lý cơ bản

## Tương Tác Và Điều Khiển

### 1. Các Thao Tác Cơ Bản
- **Chạm đơn**: Kích hoạt xoay wool và thay đổi màu
- **Giữ và kéo**: Di chuyển camera quanh wool
- **Zoom**: Phóng to/thu nhỏ để quan sát chi tiết

### 2. Các Trạng Thái Của Wool
- **Bình thường**: Hiển thị lớp màu hiện tại
- **Đang tương tác**: Phát hiện đầu vào người dùng
- **Vô hiệu hóa**: Không thể tương tác

### 3. Xử Lý Sự Kiện
- OnWoolRotate: Kích hoạt khi wool xoay
- OnColorChange: Khi màu sắc thay đổi
- OnInteractionStart/End: Bắt đầu/kết thúc tương tác

## Hướng Dẫn Sử Dụng

### 1. Tạo Mới Một Wool
1. Tạo đối tượng 3D mới trong scene
2. Thêm component `WoolControl`
3. Thiết lập các tham số cơ bản:
   - MeshObjectData
   - ColorPalleteData
   - Các thành phần renderer

### 2. Tùy Chỉnh Màu Sắc
1. Sửa đổi `ColorPalleteData`
2. Gọi `PushColor("tên_màu")` để thay đổi màu

## Tối Ưu Hóa

### 1. Hiệu Suất
- Sử dụng MaterialPropertyBlock thay vì tạo material mới
- Tối ưu hóa draw call bằng cách gộp các đối tượng tương tự
- Sử dụng object pooling cho các đối tượng thường xuyên được tạo/hủy

### 2. Bộ Nhớ
- Quản lý bộ nhớ hiệu quả bằng cách giải phóng tài nguyên không cần thiết
- Sử dụng caching cho các đối tượng thường xuyên truy cập
- Tối ưu kích thước texture và mesh

### 3. Đồ Họa
- Sử dụng LOD (Level of Detail) cho các đối tượng ở xa
- Tối ưu shader cho hiệu suất tốt hơn
- Sử dụng culling để giảm tải render


### 1. MaterialPropertyBlock
```csharp
private void UpdateMaterialColor()
{
    if (_topMaterialPropertyBlock == null)
        _topMaterialPropertyBlock = new MaterialPropertyBlock();
        
    _topMaterialPropertyBlock.SetColor("_Color", currentColor);
    TopMeshRenderer.SetPropertyBlock(_topMaterialPropertyBlock);
}
```

### 2. Object Pooling
- Sử dụng pool cho các đối tượng wool
- Tái sử dụng thay vì khởi tạo mới

## Mở Rộng Và Tùy Chỉnh

### 1. Thêm Hiệu Ứng Mới
- Tạo script mới kế thừa từ WoolEffectBase
- Đăng ký hiệu ứng với WoolEffectManager
- Cấu hình thông qua Inspector

### 2. Tùy Chỉnh Giao Diện
- Thay đổi màu sắc trong ColorPalleteData
- Điều chỉnh kích thước và tỷ lệ trong prefab
- Tùy chỉnh animation thông qua AnimationController

### 3. Mở Rộng Chức Năng
- Thêm loại tương tác mới bằng cách mở rộng IWoolInteractable
- Tích hợp hệ thống tính điểm hoặc nhiệm vụ
- Thêm chế độ chơi đặc biệt với các quy tắc riêng

### 4. Tích Hợp Với Hệ Thống Khác
- Kết nối với hệ thống lưu trữ để lưu trạng thái
- Tích hợp với hệ thống quảng cáo và mua hàng trong ứng dụng
- Kết nối với hệ thống xếp hạng và thành tích

## Gỡ Lỗi Và Xử Lý Lỗi

### 1. Các Lỗi Thường Gặp
- **Wool không xoay**: Kiểm tra collider và layer
- **Màu sắc không thay đổi**: Xác nhận ColorPalleteData được gán đúng
- **Hiệu suất thấp**: Kiểm tra số lượng đỉnh và draw call

### 2. Công C� Gỡ Lỗi
- Sử dụng Debug.Log để theo dõi trạng thái
- Sử dụng Profiler để phân tích hiệu suất
- Kiểm tra console để phát hiện cảnh báo và lỗi

### 3. Tối Ưu Hóa
- Giảm số lượng đỉnh trong mesh
- Sử dụng LOD cho các đối tượng ở xa
- Tối ưu hóa vật liệu và shader

## Kết Luận

Hệ thống Wool được thiết kế để dễ dàng mở rộng và tùy chỉnh, đồng thời đảm bảo hiệu suất tốt trên nhiều thiết bị khác nhau. Với kiến trúc module, bạn có thể dễ dàng thêm các tính năng mới hoặc điều chỉnh hành vi hiện có mà không ảnh hưởng đến các phần khác của hệ thống.

### 1. Thêm Hiệu Ứng Mới
1. Tạo script mới kế thừa từ `WoolEffect`
2. Đăng ký với hệ thống
3. Áp dụng cho đối tượng wool

### 2. Tùy Chỉnh Trong Editor
- Sử dụng `[ExecuteInEditMode]` để xem trước thay đổi
- Tạo custom editor cho dễ sử dụng

## Xử Lý Lỗi Thường Gặp

### 1. Màu Không Hiển Thị Đúng
- Kiểm tra `ColorPalleteData`
- Xác nhận tên màu tồn tại

### 2. Lỗi Va Chạm
- Kiểm tra `BoxCollider`
- Xác nhận layer va chạm

## Tài Liệu Tham Khảo
- [Unity Manual: MaterialPropertyBlock](https://docs.unity3d.com/ScriptReference/MaterialPropertyBlock.html)
- [Unity Manual: Object Pooling](https://learn.unity.com/tutorial/introduction-to-object-pooling)

---
*Tài liệu được cập nhật lần cuối: 08/07/2025*
