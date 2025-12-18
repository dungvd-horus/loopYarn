using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndGameXmas : UiEndGame
{
    [SerializeField] private Image m_xmasHeaderImage;
    [SerializeField] private Sprite m_xmasWinSprite;
    [SerializeField] private Sprite m_xmasLoseSprite;
    [SerializeField] private Image m_xmasFooterImage;
    [SerializeField] private Sprite[] m_xmasFooterSprite;

    protected override void Start()
    {
        base.Start();
        m_endText.enabled = false;
        m_endText1.enabled = false;
        m_buttonText.enabled = false;
        m_buttonText1.enabled = false;
    }
    [ContextMenu("Show Win 1")]
    public override void ShowEndGameWin()
    {
        m_xmasHeaderImage.sprite = m_xmasWinSprite;
        m_xmasFooterImage.sprite = m_xmasFooterSprite[0];
        SoundManager.Instance.PlayOneShot(m_endGameSound[0]);
        FadeInEndGame();
    }
    [ContextMenu("Show Lose 1")]
    public override void ShowEndGameLose()
    {
        m_xmasHeaderImage.sprite = m_xmasLoseSprite;
        m_xmasFooterImage.sprite = m_xmasFooterSprite[1];

        SoundManager.Instance.PlayOneShot(m_endGameSound[1]);
        FadeInEndGame();
    }
}
