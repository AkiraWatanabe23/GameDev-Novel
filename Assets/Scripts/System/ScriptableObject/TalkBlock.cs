using NovelSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkData", menuName = "ScriptableObjects/Create TalkBlockData")]
public class TalkBlock : ScriptableObject
{
    [SerializeReference]
    [SubclassSelector]
    [SerializeField]
    private INovelCommand[] _commands = default;

    public INovelCommand[] Commands => _commands;
}
