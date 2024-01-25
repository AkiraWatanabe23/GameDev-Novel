using NovelSystem;
using System;
using UnityEngine;

[Serializable]
public class TalkBlock
{
    [SubclassSelector]
    [SerializeReference]
    [SerializeField]
    private INovelCommand[] _commands = default;

    public INovelCommand[] Commands => _commands;
}
