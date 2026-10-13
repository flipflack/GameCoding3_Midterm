using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIFadeSequences : MonoBehaviour
{   
    [Header("Scene Fade In References")]
    public Image fadeOverlay;
    public float fadeDuration;
    public float delayDuration;

    [Header("Title Fade In References")]
    public TextMeshProUGUI titleText;
    public float initialDelay;
    public float fadeInDuration;
    public float holdDuration;
    public float fadeOutDuration;

    void Start()
    {   
        if (!GameController.ranFadeSequence)
        {
            if (fadeOverlay != null)
            {   
                StartCoroutine(FadeIn());
                GameController.ranFadeSequence = true;
            }
        }
        else if (fadeOverlay != null)
        {
            fadeOverlay.gameObject.SetActive(false);
        }

        if (!GameController.ranTitleSequence)
        {
            if (titleText != null)
            {
                StartCoroutine(DoTitleSequence());
                GameController.ranTitleSequence = true;
            }
        }
        else if (titleText != null)
        {
            titleText.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeIn()
    {   
        yield return new WaitForSeconds(delayDuration);
        
        float elapsedTime = 0f;
        Color overlayColor = fadeOverlay.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            overlayColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeOverlay.color = overlayColor;
            yield return null;
        }
        overlayColor.a = 0f;
        fadeOverlay.color = overlayColor;
        fadeOverlay.gameObject.SetActive(false);
    }
    IEnumerator DoTitleSequence()
    {   
        yield return new WaitForSeconds(initialDelay);
        // Fade In
        Color textColor = titleText.color;
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            titleText.color = textColor;
            yield return null;
        }
        textColor.a = 1f;
        titleText.color = textColor;

        // Hold
        yield return new WaitForSeconds(holdDuration);

        // Fade Out
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            titleText.color = textColor;
            yield return null;
        }
        
        textColor.a = 0f;
        titleText.color = textColor;
        titleText.gameObject.SetActive(false);
    }
}
