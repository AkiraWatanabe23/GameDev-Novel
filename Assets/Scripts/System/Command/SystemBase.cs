using System;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

namespace NovelSystem
{
    /// <summary> 実際の処理を記述するクラスが継承する基底クラス </summary>
    public abstract class SystemBase
    {
        public CommandsData CommandsData { get; set; }
        public CommandAction CommandAction { get; set; }

        public virtual void Initialize() { }

        public virtual void OnDestroy() { }

        public virtual void Skip() { }

        public virtual void OnComplete(Image target, FadeType fadeType) { }

        public virtual void OnComplete(Image target) { }

        public virtual void OnComplete(string message) { }
    }

    [Serializable]
    public class Command
    {
        public IEnumerator Coroutine { get; set; }
        public CancellationTokenSource CTS { get; set; }
    }
}
