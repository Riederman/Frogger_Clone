using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelManagerMB : Singleton<LevelManagerMB>
{
    [SerializeField]
    private GameObject levelPrefab;

    private LevelMB currentLevel;

    private List<LevelMB> levels = new List<LevelMB>();

    private void Awake()
    {
        ProceedToNextLevel();
    }

    private LevelMB GenerateLevel()
    {
        LevelMB level = Instantiate(levelPrefab).GetComponent<LevelMB>();
        Assert.IsTrue(level != null);
        level.index = levels.Count;
        return level;
    }

    public void ProceedToNextLevel()
    {
        if (currentLevel.index + 1 <= GameConstants.MAX_NUM_LEVELS)
        {
            currentLevel.gameObject.SetActive(false);
            currentLevel = GenerateLevel();
            levels.Add(currentLevel);
        }
    }

    public void ReturnToPrevLevel()
    {
        if (currentLevel.index - 1 >= 0)
        {
            currentLevel.gameObject.SetActive(false);
            currentLevel = levels[currentLevel.index - 1];
            currentLevel.gameObject.SetActive(true);
        }
    }
}