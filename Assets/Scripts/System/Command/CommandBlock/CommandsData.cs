using NovelSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandsData : MonoBehaviour
{
    [SerializeField]
    private CommandPair[] _commandDatas = default;

    public Dictionary<CommandType, INovelCommand> NovelCommands { get; private set; }

    public void Initialize()
    {
        NovelCommands ??= new();
        foreach (var command in _commandDatas) { NovelCommands.Add(command.CommandType, command.Command); }
    }
}

[Serializable]
public class CommandPair
{
    [SerializeField]
    private CommandType _commandType = CommandType.None;
    [SubclassSelector]
    [SerializeReference]
    [SerializeField]
    private INovelCommand _command = default;

    public CommandType CommandType => _commandType;
    public INovelCommand Command => _command;
}
