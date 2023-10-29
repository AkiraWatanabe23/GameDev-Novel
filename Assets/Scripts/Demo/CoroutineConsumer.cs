using System.Threading;
using UnityEngine;

public class CoroutineConsumer : MonoBehaviour
{
    [SerializeField]
    private CoroutineProvider _provider = default;

    private readonly CancellationTokenSource _ctsA = new();
    private readonly CancellationTokenSource _ctsB = new();

    private readonly CancelledData _cancelledA = new();
    private readonly CancelledData _cancelledB = new();

    private void Start()
    {
        _provider.Run("処理1", _ctsA.Token, _cancelledA);
        _provider.Run("処理2", _ctsB.Token, _cancelledB);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { _ctsA.Cancel(); }
        if (Input.GetKeyDown(KeyCode.B)) { _ctsB.Cancel(); }
    }

    private void OnDestroy()
    {
        _ctsA.Dispose();
        _ctsB.Dispose();
    }
}

public class CancelledData
{
    /// <summary> 終了時の時間 </summary>
    private float _cancelledTime = 0f;
    /// <summary> 動的なCancel命令が入ったか </summary>
    private bool _isCancelOrder = false;

    /// <summary> Cancel命令が行われたかどうか </summary>
    public bool IsGetCancelOrder => _isCancelOrder;

    public void SetResultData(float time)
    {
        _cancelledTime = time;
        _isCancelOrder = true;

        Debug.Log($"CancelTime={_cancelledTime}");
    }
}
