using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CommandAction
{
    public Func<string, string, float, IEnumerator> OnMessagePrint;

    public Func<ActorController, Image, FacialExpression, IEnumerator> OnSpriteChange;

    public Func<Image, float, IEnumerator> OnFadeIn;
    public Func<Image, float, IEnumerator> OnFadeOut;
    public Func<Image, Color, Color, float, IEnumerator> OnFadeColor;
}
