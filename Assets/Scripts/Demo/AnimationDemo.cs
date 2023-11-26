using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationDemo : MonoBehaviour
{
    [SerializeField]
    private int _count = 5;
    [SerializeField]
    private float _duration = 1f;

    [SerializeField]
    private Color _from = Color.red;
    [SerializeField]
    private Color _to = Color.blue;

    private void Start()
    {
        StartCoroutine(RunAsync());
    }

    private IEnumerator RunAsync()
    {
        var images = CreateImages(_count);
        foreach (var image in images)
        {
            //yield return ColorChangeAsync(image, _from, _to);
            //yield return ShakeAsync(image.transform, _duration);

            var coroutine1 = StartCoroutine(ColorChangeAsync(image, _from, _to));
            var coroutine2 = StartCoroutine(ShakeAsync(image.transform, _duration));

            yield return coroutine1;
            yield return coroutine2;
        }

        yield return null;
    }

    private Image[] CreateImages(int count)
    {
        var images = new Image[count];
        for (int i = 0; i < _count; i++)
        {
            var obj = new GameObject($"Image ({i})");
            obj.transform.parent = transform;

            images[i] = obj.AddComponent<Image>();
            images[i].color = _from;
        }

        return images;
    }

    private IEnumerator ColorChangeAsync(Image image, Color from, Color to)
    {
        for (float timer = 0f; timer < _duration; timer += Time.deltaTime)
        {
            image.color = Color.Lerp(from, to, timer / _duration);
            yield return null;
        }
    }

    private IEnumerator ShakeAsync(Transform target, float duration)
    {
        yield return RotateAsync(target, Vector3.zero, new Vector3(0, 0, 20), duration * 0.25f);
        yield return RotateAsync(target, new Vector3(0, 0, 20), new Vector3(0, 0, -20), duration * 0.5f);
        yield return RotateAsync(target, new Vector3(0, 0, -20), Vector3.zero, duration * 0.25f);
    }

    private IEnumerator RotateAsync(Transform target, Vector3 from, Vector3 to, float duration)
    {
        for (var t = 0F; t < duration; t += Time.deltaTime)
        {
            target.eulerAngles = Vector3.Lerp(from, to, t / duration);
            yield return null;
        }
    }

    private IEnumerator FadeIn(Image image)
    {
        float alpha = 1f;
        Color color = image.color;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / _duration;

            if (alpha <= 0f) { alpha = 0f; }

            color.a = alpha;
            image.color = color;
            yield return null;
        }
        Debug.Log("finish FadeIn");
    }

    private IEnumerator FadeOut(Image image)
    {
        float alpha = 0f;
        Color color = image.color;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / _duration;

            if (alpha >= 1f) { alpha = 1f; }

            color.a = alpha;
            image.color = color;
            yield return null;
        }
        Debug.Log("finish FadeOut");
    }
}
