using System.Collections;
using TMPro;
using UnityEngine;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textUI = default;
    [SerializeField]
    private float _speed = 1f;

    private bool _isSkipRequest = false;

    /// <summary> 文字出力中かどうか。 </summary>
    public bool IsPrinting { get; private set; }

    /// <summary> 指定のメッセージを表示する </summary>
    /// <param name="message"> テキストとして表示するメッセージ </param>
    public void ShowMessage(string message)
    {
        if (message is null) { return; }
        StartCoroutine(ShowMessageCoroutine(message));
    }

    private IEnumerator ShowMessageCoroutine(string message)
    {
        if (_textUI is null || message is null) { yield break; }

        _isSkipRequest = false;
        _textUI.text = "";
        IsPrinting = true;

        int index = -1;
        var interval = _speed / message.Length;
        while (index + 1 < message.Length && !_isSkipRequest)
        {
            index++;
            _textUI.text += message[index];
            yield return new WaitForSeconds(interval);
        }

        _textUI.text = message;
        IsPrinting = false;
    }

    /// <summary> 現在再生中の文字出力を省略する </summary>
    public void Skip()
    {
        _isSkipRequest = true;
    }
}