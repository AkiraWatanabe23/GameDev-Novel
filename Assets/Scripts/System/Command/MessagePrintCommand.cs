using Constants;
using NovelSystem;
using System.Collections;
using TMPro;
using UnityEngine;

public class MessagePrintCommand : SystemBase
{
    private TMP_Text _characterNameText = default;
    private TMP_Text _messageText = default;

    public override void Initialize()
    {
        _characterNameText = CommandsData.MessengerText;
        _messageText = CommandsData.MessageText;

        CommandAction.OnMessagePrint += PrintMessage;
    }

    public override void OnDestroy()
    {
        CommandAction.OnMessagePrint -= PrintMessage;
    }

    private IEnumerator PrintMessage(string messenger, string message, float showSpeed = 1f)
    {
        if (_messageText == null) { Consts.LogError("表示用のTextの割り当てがありません"); yield break; }

        _characterNameText.text = messenger;
        _messageText.text = "";

        int index = -1;
        var interval = showSpeed / message.Length;

        while (index + 1 < message.Length)
        {
            index++;
            _messageText.text += message[index];

            float timer = 0f;
            while (timer < interval) { timer += Time.deltaTime; yield return null; }
        }

        _messageText.text = message;
        yield return null;
    }
}
