using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelManagerMB : Singleton<LevelManagerMB>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject levelPrefab;

    private PlayerMB player;
    private LevelMB currentLevel;

    private List<LevelMB> levels = new List<LevelMB>();

    private void Awake()
    {
        GeneratePlayer();
        currentLevel = GenerateLevel();
        currentLevel.AddPlayer(player);
    }

    private void GeneratePlayer()
    {
        player = Instantiate(playerPrefab).GetComponent<PlayerMB>();
        Assert.IsTrue(player != null);
    }

    private LevelMB GenerateLevel()
    {
        LevelMB level = Instantiate(levelPrefab).GetComponent<LevelMB>();
        Assert.IsTrue(currentLevel != null);
        Assert.IsTrue(player != null);
        level.index = levels.Count;
        level.Generate();
        levels.Add(level);
        return level;
    }

    public void ProceedToNextLevel()
    {
        if (currentLevel != null && currentLevel.index + 1 <= GameConstants.MAX_NUM_LEVELS)
        {
            currentLevel.gameObject.SetActive(false);
            currentLevel = GenerateLevel();
            currentLevel.AddPlayer(player);
        }
    }

    public void ReturnToPrevLevel()
    {
        if (currentLevel.index - 1 >= 0)
        {
            currentLevel.gameObject.SetActive(false);
            currentLevel = levels[currentLevel.index - 1];
            currentLevel.gameObject.SetActive(true);
            currentLevel.AddPlayer(player);
        }
    }
}