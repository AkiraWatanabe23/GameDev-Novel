using NovelSystem;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChange : INovelCommand
{
    [field: SerializeField]
    public ActorController Actor { get; private set; }
    [field: SerializeField]
    public Image Target { get; private set; }
    [field: SerializeField]
    public FacialExpression FacialExpression { get; private set; } = FacialExpression.Normal;
}

public enum FacialExpression
{
    Normal,
    ClosedEyes,
    Surprised,
    Angry,
    Happy
}
