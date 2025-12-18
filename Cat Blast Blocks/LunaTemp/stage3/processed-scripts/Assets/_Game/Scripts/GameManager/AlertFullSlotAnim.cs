using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AlertFullSlotAnim : MonoBehaviour
{
    [SerializeField] private Image[] alertImages;
    [SerializeField] private float animDuration = 0.5f;
    private float timeDelayShow;
    private Sequence[] cachedSequences; // Cache sequences to avoid creating new ones each time

    private void Awake()
    {
        // Pre-initialize cached sequences to avoid allocation during runtime
        if (alertImages != null && alertImages.Length > 0)
        {
            cachedSequences = new Sequence[alertImages.Length];
        }
    }

    public void PlayAlertAnim()
    {
        if (Time.time - timeDelayShow < animDuration * 2)
        {
            return;
        }
        timeDelayShow = Time.time;

        for (int i = 0; i < alertImages.Length; i++)
        {
            Image img = alertImages[i];

            // Kill any existing animation on this image to prevent conflicts
            if (cachedSequences[i] != null)
            {
                cachedSequences[i].Kill();
            }

            // Create and cache the sequence
            cachedSequences[i] = DOTween.Sequence();
            cachedSequences[i].Append(img.DOFade(1f, animDuration * 0.5f));
            cachedSequences[i].Append(img.DOFade(0f, animDuration * 0.5f));
            cachedSequences[i].Append(img.DOFade(1f, animDuration * 0.5f));
            cachedSequences[i].Append(img.DOFade(0f, animDuration * 0.5f));
        }
    }

    // Clean up sequences when the object is destroyed to prevent memory leaks
    private void OnDestroy()
    {
        if (cachedSequences != null)
        {
            for (int i = 0; i < cachedSequences.Length; i++)
            {
                if (cachedSequences[i] != null)
                {
                    cachedSequences[i].Kill();
                    cachedSequences[i] = null;
                }
            }
        }
    }
}
