using System.Collections;
using System.Threading;
using UnityEngine;

public class CoroutineProvider : MonoBehaviour
{
    /// <summary> 外から、何か継続的な処理の開始を指示する。 </summary>
    public Coroutine Run(string name, CancellationToken token, CancelledData cancelled)
    {
        return StartCoroutine(RunCoroutine(name, token, cancelled));
    }

    private IEnumerator RunCoroutine(string name, CancellationToken token, CancelledData cancelled)
    {
        var elapsed = 0f;

        while (!token.IsCancellationRequested && elapsed <= 5f)
        {
            elapsed += Time.deltaTime;
            // 何か継続処理...
            Debug.Log($"{name}: {Time.frameCount}");
            yield return null;
        }
        Debug.Log($"{name}が終了しました");

        cancelled.SetResultData(elapsed);
    }
}