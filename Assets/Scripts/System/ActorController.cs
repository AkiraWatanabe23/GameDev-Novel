using UnityEngine;
using UnityEngine.UI;

public class ActorController : MonoBehaviour
{
    [SerializeField]
    private Image _actorImage = default;

    public Image ActorImage { get => _actorImage; set => _actorImage = value; }
}
