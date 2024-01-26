using NovelSystem;
using System.Collections.Generic;

public class SkipCommand : SystemBase
{
    public void Skip(List<SystemBase> systems)
    {
        foreach (var system in systems) { system.Skip(); }
    }
}
