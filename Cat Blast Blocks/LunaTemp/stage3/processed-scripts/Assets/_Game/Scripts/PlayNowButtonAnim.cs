using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayNowButtonAnim : MonoBehaviour
{
    [SerializeField] private Button playerNowButton;
    [SerializeField] private Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private Vector3 minScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float scaleDuration = 0.5f;
    [SerializeField] private bool m_autoStart = true;

    private void Start()
    {
        playerNowButton.onClick.AddListener(GotoStore);
        StartScalingAnimation();
    }

    private void OnDestroy()
    {
        playerNowButton.onClick.RemoveListener(GotoStore);
    }

    public void GotoStore()
    {
        Luna.Unity.LifeCycle.GameEnded();
        Luna.Unity.Playable.InstallFullGame();
    }

    private void StartScalingAnimation()
    {
        if (!m_autoStart) return;
        playerNowButton.transform.DOScale(maxScale, scaleDuration)
           .SetEase(Ease.InOutSine)
           .OnComplete(() =>
            {
                playerNowButton.transform.DOScale(minScale, scaleDuration)
                   .SetEase(Ease.InOutSine)
                   .OnComplete(StartScalingAnimation);
            });
    }
}
