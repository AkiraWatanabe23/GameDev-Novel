using NovelSystem;
using System;
using TMPro;
using UnityEngine;

[Serializable]
public class CommandsData
{
    [field: SerializeField]
    public TMP_Text MessengerText { get; private set; }
    [field: SerializeField]
    public TMP_Text MessageText { get; private set; }

    private SystemBase[] _systems = default;

    public CommandAction CommandAction { get; private set; }

    public void Initialize(CommandsData commandsData, params SystemBase[] systems)
    {
        CommandAction = new();

        _systems = systems;
        for (int i = 0; i < _systems.Length; i++)
        {
            systems[i].CommandsData = commandsData;
            systems[i].CommandAction = CommandAction;
            _systems[i].Initialize();
        }
    }

    public void OnDestroy()
    {
        foreach (var system in _systems) { system.OnDestroy(); }
    }
}
