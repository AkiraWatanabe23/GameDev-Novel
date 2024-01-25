using NovelSystem;
using UnityEngine;
using UnityEngine.UI;

public class Fade : INovelCommand
{
    [field: SerializeField]
    public FadeType FadeType { get; private set; } = FadeType.None;
    [field: SerializeField]
    public Image Target {  get; private set; }
}
