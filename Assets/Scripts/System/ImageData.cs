using System;
using UnityEngine;

[Serializable]
public class ImageData
{
    [SerializeField]
    private Character _character = Character.None;
    [SerializeField]
    private Sprite[] _sprites = default;

    public Character Character => _character;
    public Sprite[] Sprites => _sprites;
}

public enum Character
{
    None,
    Kohaku,
    Misaki,
    Yuko
}
