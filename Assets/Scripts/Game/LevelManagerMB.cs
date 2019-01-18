using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class LevelManagerMB : Singleton<LevelManagerMB>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Text levelText;

    private PlayerMB player;
    private LevelMB currentLevel;

    private List<LevelMB> levels = new List<LevelMB>();

    private void Awake()
    {
        // Generate game objects
        GeneratePlayer();
        currentLevel = GenerateLevel();
        currentLevel.AddPlayer(player);

        RefreshUI();
    }

    private void GeneratePlayer()
    {
        // Instantiat the player in the scene
        player = Instantiate(playerPrefab).GetComponent<PlayerMB>();
        Assert.IsTrue(player != null);
    }

    private LevelMB GenerateLevel()
    {
        // Instantiate the level
        LevelMB level = Instantiate(levelPrefab).GetComponent<LevelMB>();
        Assert.IsTrue(currentLevel != null);

        // Initialize the level
        level.index = levels.Count;
        level.Generate();
        levels.Add(level);

        // Set enter position
        if (currentLevel != null)
        {
            level.enterXY = currentLevel.exitXY;
        }

        return level;
    }

    public void ProceedToNextLevel()
    {
        if (currentLevel.index + 1 <= GameConstants.MAX_NUM_LEVELS)
        {
            // Deactivate the current level
            currentLevel.gameObject.SetActive(false);

            // Generate the next level
            currentLevel = currentLevel.index + 1 < levels.Count ? levels[currentLevel.index + 1] : GenerateLevel();

            // Activate the next level
            currentLevel.gameObject.SetActive(true);
            currentLevel.AddPlayer(player);

            RefreshUI();
        }
        else
        {
            GameManagerMB.Instance.ProceedToWinScreen();
        }
    }

    public void ReturnToPrevLevel()
    {
        if (currentLevel.index - 1 >= 0)
        {
            // Deactivate the current
            currentLevel.gameObject.SetActive(false);

            // Return to the previous level
            currentLevel = levels[currentLevel.index - 1];
            currentLevel.gameObject.SetActive(true);
            currentLevel.AddPlayer(player);

            RefreshUI();
        }
    }

    public void RepeatCurrentLevel()
    {
        // Move the player back to the start
        currentLevel.AddPlayer(player);
    }

    private void RefreshUI()
    {
        // Refresh the text and images on the screen
        levelText.text = "Level " + (currentLevel.index + 1) + " / " + GameConstants.MAX_NUM_LEVELS;
    }
}