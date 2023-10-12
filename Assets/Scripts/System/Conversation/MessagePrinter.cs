using TMPro;
using UnityEngine;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textUI = default;
    [SerializeField]
    private string _message = "";
    [SerializeField]
    private float _speed = 1f;

    private float _elapsed = 0f; // 文字を表示してからの経過時間
    private float _interval = 0f; // 文字毎の待ち時間

    // _message フィールドから表示する現在の文字インデックス
    // 何も指していない場合は -1 とする
    private int _currentIndex = -1;

    /// <summary> 文字出力中かどうか。 </summary>
    public bool IsPrinting
    {
        get
        {
            // TODO: ここにコードを書く
            //（がテキストを表示中であれば true、そうでなければ false）
            return _currentIndex != _message.Length - 1;
        }
    }

    private void Start()
    {
        ShowMessage(_message);
    }

    private void Update()
    {
        if (_textUI is null || _message is null || _currentIndex + 1 >= _message.Length) { return; }

        _elapsed += Time.deltaTime;
        if (_elapsed > _interval)
        {
            _elapsed = 0;
            _currentIndex++;
            _textUI.text += _message[_currentIndex];
        }
    }

    /// <summary> 指定のメッセージを表示する </summary>
    /// <param name="message"> テキストとして表示するメッセージ </param>
    public void ShowMessage(string message)
    {
        // TODO: ここにコードを書く
        if (_textUI is null) { return; }

        //次のテキストアニメーションのために一度リセットを行う
        _textUI.text = "";
        _interval = _speed / _message.Length;
        _message = message;
        _currentIndex = -1;
    }

    /// <summary> 現在再生中の文字出力を省略する </summary>
    public void Skip()
    {
        // TODO: ここにコードを書く
        //強制的にメッセージを全て表示する
        _currentIndex = _message.Length - 1;
        _textUI.text = _message;
    }
}