using System.Collections;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textToUse;
    private Coroutine FadingIn;

    public void FadeInText()
    {
        FadingIn = StartCoroutine(FadeInTextCoroutine(textToUse));
    }

    public void FadeOutText()
    {
        StopCoroutine(FadingIn);
        StartCoroutine(FadeOutTextCoroutine(textToUse));
    }

    private IEnumerator FadeInTextCoroutine(TextMeshProUGUI[] text)
    {
        foreach (var textToFade in text)
        {
            while (textToFade.color.a < 1.0f)
            {
                textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, textToFade.color.a + (Time.deltaTime * 2f));
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    private IEnumerator FadeOutTextCoroutine(TextMeshProUGUI[] text)
    {
        foreach (var textToFade in text)
        {
            while (textToFade.color.a > 0.0f)
            {
                textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, textToFade.color.a - (Time.deltaTime * 10f));
                yield return null;
            }
        }
    }
}
