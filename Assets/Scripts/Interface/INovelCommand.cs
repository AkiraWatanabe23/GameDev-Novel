using System.Collections;

namespace NovelSystem
{
    /// <summary> 次の会話に入ったタイミングでどのような動きをするか </summary>
    public interface INovelCommand
    {
        public IEnumerator RegisterCoroutine();
    }
}
