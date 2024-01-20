using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class MessageBlock
{
    public MessageData[] MessageData;
}

public class NovelSystemController : MonoBehaviour
{
    [Tooltip("会話データを記入したスプレッドシートのURL")]
    [SerializeField]
    private string _talkSheetPath = "";
    [SerializeField]
    private CommandsData _commandsData = default;
    [SerializeField]
    private TalkBlock _talkBlock = default;

    private string _downloadText = "";
    /// <summary> 会話セット内の実行中のインデックス </summary>
    private int _currentIndex = -1;
    /// <summary> 実行中のIEnumratorを格納するList </summary>
    private List<IEnumerator> _enumerators = default;

    public MessageBlock MessageBlock { get; private set; }

    private IEnumerator Start()
    {
        yield return Initialize();
        _commandsData.Initialize();
        Load();

        StartCoroutine(InputReceiveCoroutine());
    }

    private IEnumerator Initialize()
    {
        if (_talkBlock == null) { Consts.LogWarning("データの割り当てがありません"); yield break; }

        _currentIndex = -1;
        _enumerators = new();

        yield return null;

        Consts.Log("Finish Initialized");
    }

    private async void Load() => MessageBlock = await SheetLoad<MessageBlock>(_talkSheetPath);

    /// <summary> webURLを指定してスプレッドシートを読み込み、jsonファイルに変換する </summary>
    public async Task<T> SheetLoad<T>(string requestURL)
    {
        T loadData = default;
        if (await TryGetText(requestURL)) { loadData = JsonUtility.FromJson<T>(_downloadText); }

        return loadData;
    }

    /// <summary> WebRequestを送信し、テキストデータを取得する </summary>
    private async Task<bool> TryGetText(string requestURL)
    {
        UnityWebRequest www = UnityWebRequest.Get(requestURL);
        var operation = www.SendWebRequest();

        Consts.Log("loading...");

        //終わるまで待機
        while (!operation.isDone) { await Task.Delay(100); }

        if (www.result == UnityWebRequest.Result.Success)
        {
            _downloadText = www.downloadHandler.text;

            // 結果をテキストとして表示します
            Consts.Log("Load Success!");
            return true;
        }
        else { Consts.LogError($"Load Failed : {www.error}"); return false; }
    }

    private IEnumerator InputReceiveCoroutine()
    {
        //全て実行し終わったら終了
        if (_currentIndex >= _talkBlock.TalkCommands.Length) { yield break; }

        //実行する内容がある間回し続ける
        while (_currentIndex < _talkBlock.TalkCommands.Length)
        {
            Run();
            //入力待機
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return null;
        }
    }

    public void AddCoroutines(params IEnumerator[] enumerators)
    {
        if (enumerators == null) { return; }
        foreach (var enumerator in enumerators) { _enumerators.Add(enumerator); }
    }

    /// <summary> 登録したデータの実行処理 </summary>
    private IEnumerator RunningCoroutines()
    {
        if (_enumerators.Count == 0) { yield break; }

        for (int i = 0; i < _enumerators.Count; i++)
        {
            if (_enumerators[i] != null && !_enumerators[i].MoveNext()) { _enumerators.RemoveAt(i); }
        }
    }

    private void Run()
    {
        if (_talkBlock == null) { Consts.LogError("データの割り当てがありません"); return; }
        if (_currentIndex + 1 >= _talkBlock.TalkCommands.Length) { Consts.Log("全て終了しました"); return; }

        _currentIndex++;
        AddCoroutines(_talkBlock.TalkCommands[_currentIndex].Coroutines);
    }
}
