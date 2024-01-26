using Constants;
using NovelSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeCommand : SystemBase
{
    public override void Initialize()
    {
        CommandAction.OnFadeIn += FadeIn;
        CommandAction.OnFadeOut += FadeOut;
        CommandAction.OnFadeColor += FadeColor;
    }

    public override void OnDestroy()
    {
        CommandAction.OnFadeIn -= FadeIn;
        CommandAction.OnFadeOut -= FadeOut;
        CommandAction.OnFadeColor -= FadeColor;
    }

    public override void OnComplete(Image target, FadeType fadeType)
    {
        Color color = target.color;
        if (fadeType == FadeType.FadeIn) { color.a = 0f; Consts.Log("Finish FadeIn"); }
        else if (fadeType == FadeType.FadeOut) { color.a = 1f; Consts.Log("Finish FadeOut"); }

        target.color = color;
    }

    private IEnumerator FadeIn(Image target, float duration = 1f)
    {
        if (!target.gameObject.activeSelf) { target.gameObject.SetActive(true); }

        //α値(透明度)を 1 -> 0 にする(少しずつ明るくする)
        float alpha = 1f;
        Color color = target.color;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / duration;

            if (alpha <= 0f) { alpha = 0f; }

            color.a = alpha;
            target.color = color;
            yield return null;
        }
        OnComplete(target, FadeType.FadeIn);
    }

    private IEnumerator FadeOut(Image target, float duration = 1f)
    {
        if (!target.gameObject.activeSelf) { target.gameObject.SetActive(true); }

        //α値(透明度)を 0 -> 1 にする(少しずつ暗くする)
        float alpha = 0f;
        Color color = target.color;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / duration;

            if (alpha >= 1f) { alpha = 1f; }

            color.a = alpha;
            target.color = color;
            yield return null;
        }
        OnComplete(target, FadeType.FadeOut);
    }

    private IEnumerator FadeColor(Image target, Color from, Color to, float duration = 1f)
    {
        for (float timer = 0f; timer < duration; timer += Time.deltaTime)
        {
            target.color = Color.Lerp(from, to, timer / duration);
            yield return null;
        }
    }
}

public enum FadeType
{
    None,
    FadeIn,
    FadeOut,
    FadeColor,
}
