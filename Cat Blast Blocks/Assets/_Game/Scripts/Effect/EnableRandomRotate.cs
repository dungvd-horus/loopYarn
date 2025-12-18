using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRandomRotate : MonoBehaviour
{
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 360f;
    public Transform Target;

    private void OnEnable()
    {
        RandomRotate();
    }

    public void RandomRotate()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 current = Target.eulerAngles;
        Target.eulerAngles = new Vector3(current.x, randomY, current.z);
    }
}
