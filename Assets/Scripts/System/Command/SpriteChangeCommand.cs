using NovelSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChangeCommand : SystemBase
{
    public override void Initialize() => CommandAction.OnSpriteChange += SpriteChange;

    public override void OnDestroy() => CommandAction.OnSpriteChange -= SpriteChange;

    private IEnumerator SpriteChange(ActorController actor, Image target, FacialExpression next)
    {
        var sprite = next switch
        {
            FacialExpression.Normal => actor.Normal,
            FacialExpression.ClosedEyes => actor.ClosedEyes,
            FacialExpression.Surprised => actor.Surprised,
            FacialExpression.Angry => actor.Angry,
            FacialExpression.Happy => actor.Happy,
            _ => null
        };
        target.sprite = sprite;

        yield return null;
    }
}
