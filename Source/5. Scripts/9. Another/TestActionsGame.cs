using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestActionsGame : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void BuyAllItems()
    {
        GamePlayManager gamePlayManager = FindFirstObjectByType<GamePlayManager>();
        SaveGame saveGame = FindFirstObjectByType<SaveGame>();

        for (int i = 0; i < gamePlayManager._allItems.Count; i++)
        {
            saveGame.SaveIdOpenedItem(gamePlayManager._allItems[i].Id);
        }

        SceneManager.LoadScene(0);
    }

    public void SwitchNextLevel(bool isNext)
    {
        LevelsManager levelsManager = FindFirstObjectByType<LevelsManager>();
        SaveGame saveGame = FindFirstObjectByType<SaveGame>();
        int numberLevel = levelsManager.CurrentLevel.Number;
        int maxNumberLevel = levelsManager._levels[levelsManager._levels.Count - 1].Number;

        if (isNext)
            numberLevel++;
        else
            numberLevel--;

        if (numberLevel == 0)
            numberLevel = maxNumberLevel;
        else if (numberLevel == maxNumberLevel + 1)
            numberLevel = 1;

        saveGame.SaveExperience(0, numberLevel);
        SceneManager.LoadScene(0);
    }
}
