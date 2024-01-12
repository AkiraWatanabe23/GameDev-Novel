using Constants;
using NovelSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeCommand : INovelCommand
{
    [SerializeField]
    private Image _target = default;
    [SerializeField]
    private FadeType _fadeType = FadeType.None;

    public IEnumerator RegisterCoroutine()
    {
        return _fadeType switch
        {
            FadeType.FadeIn => FadeIn(),
            FadeType.FadeOut => FadeOut(),
            //FadeType.FadeColor => FadeColor(),
            _ => null
        };
    }

    private IEnumerator FadeIn(float duration = 1f)
    {
        if (!_target.gameObject.activeSelf) { _target.gameObject.SetActive(true); }

        //α値(透明度)を 1 -> 0 にする(少しずつ明るくする)
        float alpha = 1f;
        Color color = _target.color;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / duration;

            if (alpha <= 0f) { alpha = 0f; }

            color.a = alpha;
            _target.color = color;
            yield return null;
        }
        Consts.Log("Finish FadeIn");
    }

    private IEnumerator FadeOut(float duration = 1f)
    {
        if (!_target.gameObject.activeSelf) { _target.gameObject.SetActive(true); }

        //α値(透明度)を 0 -> 1 にする(少しずつ暗くする)
        float alpha = 0f;
        Color color = _target.color;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / duration;

            if (alpha >= 1f) { alpha = 1f; }

            color.a = alpha;
            _target.color = color;
            yield return null;
        }
        Consts.Log("Finish FadeOut");
    }

    private IEnumerator FadeColor(Color from, Color to, float duration = 1f)
    {
        for (float timer = 0f; timer < duration; timer += Time.deltaTime)
        {
            _target.color = Color.Lerp(from, to, timer / duration);
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
