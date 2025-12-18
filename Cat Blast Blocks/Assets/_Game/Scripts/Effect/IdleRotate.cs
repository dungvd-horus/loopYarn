using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRotate : MonoBehaviour
{
    public bool Active;

    [Header("OBJECT")]
    public Transform target;

    [Header("STAT")]
    public Vector3 rotationSpeed = new Vector3(0, 90f, 0); // degrees per second
    public int frameInterval = 1; // rotate once every N frames

    private int frameCounter = 0;

    private float temporarySpeedBoost = 1;

    private void OnEnable()
    {
        frameCounter = 0;
        temporarySpeedBoost = 1;
    }

    void Update()
    {
        if (!Active) return;

        frameCounter++;

        if (frameCounter >= frameInterval)
        {
            float delta = Time.deltaTime;
            target.Rotate(rotationSpeed * delta * temporarySpeedBoost, Space.Self);

            frameCounter = 0;
        }
    }

    public void StartRotate() => Active = true;
    public void StopRotate() => Active = false;
    public void BoostSpeed(float speed) => temporarySpeedBoost = speed;
}
