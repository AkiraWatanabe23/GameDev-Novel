using System.Collections;
using UnityEngine;

public class MessageSequencer : MonoBehaviour
{
    [SerializeField]
    private MessagePrinter _printer = default;
    [SerializeField]
    private string[] _messages = default;

    private void Start()
    {
        StartCoroutine(InputCoroutine());
    }

    private IEnumerator InputCoroutine()
    {
        if (_messages is null or { Length: 0 }) { yield break; }

        var index = 0;
        while (index < _messages.Length)
        {
            if (_printer.IsPrinting) { _printer.Skip(); }
            else { _printer?.ShowMessage(_messages[index++]); }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return null;
        }
    }

    private IEnumerator WaitSecOrInput(float second)
    {
        for (float timer = 0f; timer < second; timer += Time.deltaTime)
        {
            if (Input.GetMouseButtonDown(0)) { yield break; }

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
