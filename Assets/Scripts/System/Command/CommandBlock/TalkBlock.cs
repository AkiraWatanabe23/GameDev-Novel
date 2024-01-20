using NovelSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TalkBlock
{
    [SerializeField]
    private CommandBlock[] _talkCommands = default;

    public CommandBlock[] TalkCommands => _talkCommands;

    [Serializable]
    public class CommandBlock
    {
        [SerializeField]
        private CommandType[] _commands = default;

        public CommandType[] Commands => _commands;
        public IEnumerator[] Coroutines { get; private set; }

        public void SetUpCoroutines(Dictionary<CommandType, INovelCommand> novelCommands)
        {
            if (_commands != null) { return; }

            var enumrators = new List<IEnumerator>();

            //foreach (var commandType in novelCommands.Keys)
            //{
            //    if (commandType == CommandType.Fade) { enumrators.Add(); }
            //}

            //Array.ForEach(_commands, command => enumrators.Add(command.Coroutine));
            Coroutines = enumrators.ToArray();
        }
    }
}

public enum CommandType
{
    None,
    Fade,
    MessagePrint,
}
