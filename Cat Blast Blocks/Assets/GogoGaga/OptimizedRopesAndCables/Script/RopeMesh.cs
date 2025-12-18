using System.Collections.Generic;
using UnityEngine;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GogoGaga.OptimizedRopesAndCables
{
    [RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(Rope))]
    public class RopeMesh : MonoBehaviour
    {
        [Range(3, 25)] public int OverallDivision = 6;
        [Range(0.01f, 10)] public float ropeWidth = 0.3f;
        [Range(3, 20)] public int radialDivision = 8;
        [Tooltip("For now only base color is applied")]
        public Material material;
        [Tooltip("Tiling density per meter of the rope")]
        public float tilingPerMeter = 1.0f;

        private Rope rope;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private Mesh ropeMesh;
        private bool isStartOrEndPointMissing;

        // Use fields to store lists
        private List<Vector3> vertices = new List<Vector3>();
        private List<int> triangles = new List<int>();
        private List<Vector2> uvs = new List<Vector2>();

        [Header("ROPE TEXTURE EQUALIZER")]
        public Rope RopeLogic;
        public Transform StartPoint;
        public Transform EndPoint;
        public Color FirstHalfColor;
        public Color SecondHalfColor;
        public float ThresholdDistance = 1.0f;
        public float ThresholdOffsetY = -0.5f;
        public int UpdateRate = 5;
        private int frameCounter; // default 0

        // Cache shader property IDs to avoid string lookups
        private static readonly int ColorID = Shader.PropertyToID("_Color");
        private static readonly int TwoHalfColor1ID = Shader.PropertyToID("_TwoHalfColorsColor1");
        private static readonly int TwoHalfColor2ID = Shader.PropertyToID("_TwoHalfColorsColor2");
        private static readonly int TwoHalfMaskSTID = Shader.PropertyToID("_TwoHalfColorsMaskTex_ST");
        // Additional common color property IDs (URP/HDRP/custom)
        private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
        private static readonly int TintColorID = Shader.PropertyToID("_TintColor");
        private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
        private static readonly int MainColorID = Shader.PropertyToID("_MainColor");
        private static readonly int TintID = Shader.PropertyToID("_Tint");

        [Header("Debug / Color Application")] public bool EnableDebugColorLogs;
        [Tooltip("Tùy chọn: tên property màu chính của shader (ví dụ _BaseColor, _Color, _TintColor...). Nếu điền đúng, sẽ ưu tiên dùng property này.")]
        public string OverrideColorProperty;
        private int overrideColorID = -1;
        private string lastColorPropertyUsed;

        [Header("Performance Optimization")]
        [Tooltip("If true, always rebuild mesh each frame (debug). If false, rebuild only when endpoints or rope settings change.")] public bool RebuildOnEveryFrame = false;
        [Tooltip("Minimum seconds between automatic rebuilds when settings move rapidly.")][Range(0f, 0.25f)] public float MinSecondsBetweenRebuilds = 0.02f;
        [Tooltip("Distance threshold to consider endpoints moved enough to rebuild.")] public float RebuildMovementThreshold = 0.0005f;
        [Tooltip("Whether to recalculate normals each rebuild (disable if using flat shader for perf). ")] public bool RecalculateNormals = true;

        private Vector3 lastStartPointPos;
        private Vector3 lastEndPointPos;
        private int lastOverallDivision;
        private int lastRadialDivision;
        private float lastRopeWidth;
        private float lastTilingPerMeter;
        private float lastRebuildTime;
        private bool firstRuntimeFrame = true;
        private Vector3[] points;
        //         public void OnValidate()
        //         {
        //             InitializeComponents(assignMeshToFilter: false);
        //             if (rope != null && rope.IsPrefab)
        //                 return;

        //             SubscribeToRopeEvents();

        //             if (meshRenderer && material)
        //             {
        //                 // Use sharedMaterial in editor to avoid instancing and extra side effects
        //                 meshRenderer.sharedMaterial = material;
        //             }

        //             CacheOverrideColorID();

        // #if UNITY_EDITOR
        //             if (overrideColorID == -1)
        //             {
        //                 AutoDetectOverrideColorProperty();
        //                 CacheOverrideColorID();
        //             }
        //             // Defer any mesh assignment/build out of OnValidate
        //             EditorApplication.delayCall += DelayedGenerateMesh;
        // #endif
        //         }
        private void Awake()
        {
            InitializeComponents(assignMeshToFilter: false);
            SubscribeToRopeEvents();
            CacheOverrideColorID();
        }

        private void OnEnable()
        {
            if (!Application.isPlaying)
            {
#if UNITY_EDITOR
                EditorApplication.delayCall += DelayedGenerateMesh;
#endif
            }
            SubscribeToRopeEvents();
            ReOffsetTextureTwoHalf();
        }

        private void OnDisable()
        {
            UnsubscribeFromRopeEvents();
#if UNITY_EDITOR
            EditorApplication.delayCall -= DelayedGenerateMesh;
#endif
        }

        private void InitializeComponents(bool assignMeshToFilter = true)
        {
            if (!rope) rope = GetComponent<Rope>();
            if (!meshFilter) meshFilter = GetComponent<MeshFilter>();
            if (!meshRenderer) meshRenderer = GetComponent<MeshRenderer>();

            if (ropeMesh == null)
            {
                ropeMesh = new Mesh { name = "RopeMesh" };
                ropeMesh.MarkDynamic();
            }

            // Only assign to MeshFilter when it's safe (not from OnValidate)
            if (assignMeshToFilter && meshFilter && meshFilter.sharedMesh != ropeMesh)
            {
                meshFilter.sharedMesh = ropeMesh;
            }

            CheckEndPoints();
        }

        private void CheckEndPoints()
        {
            // // Check if start and end points are assigned
            // if (gameObject.scene.rootCount == 0)
            // {
            //     isStartOrEndPointMissing = false;
            //     return;
            // }

            if (rope.StartPoint == null || rope.EndPoint == null)
            {
                isStartOrEndPointMissing = true;
                Debug.LogError("StartPoint or EndPoint is not assigned.", gameObject);
            }
            else
            {
                isStartOrEndPointMissing = false;
            }
        }

        private void SubscribeToRopeEvents()
        {
            UnsubscribeFromRopeEvents();
            if (rope != null)
            {
                rope.OnPointsChanged += GenerateMesh;
            }
        }

        private void UnsubscribeFromRopeEvents()
        {
            if (rope != null)
            {
                rope.OnPointsChanged -= GenerateMesh;
            }
        }

        public void CreateRopeMesh(Vector3[] points, float radius, int segmentsPerWire)
        {
            if (points == null || points.Length < 2)
            {
                Debug.LogError("Need at least two points to create a rope mesh.", gameObject);
                return;
            }

            if (ropeMesh == null)
            {
                ropeMesh = new Mesh { name = "RopeMesh" };
                ropeMesh.MarkDynamic();
            }

            // Safe to assign here (not during OnValidate)
            if (meshFilter && meshFilter.sharedMesh != ropeMesh)
            {
                meshFilter.sharedMesh = ropeMesh;
            }

            ropeMesh.Clear();

            // Reserve capacity (approx) to reduce list reallocations
            int rings = points.Length;
            int vertsPerRing = segmentsPerWire + 1;
            int estimatedVerts = rings * vertsPerRing + (segmentsPerWire + 2) * 2; // + caps
            if (vertices.Capacity < estimatedVerts) vertices.Capacity = estimatedVerts;
            if (uvs.Capacity < estimatedVerts) uvs.Capacity = estimatedVerts;
            int estimatedTriangles = (rings - 1) * segmentsPerWire * 6 + segmentsPerWire * 3 * 2; // body + caps
            if (triangles.Capacity < estimatedTriangles) triangles.Capacity = estimatedTriangles;

            // Clear the lists before using them
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();

            float currentLength = 0f;

            // Generate vertices and UVs for each segment along the points
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 direction = i < points.Length - 1 ? points[i + 1] - points[i] : points[i] - points[i - 1];
                // Guard against zero-length direction which causes Quaternion.LookRotation to throw
                if (direction.sqrMagnitude < 1e-6f)
                {
                    // Try fallback to previous segment
                    if (i > 0)
                        direction = points[i] - points[i - 1];
                }
                if (direction.sqrMagnitude < 1e-6f)
                {
                    // Try fallback to next segment
                    if (i < points.Length - 1)
                        direction = points[i + 1] - points[i];
                }
                if (direction.sqrMagnitude < 1e-6f)
                {
                    // Ultimate fallback: a stable forward vector
                    direction = Vector3.forward;
                }
                Quaternion rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

                // Create vertices around a circle at this point
                for (int j = 0; j <= segmentsPerWire; j++)
                {
                    float angle = j * Mathf.PI * 2f / segmentsPerWire;
                    Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                    vertices.Add(transform.InverseTransformPoint(points[i] + rotation * offset));

                    float u = (float)j / segmentsPerWire;
                    float v = currentLength * tilingPerMeter;
                    uvs.Add(new Vector2(u, v));
                }

                if (i < points.Length - 1)
                {
                    currentLength += Vector3.Distance(points[i], points[i + 1]);
                }
            }

            // Generate triangles for each segment
            for (int i = 0; i < points.Length - 1; i++)
            {
                for (int j = 0; j < segmentsPerWire; j++)
                {
                    int current = i * (segmentsPerWire + 1) + j;
                    int next = current + 1;
                    int nextSegment = current + segmentsPerWire + 1;
                    int nextSegmentNext = nextSegment + 1;

                    triangles.Add(current);
                    triangles.Add(next);
                    triangles.Add(nextSegment);

                    triangles.Add(next);
                    triangles.Add(nextSegmentNext);
                    triangles.Add(nextSegment);
                }
            }

            // Generate vertices, triangles, and UVs for the start cap
            int startCapCenterIndex = vertices.Count;
            vertices.Add(transform.InverseTransformPoint(points[0]));
            uvs.Add(new Vector2(0.5f, 0)); // Center of the cap
            // Guard start cap rotation against zero-length vector
            Vector3 startDir = (points.Length > 1) ? (points[1] - points[0]) : Vector3.forward;
            if (startDir.sqrMagnitude < 1e-6f) startDir = Vector3.forward;
            Quaternion startRotation = Quaternion.LookRotation(startDir.normalized, Vector3.up);
            for (int j = 0; j <= segmentsPerWire; j++)
            {
                float angle = j * Mathf.PI * 2f / segmentsPerWire;
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                vertices.Add(transform.InverseTransformPoint(points[0] + startRotation * offset));

                if (j < segmentsPerWire)
                {
                    triangles.Add(startCapCenterIndex);
                    triangles.Add(startCapCenterIndex + j + 1);
                    triangles.Add(startCapCenterIndex + j + 2);
                }

                uvs.Add(new Vector2((Mathf.Cos(angle) + 1) / 2, (Mathf.Sin(angle) + 1) / 2));
            }

            // Generate vertices, triangles, and UVs for the end cap
            int endCapCenterIndex = vertices.Count;
            vertices.Add(transform.InverseTransformPoint(points[points.Length - 1]));
            uvs.Add(new Vector2(0.5f, currentLength * tilingPerMeter)); // Center of the cap
            // Guard end cap rotation against zero-length vector
            Vector3 endDir = (points.Length > 1) ? (points[points.Length - 1] - points[points.Length - 2]) : Vector3.forward;
            if (endDir.sqrMagnitude < 1e-6f) endDir = Vector3.forward;
            Quaternion endRotation = Quaternion.LookRotation(endDir.normalized, Vector3.up);
            for (int j = 0; j <= segmentsPerWire; j++)
            {
                float angle = j * Mathf.PI * 2f / segmentsPerWire;
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                vertices.Add(transform.InverseTransformPoint(points[points.Length - 1] + endRotation * offset));

                if (j < segmentsPerWire)
                {
                    triangles.Add(endCapCenterIndex);
                    triangles.Add(endCapCenterIndex + j + 1);
                    triangles.Add(endCapCenterIndex + j + 2);
                }

                uvs.Add(new Vector2((Mathf.Cos(angle) + 1) / 2, (Mathf.Sin(angle) + 1) / 2));
            }

            ropeMesh.SetVertices(vertices);
            ropeMesh.SetTriangles(triangles, 0, true);
            ropeMesh.SetUVs(0, uvs);
            if (RecalculateNormals)
                ropeMesh.RecalculateNormals();
        }

        void GenerateMesh()
        {
            if (this == null || rope == null || meshFilter == null)
            {
                return;
            }

            if (isStartOrEndPointMissing)
            {
                if (meshFilter.sharedMesh != null)
                {
                    meshFilter.sharedMesh.Clear();
                }
                return;
            }
            if (points == null || points.Length != OverallDivision + 1)
            {
                points = new Vector3[OverallDivision + 1];
            }

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = rope.GetPointAt(i / (float)OverallDivision);
            }
            CreateRopeMesh(points, ropeWidth, radialDivision);
        }

        void Update()
        {
            if (rope.IsPrefab)
                return;

            if (!Application.isPlaying)
            {
                // In editor (not playing) keep previous behaviour for immediate feedback
                GenerateMesh();
                return;
            }

            bool needRebuild = RebuildOnEveryFrame;

            // Detect endpoint movement
            if (!isStartOrEndPointMissing && rope.StartPoint && rope.EndPoint)
            {
                var sp = rope.StartPoint.position;
                var ep = rope.EndPoint.position;
                if (firstRuntimeFrame || (Vector3.SqrMagnitude(sp - lastStartPointPos) > RebuildMovementThreshold) || (Vector3.SqrMagnitude(ep - lastEndPointPos) > RebuildMovementThreshold))
                {
                    needRebuild = true;
                }
            }

            // Detect parameter changes
            if (OverallDivision != lastOverallDivision || radialDivision != lastRadialDivision || !Mathf.Approximately(ropeWidth, lastRopeWidth) || !Mathf.Approximately(tilingPerMeter, lastTilingPerMeter))
            {
                needRebuild = true;
            }

            // Throttle rebuilds
            if (needRebuild)
            {
                float t = Time.time;
                if ((t - lastRebuildTime) >= MinSecondsBetweenRebuilds || firstRuntimeFrame)
                {
                    GenerateMesh();
                    lastRebuildTime = t;
                    firstRuntimeFrame = false;
                    if (rope.StartPoint) lastStartPointPos = rope.StartPoint.position;
                    if (rope.EndPoint) lastEndPointPos = rope.EndPoint.position;
                    lastOverallDivision = OverallDivision;
                    lastRadialDivision = radialDivision;
                    lastRopeWidth = ropeWidth;
                    lastTilingPerMeter = tilingPerMeter;
                }
            }

            if (frameCounter > UpdateRate)
            {
                ReOffsetTextureTwoHalf();
                frameCounter = 0;
            }
            frameCounter++;
        }

        private void DelayedGenerateMesh()
        {
            if (this != null)
            {
                GenerateMesh();
            }
        }

        private void OnDestroy()
        {
            UnsubscribeFromRopeEvents();
#if UNITY_EDITOR
            EditorApplication.delayCall -= DelayedGenerateMesh;
#endif

            if (meshRenderer != null)
                Destroy(meshRenderer);
            if (meshFilter != null)
                Destroy(meshFilter);
        }

        #region rope texture re-offset
        private void CacheOverrideColorID()
        {
            if (!string.IsNullOrEmpty(OverrideColorProperty))
                overrideColorID = Shader.PropertyToID(OverrideColorProperty);
            else
                overrideColorID = -1;
        }

        private bool TrySetSingleColor(Material mat, Color color)
        {
            if (mat == null) return false;
            // 1) Manual override
            if (overrideColorID != -1 && mat.HasProperty(overrideColorID))
            {
                mat.SetColor(overrideColorID, color);
                lastColorPropertyUsed = OverrideColorProperty;
                return true;
            }
            // 2) Common properties (order of preference)
            if (mat.HasProperty(BaseColorID)) { mat.SetColor(BaseColorID, color); lastColorPropertyUsed = "_BaseColor"; return true; }
            if (mat.HasProperty(ColorID)) { mat.SetColor(ColorID, color); lastColorPropertyUsed = "_Color"; return true; }
            if (mat.HasProperty(MainColorID)) { mat.SetColor(MainColorID, color); lastColorPropertyUsed = "_MainColor"; return true; }
            if (mat.HasProperty(TintColorID)) { mat.SetColor(TintColorID, color); lastColorPropertyUsed = "_TintColor"; return true; }
            if (mat.HasProperty(TintID)) { mat.SetColor(TintID, color); lastColorPropertyUsed = "_Tint"; return true; }
            if (mat.HasProperty(EmissionColorID)) { mat.SetColor(EmissionColorID, color); lastColorPropertyUsed = "_EmissionColor"; mat.EnableKeyword("_EMISSION"); return true; }
            return false;
        }

        public void SetColor(Color _color)
        {
            if (meshRenderer != null)
            {
                var mat = meshRenderer.material; // instance material
                if (TrySetSingleColor(mat, _color))
                {
                    if (EnableDebugColorLogs)
                        Debug.Log($"[RopeMesh] Applied color {_color} using property '{lastColorPropertyUsed}' on material '{mat.name}'.", this);
                }
                else if (EnableDebugColorLogs)
                {
                    Debug.LogWarning("[RopeMesh] Không tìm thấy property màu phù hợp. Hãy set OverrideColorProperty hoặc dùng menu 'Log Material Color Props' để kiểm tra.", this);
                }
            }
            else if (EnableDebugColorLogs)
            {
                Debug.LogWarning("[RopeMesh] meshRenderer null, không set được màu.", this);
            }
        }

        public void SetColor(Color _firstColor, Color _secondColor)
        {
            FirstHalfColor = _firstColor;
            SecondHalfColor = _secondColor;
            if (meshRenderer != null)
            {
                var mat = meshRenderer.material;
                bool anyApplied = false;
                if (mat.HasProperty(TwoHalfColor1ID)) { mat.SetColor(TwoHalfColor1ID, FirstHalfColor); anyApplied = true; }
                if (mat.HasProperty(TwoHalfColor2ID)) { mat.SetColor(TwoHalfColor2ID, SecondHalfColor); anyApplied = true; }

                if (!anyApplied)
                {
                    // Fallback: if special two-half properties don't exist, just blend single color (average) or first color
                    Color fallback = Color.Lerp(FirstHalfColor, SecondHalfColor, 0.5f);
                    if (TrySetSingleColor(mat, fallback))
                    {
                        if (EnableDebugColorLogs)
                            Debug.Log($"[RopeMesh] TwoHalf properties missing; dùng màu trộn {fallback} cho property '{lastColorPropertyUsed}'.", this);
                    }
                    else if (EnableDebugColorLogs)
                    {
                        Debug.LogWarning("[RopeMesh] Không có TwoHalf properties và cũng không có property màu cơ bản trong material để fallback.", this);
                    }
                }
                else if (EnableDebugColorLogs)
                {
                    Debug.Log($"[RopeMesh] Applied two-half colors {FirstHalfColor} / {SecondHalfColor}.", this);
                }
            }
            else if (EnableDebugColorLogs)
            {
                Debug.LogWarning("[RopeMesh] meshRenderer null, không set được hai màu.", this);
            }
        }

        [ContextMenu("Test Random Color")]
        private void TestRandomColor()
        {
            SetColor(new Color(Random.value, Random.value, Random.value));
        }

        public void ReOffsetTextureTwoHalf()
        {
            if (rope == null || rope.EndPoint == null || rope.StartPoint == null || meshRenderer == null)
                return;

            var mat = meshRenderer.material;
            if (mat == null)
                return;

            float currentDist = Vector3.Distance(rope.StartPoint.position, rope.EndPoint.position);
            float distRatio = ThresholdDistance > 0.0001f ? (currentDist / ThresholdDistance) : 0f;
            float rsOffsetY = distRatio * ThresholdOffsetY;

            if (mat.HasProperty(TwoHalfMaskSTID))
            {
                mat.SetVector(TwoHalfMaskSTID, new Vector4(1f, 1f, 0f, rsOffsetY));
            }
            else if (EnableDebugColorLogs)
            {
                Debug.LogWarning("[RopeMesh] Material không có property _TwoHalfColorsMaskTex_ST để offset.", this);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Log Material Color Props")]
        private void LogMaterialColorProps()
        {
            if (meshRenderer == null)
            {
                Debug.LogWarning("[RopeMesh] meshRenderer null", this);
                return;
            }
            var mat = meshRenderer.sharedMaterial;
            if (mat == null)
            {
                Debug.LogWarning("[RopeMesh] Không có material trên MeshRenderer.", this);
                return;
            }
            var shader = mat.shader;
            if (shader == null)
            {
                Debug.LogWarning("[RopeMesh] Material không có shader.", this);
                return;
            }

            int count = ShaderUtil.GetPropertyCount(shader);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"[RopeMesh] Shader: {shader.name}, Material: {mat.name}");
            sb.AppendLine("- Candidate color properties:");
            for (int i = 0; i < count; i++)
            {
                var type = ShaderUtil.GetPropertyType(shader, i);
                string propName = ShaderUtil.GetPropertyName(shader, i);
                if (type == ShaderUtil.ShaderPropertyType.Color || type == ShaderUtil.ShaderPropertyType.Vector)
                {
                    sb.AppendLine($"  • {propName} ({type})");
                }
                else if (type == ShaderUtil.ShaderPropertyType.TexEnv && propName.EndsWith("_ST"))
                {
                    sb.AppendLine($"  • {propName} (Tex ST)");
                }
            }
            Debug.Log(sb.ToString(), this);
        }

        [ContextMenu("Auto-Detect Color Property (Editor)")]
        private void AutoDetectOverrideColorProperty()
        {
            if (meshRenderer == null) return;
            var mat = meshRenderer.sharedMaterial;
            if (mat == null) return;
            var shader = mat.shader;
            if (shader == null) return;
            int count = ShaderUtil.GetPropertyCount(shader);
            for (int i = 0; i < count; i++)
            {
                var type = ShaderUtil.GetPropertyType(shader, i);
                if (type == ShaderUtil.ShaderPropertyType.Color || type == ShaderUtil.ShaderPropertyType.Vector)
                {
                    OverrideColorProperty = ShaderUtil.GetPropertyName(shader, i);
                    if (EnableDebugColorLogs)
                        Debug.Log($"[RopeMesh] Auto-detected OverrideColorProperty = {OverrideColorProperty}", this);
                    return;
                }
            }
        }
#endif

        [ContextMenu("Green")]
        public void SetColorGreen()
        {
            SetColor(Color.green);
        }
        [ContextMenu("Red")]
        public void SetColorRed()
        {
            SetColor(Color.red);
        }
        #endregion
    }
}

