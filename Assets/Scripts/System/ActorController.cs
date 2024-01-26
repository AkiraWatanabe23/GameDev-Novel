using UnityEngine;
using UnityEngine.UI;

public class ActorController : MonoBehaviour
{
    [field: SerializeField]
    public Image Image1 { get; private set; }
    [field: SerializeField]
    public Image Image2 { get; private set; }

    [Tooltip("表情差分のSprite")]
    [field: SerializeField]
    public Sprite Normal { get; private set; }
    [field: SerializeField]
    public Sprite ClosedEyes { get; private set; }
    [field: SerializeField]
    public Sprite Surprised { get; private set; }
    [field: SerializeField]
    public Sprite Angry { get; private set; }
    [field: SerializeField]
    public Sprite Happy { get; private set; }
}
