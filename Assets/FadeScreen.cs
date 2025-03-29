using UnityEngine;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart= true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart){
            FadeIn();
        }
    }

        public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    private IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;

        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);
            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha value
        Color finalColor = fadeColor;
        finalColor.a = alphaOut;
        rend.material.SetColor("_Color", finalColor);
    }

}
