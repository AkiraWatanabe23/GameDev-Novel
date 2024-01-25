namespace NovelSystem
{
    /// <summary> 実際の処理を記述するクラスが継承する基底クラス </summary>
    public abstract class SystemBase
    {
        public CommandsData CommandsData { get; set; }
        public CommandAction CommandAction { get; set; }

        public virtual void Initialize() { }

        public virtual void OnDestroy() { }
    }
}
