using UnityEngine;

public class CullableObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Renderer[] targetRenderers;
    [SerializeField] private float checkInterval = 0.5f;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private bool showDebug = false;
    
    private Camera cam;
    private bool isVisible = true;
    private bool[] originalStates;
    private float timer = 0f;
    
    void Start()
    {
        // Tự động lấy renderer
        if (targetRenderers == null || targetRenderers.Length == 0)
            targetRenderers = GetComponentsInChildren<Renderer>(true);
        
        if (targetRenderers.Length == 0)
        {
            Debug.LogWarning($"{name}: Không tìm thấy Renderer!");
            enabled = false;
            return;
        }
        
        // Lưu trạng thái ban đầu
        originalStates = new bool[targetRenderers.Length];
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            if (targetRenderers[i] != null)
                originalStates[i] = targetRenderers[i].enabled;
        }
        
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning($"{name}: Không tìm thấy Camera!");
            enabled = false;
        }
        
        if (showDebug) Debug.Log($"{name}: Khởi tạo với {targetRenderers.Length} renderer(s)");
    }
    
    void Update()
    {
        if (cam == null) return;
        
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            
            bool shouldVisible = CheckVisibility();
            
            if (shouldVisible != isVisible)
            {
                isVisible = shouldVisible;
                SetRenderers(isVisible);
                
                if (showDebug) 
                    Debug.Log($"{name}: {(isVisible ? "VISIBLE" : "INVISIBLE")}");
            }
        }
    }
    
    bool CheckVisibility()
    {
        Vector3 camPos = cam.transform.position;
        
        // Kiểm tra khoảng cách
        float dist = Vector3.Distance(transform.position, camPos);
        if (dist > maxDistance)
            return false;
        
        // Kiểm tra từng renderer
        foreach (Renderer r in targetRenderers)
        {
            if (r == null) continue;
            
            // Chuyển bounds center sang viewport space
            Vector3 viewportPoint = cam.WorldToViewportPoint(r.bounds.center);
            
            // Kiểm tra có trong viewport không (x: 0-1, y: 0-1, z: > 0)
            if (viewportPoint.z > 0 && 
                viewportPoint.x >= -0.1f && viewportPoint.x <= 1.1f && 
                viewportPoint.y >= -0.1f && viewportPoint.y <= 1.1f)
            {
                // Thêm buffer 0.1 để tránh flickering ở edge
                return true;
            }
        }
        
        return false;
    }
    
    void SetRenderers(bool enable)
    {
        for (int i = 0; i < targetRenderers.Length; i++)
        {
            if (targetRenderers[i] != null)
                targetRenderers[i].enabled = enable ? originalStates[i] : false;
        }
    }
    
    // Unity callbacks - backup method
    void OnBecameVisible()
    {
        if (!isVisible)
        {
            isVisible = true;
            SetRenderers(true);
            if (showDebug) Debug.Log($"{name}: OnBecameVisible");
        }
    }
    
    void OnBecameInvisible()
    {
        if (isVisible)
        {
            isVisible = false;
            SetRenderers(false);
            if (showDebug) Debug.Log($"{name}: OnBecameInvisible");
        }
    }
    
    // Public API
    public void ForceEnable() 
    { 
        isVisible = true; 
        SetRenderers(true); 
    }
    
    public void ForceDisable() 
    { 
        isVisible = false; 
        SetRenderers(false); 
    }
    
    public bool IsVisible() => isVisible;
    
    public void SetMaxDistance(float distance) => maxDistance = distance;
    
    // Debug Gizmos
    void OnDrawGizmosSelected()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) return;
        
        Gizmos.color = isVisible ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, cam.transform.position);
    }
}