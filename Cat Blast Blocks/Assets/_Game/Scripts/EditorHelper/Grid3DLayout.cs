using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3DLayout : MonoBehaviour
{
    public int columns = 3;
    public float spacing = 2f;

    [ContextMenu("Arrange Children")]
    void ArrangeChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int row = i / columns;
            int col = i % columns;
            transform.GetChild(i).localPosition = new Vector3(col * spacing, -row * spacing, 0f);
        }
    }
}

