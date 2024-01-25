using Constants;
using NovelSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class MessageBlock
{
    public MessageData[] MessageDatas;
}

public class NovelSystemController : MonoBehaviour
{
    [SerializeField]
    private string _jsonDirectoryPath = "";
    [SerializeField]
    private CommandsData _commandsData = new();
    [Tooltip("トーク1セット")]
    [SerializeField]
    private TalkBlock[] _talkBlock = default;

    /// <summary> 会話セット内の実行中のインデックス </summary>
    private int _currentTalkBlockIndex = -1;
    /// <summary> 実行中のIEnumratorを格納するList </summary>
    private List<IEnumerator> _enumerators = default;
    private CommandAction _commandAction = default;

    private MessageBlock _messageBlock = default;
    private int _currentMessageIndex = 0;

    private bool _isCoroutinesPlaying = false;

    private IEnumerator Start()
    {
        yield return Initialize();
        CommandsDataInitialize();
        Load();

        StartCoroutine(InputReceiveCoroutine());
    }

    private IEnumerator Initialize()
    {
        if (_talkBlock == null) { Consts.LogWarning("データの割り当てがありません"); yield break; }

        _currentTalkBlockIndex = -1;
        _enumerators = new();

        yield return null;

        Consts.Log("Finish Initialized");
    }

    private void CommandsDataInitialize()
    {
        _commandsData.Initialize(
            _commandsData,
            new FadeCommand(),
            new MessagePrintCommand());

        _commandAction = _commandsData.CommandAction;
    }

    private void Load() => _messageBlock = SheetLoad<MessageBlock>(_jsonDirectoryPath);

    public T SheetLoad<T>(string path)
    {
        if (!File.Exists(path)) { Consts.LogError("File not found"); return default; }

        var json = File.ReadAllText(path);
        T loadData = JsonUtility.FromJson<T>(json);

        return loadData;
    }

    private IEnumerator InputReceiveCoroutine()
    {
        //全て実行し終わったら終了
        if (_currentTalkBlockIndex >= _talkBlock.Length) { yield break; }

        //実行する内容がある間回し続ける
        while (_currentTalkBlockIndex < _talkBlock.Length)
        {
            yield return RunningCoroutines();
            //入力待機
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            UpdateNextCoroutines();
        }
    }

    /// <summary> 登録したデータの実行処理 </summary>
    private IEnumerator RunningCoroutines()
    {
        if (_enumerators.Count == 0) { _isCoroutinesPlaying = false; yield break; }

        Consts.Log("Running...");
        while (_enumerators.Count > 0)
        {
            for (int i = _enumerators.Count - 1; i >= 0; i--)
            {
                if (_enumerators[i] == null) { continue; }
                if (!_enumerators[i].MoveNext()) { _enumerators.RemoveAt(i); }
            }
            yield return null;
        }
    }

    private void UpdateNextCoroutines()
    {
        if (_talkBlock == null) { Consts.LogError("データの割り当てがありません"); return; }
        if (_currentTalkBlockIndex + 1 >= _talkBlock.Length) { Consts.Log("全て終了しました"); return; }
        if (_isCoroutinesPlaying) { Consts.Log("実行中です"); return; }

        _currentTalkBlockIndex++;
        //次に実行する処理の追加
        AddCoroutines(_talkBlock[_currentTalkBlockIndex]);
    }

    private void AddCoroutines(TalkBlock talkBlock)
    {
        foreach (var command in talkBlock.Commands) { GetCommand(command); }
        _isCoroutinesPlaying = true;
    }

    private void GetCommand(INovelCommand command)
    {
        IEnumerator enumerator = null;
        if (command is Fade)
        {
            var fadeCommand = (Fade)command;
            enumerator = fadeCommand.FadeType switch
            {
                FadeType.FadeIn => _commandAction.OnFadeIn?.Invoke(fadeCommand.Target, 1f),
                FadeType.FadeOut => _commandAction.OnFadeOut?.Invoke(fadeCommand.Target, 1f),
                _ => null
            };
        }
        else if (command is MessagePrint)
        {
            enumerator = _commandAction.OnMessagePrint?.Invoke(
                _messageBlock.MessageDatas[_currentMessageIndex].TalkerName, _messageBlock.MessageDatas[_currentMessageIndex].Message, 1f);

            _currentMessageIndex++;
        }
        _enumerators.Add(enumerator);
    }

    private void OnDestroy() => _commandsData.OnDestroy();
}
