using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelSystemController : MonoBehaviour
{
    [SerializeField]
    private TalkBlock _talkBlock = default;

    private int _currentIndex = 0;
    private List<IEnumerator> _enumerators = default;

    private void Awake()
    {
        if (_talkBlock == null) { Consts.LogWarning("データの割り当てがありません"); return; }
    }

    private void Run()
    {
        if (_currentIndex + 1 >= _talkBlock.Commands.Length) { Consts.Log("全て終了しました"); return; }

        _currentIndex++;
    }
}
