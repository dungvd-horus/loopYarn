using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PaintingPixel
{
    public string name;
    public int column;
    public int row;
    public Color color;
    public string colorCode;
    public Vector3 worldPos;
    public int Hearts; // Number of hits the pixel can take before being destroyed
    public bool destroyed;
    public GameObject pixelObject; // Reference to the GameObject associated with this pixel
    public PaintingPixelComponent PixelComponent;
    public bool Hidden;
    public bool VunableToAll;
    public bool Indestructible;
    public bool IsPipePixel;
    public bool IsWallPixel;
    public bool IsFountainObjectPixel;
    public bool UsePaletteMaterialColor = true; // mới: true nếu dùng màu sẵn có của material palette

    // Thêm trường sharedMaterial
    [FormerlySerializedAs("sharedMaterial")]
    public Material Material;

    public PaintingPixel()
    {
        this.name = "PaintingPixel";
        this.column = 0;
        this.row = 0;
        this.color = Color.white;
        this.colorCode = "WhiteDefault";
        this.worldPos = Vector3.zero;
        this.Hearts = 1;
        this.destroyed = false;
        this.pixelObject = null;
        this.Hidden = false;
        this.VunableToAll = false;
        this.IsPipePixel = false;
        this.IsWallPixel = false;
        this.Indestructible = false;
        this.Material = null;
        this.UsePaletteMaterialColor = true;
        this.IsFountainObjectPixel = false;
    }

    // Constructor mới với sharedMaterial
    public PaintingPixel(int column, int row, Color color, Vector3 worldPos, int heart, bool hidden, GameObject pixelObject = null, Material material = null)
    {
        this.name = $"Pixel ({column}, {row})";
        this.column = column;
        this.row = row;
        this.color = color;
        this.colorCode = "WhiteDefault";
        this.worldPos = worldPos;
        this.Hearts = heart;
        this.destroyed = false;
        this.pixelObject = pixelObject;
        this.Hidden = hidden;
        this.VunableToAll = false;
        this.Material = material; // Gán sharedMaterial ở đây
        this.UsePaletteMaterialColor = true;
    }

    // Constructor đã sửa đổi để nhận sharedMaterial từ config hoặc fallback
    public PaintingPixel(PaintingPixelConfig config, Material initialMaterial = null)
    {
        this.name = $"Pixel ({config.column}, {config.row})";
        this.column = config.column;
        this.row = config.row;
        this.color = config.color;
        this.colorCode = config.colorCode;
        this.worldPos = Vector3.zero;
        this.Hearts = 1;
        this.destroyed = false;
        this.pixelObject = null;
        this.Hidden = config.Hidden;
        this.VunableToAll = false;
        this.Material = initialMaterial; // Gán sharedMaterial khi tạo pixel
        this.UsePaletteMaterialColor = true;
    }

    public void SetUp([Bridge.Ref] Color color, string colorCode, bool hidden)
    {
        this.color = color;
        this.Hidden = hidden;
        this.destroyed = false;
        this.colorCode = colorCode;
        this.VunableToAll = false;

        // Ensure PixelComponent is set up with the updated data
        PixelComponent?.SetUp(this);

        if (Hidden)
        {
            destroyed = true;
            pixelObject.SetActive(false);
        }
        else PixelComponent?.ApplyVisual();
    }

    public void SetPosition([Bridge.Ref] Vector3 newPos)
    {
        this.worldPos = newPos;
    }

    public void DestroyPixel(bool invokeEvent = true, ColorPixelsCollectorObject collectorObject = null)
    {
        this.destroyed = true;
        this.PixelComponent?.Destroyed();
        if (invokeEvent)
        {
            if (collectorObject != null)
            {
                GameplayEventsManager.OnAPixelDestroyedByCollector?.Invoke(this, collectorObject);
            }
            else
            {
                GameplayEventsManager.OnAPixelDestroyed?.Invoke(this);
            }
        }
    }

    public void DestroyObject()
    {
        this.destroyed = true;
        if (pixelObject != null)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(pixelObject);
            }
            else
            {
                GameObject.DestroyImmediate(pixelObject);
            }
        }
    }

    public void ShowPixelObject()
    {
        pixelObject?.SetActive(true);
        PixelComponent.ShowVisualOnly();
    }
    public bool InCount() => !destroyed && (!Hidden || IsWallPixel || IsPipePixel || IsFountainObjectPixel || Indestructible);
    public bool IsCompleteHidden() => destroyed || Hidden || IsWallPixel || IsPipePixel || IsFountainObjectPixel || Indestructible;
    public bool IsMechanicPixel()
    {
        return IsPipePixel || IsWallPixel || IsFountainObjectPixel || Indestructible;
    }
}
