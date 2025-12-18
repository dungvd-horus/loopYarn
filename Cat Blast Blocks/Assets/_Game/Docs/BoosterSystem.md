# Phân Tích Hệ Thống Booster

## Mục Lục
1. [Tổng Quan](#tổng-quan)
2. [Kiến Trúc Hệ Thống](#kiến-trúc-hệ-thống)
3. [Chi Tiết Các Loại Booster](#chi-tiết-các-loại-booster)
   - [Broom Booster](#1-broom-booster-chổi-quét)
   - [Rainbow Box Booster](#2-rainbow-box-booster-hộp-cầu-vồng)
   - [AddHold Booster](#3-addhold-booster-khoan-thêm-lỗ)
4. [Luồng Hoạt Động](#luồng-hoạt-động)
5. [Tích Hợp Với Các Hệ Thống Khác](#tích-hợp-với-các-hệ-thống-khác)
6. [Tối Ưu Hóa](#tối-ưu-hóa)
7. [Mở Rộng](#mở-rộng)

## Tổng Quan

Hệ thống booster trong game được thiết kế để hỗ trợ người chơi vượt qua các thử thách khó khăn. Mỗi booster có chức năng riêng biệt và có thể được mua thêm bằng vàng hoặc xem quảng cáo.

## Điều Kiện Sử Dụng Và Khóa

### 1. Điều Kiện Chung

#### 1.1. Mở Khóa Booster
- Mỗi booster có ngưỡng level mở khóa riêng, được lấy từ Firebase:
  ```csharp
  // Broom Booster
  _levelOpenBroomBooster = FirebaseManager.Instance.GetRemoteLevelBroomTutValue();
  
  // AddHold Booster
  _levelOpenAddHoldBooster = FirebaseManager.Instance.GetRemoteLevelAddholdTutValue();
  
  // Rainbow Box Booster
  _levelOpenRainBowBooster = FirebaseManager.Instance.GetRemoteLevelRainbowTutValue();
  ```

#### 1.2. Kiểm Tra Trạng Thái Khóa
```csharp
public override void ChangeLockStateBooster()
{
    var currentLevel = DataManager.PlayerInfoData.Level;
    boosterLockGO.SetActive(currentLevel < _levelOpenBooster);
    boosterGO.SetActive(currentLevel >= _levelOpenBooster);
    var levelI2 = TextContentDisplay.GetI2("level");
    boosterLockText.text = string.Format(levelI2, _levelOpenBooster);
}
```

### 2. Điều Kiện Riêng Từng Loại

| Booster | Mở Khóa | Số Lượng | Điều Kiện Đặc Biệt |
|---------|---------|----------|-------------------|
| **Broom** | Level từ Firebase | > 0 | Không có |
| **Rainbow Box** | Level từ Firebase | > 0 | Chưa sử dụng trong lượt này |
| **AddHold** | Level từ Firebase | > 0 | Chưa đạt tối đa 7 ô |

### 3. Xử Lý Khi Không Đủ Điều Kiện
- **Chưa đủ level**: Hiển thị thông báo level yêu cầu
- **Hết số lượng**: Tự động mở popup mua thêm
- **Không thể sử dụng**: Hiển thị lý do cụ thể

### 4. Luồng Kiểm Tra Điều Kiện
1. Khi bắt đầu game: Load cấu hình và cập nhật trạng thái
2. Khi nhấn vào booster: Kiểm tra điều kiện sử dụng
3. Khi thay đổi level: Cập nhật lại trạng thái khóa/mở

## Kiến Trúc Hệ Thống

### Lớp Cơ Sở: `BaseBooster`

```csharp
public abstract class BaseBooster : MonoBehaviour
{
    // Thuộc tính chung
    public BoosterDataSO BoosterData;
    public Button button;
    
    // Các phương thức chính
    public abstract void ChangeLockStateBooster();
    public abstract void OnButtonClick();
    public abstract void ExecuteBooster();
    public abstract void BuyBooster();
    
    // Các phương thức bảo vệ
    protected abstract void OnBuyBoosterByGold();
    protected abstract void OnBuyBoosterByAds();
}
```

## Chi Tiết Các Loại Booster

### 1. Broom Booster (Chổi Quét)

#### Tác Dụng
- Xóa toàn bộ các mục tiêu đang chờ trong hàng đợi
- Giúp người chơi thoát khỏi tình huống bí

#### Thông Số
- **Lưu Trữ**: `DataManager.PlayerInfoData.BroomAmount`
- **Mở Khóa**: Level từ Firebase (`GetRemoteLevelBroomTutValue`)

#### Code Chính
```csharp
protected async UniTask Execute()
{
    button.interactable = false;
    var broom = GenericObjectPool.Instance.PopFromPool(BroomPrefab, instantiateIfNone: true);
    broom.transform.position = new Vector3(-2, 3f, -6.3f);
    GameEventManager.OnUseSaveBooster?.Invoke();
    DataManager.ChangeBroom(-1);
    await GamePlayManager.Instance.UseBroomBoosterCleanUpQueue();
    button.interactable = true;
}
```

### 2. Rainbow Box Booster (Hộp Cầu Vồng)

#### Tác Dụng
- Tạo ra một hộp đặc biệt có thể kết hợp với bất kỳ màu nào
- Giúp tạo nước đi khi không còn lựa chọn nào khả thi

#### Thông Số
- **Lưu Trữ**: `DataManager.PlayerInfoData.RainbowBoxAmount`
- **Mở Khóa**: Level cao hơn Broom Booster

#### Code Chính
```csharp
public override void ExecuteBooster()
{
    GameEventManager.OnRainbowBoxBoosterComplete?.Invoke();
    button.interactable = false;
    GameEventManager.OnUseSaveBooster?.Invoke();
    GamePlayManager.Instance.AddRainBowBox();
    DataManager.ChangeRainbowBox(-1);
    button.interactable = true;
    effectClick.SetInteractable(true);
    TrackingUseItem();
}
```

### 3. AddHold Booster (Khoan Thêm Lỗ)

#### Tác Dụng
- Tăng thêm 1 ô chứa trong hàng đợi mục tiêu
- Giới hạn tối đa: 7 ô

#### Thông Số
- **Lưu Trữ**: `DataManager.PlayerInfoData.HoleAmount`
- **Mở Khóa**: Level từ Firebase (`GetRemoteLevelAddholdTutValue`)

#### Code Chính
```csharp
protected IEnumerator AddHold()
{
    button.interactable = false;
    GameEventManager.OnUseSaveBooster?.Invoke();
    GamePlayManager.Instance.AddQueueTarget();
    DataManager.ChangeHole(-1);
    yield return CoroutineYielders.Get(1.5f);
    button.interactable = true;
}
```

## Luồng Hoạt Động

1. **Kích Hoạt Booster**:
   - Kiểm tra điều kiện sử dụng
   - Trừ số lượng booster
   - Thực hiện hiệu ứng
   - Cập nhật giao diện

2. **Mua Thêm Booster**:
   - Kiểm tra đủ điều kiện mua (vàng/ads)
   - Trừ tài nguyên hoặc hiển thị quảng cáo
   - Cộng thêm số lượng
   - Lưu dữ liệu

## Tích Hợp Với Các Hệ Thống Khác

### 1. GamePlayManager
- Quản lý trạng thái booster
- Xử lý logic khi kích hoạt

### 2. DataManager
- Lưu trữ số lượng booster
- Cập nhật dữ liệu khi sử dụng/mua

### 3. Hệ Thống Sự Kiện
- **On[BoomerName]DataChange**: Khi số lượng thay đổi
- **OnUseSaveBooster**: Khi sử dụng booster
- **On[BoosterName]BoosterComplete**: Khi kích hoạt thành công

## Tối Ưu Hóa

1. **Object Pooling**: Sử dụng `GenericObjectPool` cho hiệu ứng
2. **Bất Đồng Bộ**: Sử dụng `UniTask` cho các tác vụ
3. **Sự Kiện**: Giảm phụ thuộc giữa các thành phần

## Mở Rộng

Để thêm booster mới:
1. Tạo class kế thừa `BaseBooster`
2. Triển khai các phương thức trừu tượng
3. Thêm dữ liệu vào `BoosterDataSO`
4. Tích hợp với giao diện người dùng

## So Sánh Các Loại Booster

| Đặc Điểm | Broom Booster | Rainbow Box | AddHold |
|----------|--------------|-------------|---------|
| **Mục Đích** | Xóa hàng đợi | Tạo ô đa năng | Tăng ô chứa |
| **Giới Hạn** | Không | Có | Tối đa 7 ô |
| **Độ Hiếm** | Thấp | Cao | Trung bình |
| **Tác Động** | Xóa nhiều | Thêm 1 ô đa năng | Thêm 1 ô thường |

## Kết Luận

Hệ thống booster được thiết kế linh hoạt, dễ dàng mở rộng và tích hợp với các hệ thống khác. Mỗi booster có vai trò riêng, giúp người chơi có nhiều lựa chọn chiến thuật khi gặp khó khăn.
