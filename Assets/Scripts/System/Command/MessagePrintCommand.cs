using Constants;
using NovelSystem;
using System.Collections;
using TMPro;
using UnityEngine;

public class MessagePrintCommand : INovelCommand
{
    [SerializeField]
    private TMP_Text _characterNameText = default;
    [SerializeField]
    private TMP_Text _messageText = default;

    //メッセージを出力中かどうか
    private bool _isPrinting = false;
    private string _currentCharacter = "";
    private string _currentMessage = "";

    public IEnumerator Coroutine => PrintMessage();

    private IEnumerator PrintMessage(float showSpeed = 1f)
    {
        if (_messageText == null) { Consts.LogError("表示用のTextの割り当てがありません"); yield break; }

        _characterNameText.text = _currentCharacter;
        _messageText.text = "";
        _isPrinting = true;

        var message = _currentMessage;

        int index = -1;
        var interval = showSpeed / message.Length;
        var waitSec = new WaitForSeconds(interval);

        while (index + 1 < message.Length)
        {
            index++;
            _messageText.text += message[index];
            yield return waitSec;
        }

        _messageText.text = message;
        _isPrinting = false;
    }
}
