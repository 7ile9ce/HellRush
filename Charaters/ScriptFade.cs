using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        
    }

    public void FadeOut()
    {
        StartCoroutine(FadeToBlack());
    }
public void FadeIn()
    {
        StartCoroutine(FadeFromBlack());
    }
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color c = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
    }
    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;
        Color c = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            fadeImage.color = c;
            yield return null;
        }
    }
}
