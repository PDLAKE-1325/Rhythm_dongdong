using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    public int currentLevel { get; private set; }
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }
}
