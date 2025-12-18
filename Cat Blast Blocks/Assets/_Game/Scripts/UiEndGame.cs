using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;
public class UiEndGame : Singleton<UiEndGame>
{

    [SerializeField] private CanvasGroup m_endGameCanvas;
    [SerializeField] private CanvasGroup m_headerCanvas;
    [SerializeField] private CanvasGroup m_levelTransitionOverlay;
    [SerializeField] protected Text m_endText;
    [SerializeField] protected Text m_endText1;
    [SerializeField] protected Text m_buttonText;
    [SerializeField] protected Text m_buttonText1;
    [SerializeField] protected AudioClip[] m_endGameSound;
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI m_loadingText; // Text hiển thị "Loading..."

    private Coroutine m_loadingCoroutine;
    protected virtual void Start()
    {
        m_endGameCanvas.alpha = 0f;
        m_endGameCanvas.gameObject.SetActive(false);
        m_headerCanvas.alpha = 1f;
        m_headerCanvas.gameObject.SetActive(true);
    }
    [ContextMenu("Show Win")]
    public virtual void ShowEndGameWin()
    {
        m_endText.text = "You Win!".ToUpper();
        m_buttonText.text = "Play Now".ToUpper();
        m_endText1.text = "You Win!".ToUpper();
        m_buttonText1.text = "Play Now".ToUpper();
        SoundManager.Instance.PlayOneShot(m_endGameSound[0]);
        FadeInEndGame();
    }
    [ContextMenu("Show Lose")]
    public virtual void ShowEndGameLose()
    {
        m_endText.text = "You Lose!".ToUpper();
        m_buttonText.text = "Play Again".ToUpper();
        m_endText1.text = "You Lose!".ToUpper();
        m_buttonText1.text = "Play Again".ToUpper();
        FadeInEndGame();
        SoundManager.Instance.PlayOneShot(m_endGameSound[1]);
    }

    public void StartLevelTransition(System.Action onComplete = null, float delayBeforeFadeOut = 0.25f)
    {
        if (m_levelTransitionOverlay != null)
        {
            GameplayEventsManager.OnBlockGamePlayInput?.Invoke(true);
            m_levelTransitionOverlay.DOKill(true);
            m_levelTransitionOverlay.alpha = 0f;
            m_levelTransitionOverlay.gameObject.SetActive(true);

            // Bắt đầu animation loading dots
            StartLoadingDots();

            // Fade In overlay
            m_levelTransitionOverlay
                .DOFade(1f, 1f)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();

                    // Delay trước fade out
                    m_levelTransitionOverlay.alpha = 1f;
                    m_levelTransitionOverlay
                        .DOFade(0f, 1f)
                        .SetDelay(delayBeforeFadeOut)
                        .OnComplete(() =>
                        {
                            m_levelTransitionOverlay.gameObject.SetActive(false);
                            StopLoadingDots();
                            GameplayEventsManager.OnBlockGamePlayInput?.Invoke(false);
                            // Dừng animation dots
                        });
                });
        }
        else
        {
            onComplete?.Invoke();
        }
    }




    public void FadeInEndGame()
    {
        m_endGameCanvas.gameObject.SetActive(true);
        m_headerCanvas.gameObject.SetActive(true);
        m_endGameCanvas.DOFade(1f, 1f);
        m_headerCanvas.DOFade(0f, 1f).OnComplete(() =>
        {
            m_headerCanvas.gameObject.SetActive(false);
        });
    }

    private void StartLoadingDots()
    {
        if (m_loadingCoroutine != null)
            StopCoroutine(m_loadingCoroutine);

        m_loadingCoroutine = StartCoroutine(LoadingDotsCoroutine());
    }

    private void StopLoadingDots()
    {
        if (m_loadingCoroutine != null)
        {
            StopCoroutine(m_loadingCoroutine);
            m_loadingCoroutine = null;
        }
    }

    private IEnumerator LoadingDotsCoroutine()
    {
        int dotCount = 0;
        while (true)
        {
            dotCount = (dotCount % 3) + 1; // 1 → 2 → 3 → 1
            m_loadingText.text = "Loading" + new string('.', dotCount);
            yield return new WaitForSeconds(0.25f); // 0.5 giây đổi 1 lần
        }
    }


}

