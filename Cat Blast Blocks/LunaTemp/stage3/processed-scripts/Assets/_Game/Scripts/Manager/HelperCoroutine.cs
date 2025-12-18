
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperCoroutine
{
    private static Dictionary<float, WaitForSeconds> _waitForSeconds = new Dictionary<float, WaitForSeconds>();
    private const int MAX_CACHE_SIZE = 120;

    public static WaitForSeconds WaitSeconds(float seconds)
    {
        if (!_waitForSeconds.TryGetValue(seconds, out var wait))
        {
            wait = new WaitForSeconds(seconds);
            _waitForSeconds[seconds] = wait;

            // Prevent memory leak by limiting cache size
            if (_waitForSeconds.Count > MAX_CACHE_SIZE)
            {
                _waitForSeconds.Clear();
            }
        }
        return wait;
    }

    /// <summary>
    /// Clear the cached WaitForSeconds objects to free memory
    /// </summary>
    public static void ClearCache()
    {
        _waitForSeconds.Clear();
    }

    public static void DelaySeconds(this MonoBehaviour mono, float seconds, Action callback)
    {
        if (mono == null || callback == null || !mono.gameObject.activeInHierarchy)
            return; // Don't start coroutine if GameObject is inactive

        mono.StartCoroutine(DelayCall(seconds, callback));
    }
    public static void DelayFrames(this MonoBehaviour mono, int frames, Action callback)
    {
        if (mono == null || callback == null || !mono.gameObject.activeInHierarchy)
            return; // Don't start coroutine if GameObject is inactive

        mono.StartCoroutine(DelayCall(frames, callback));
    }

    public static void RunOnSeconds(this MonoBehaviour mono, float seconds, Action callback)
    {
        if (mono == null || callback == null || !mono.gameObject.activeInHierarchy)
            return; // Don't start coroutine if GameObject is inactive

        mono.StartCoroutine(RunCall(seconds, callback));
    }

    public static void RunOnFrames(this MonoBehaviour mono, int frames, Action callback)
    {
        if (mono == null || callback == null || !mono.gameObject.activeInHierarchy)
            return; // Don't start coroutine if GameObject is inactive

        mono.StartCoroutine(RunCall(frames, callback));
    }


    private static IEnumerator DelayCall(float seconds, Action callback)
    {
        yield return WaitSeconds(seconds);
        callback.Invoke();
    }

    private static IEnumerator DelayCall(int frames, Action callback)
    {
        for (int i = 0; i < frames; i++)
            yield return null;
        callback.Invoke();
    }

    private static IEnumerator RunCall(float seconds, Action callback)
    {
        var time = 0f;
        while (time < seconds)
        {
            time += Time.deltaTime;
            callback.Invoke();
            yield return null;
        }
    }

    private static IEnumerator RunCall(int frames, Action callback)
    {
        for (int i = 0; i < frames; i++)
        {
            callback.Invoke();
            yield return null;
        }
    }
}
