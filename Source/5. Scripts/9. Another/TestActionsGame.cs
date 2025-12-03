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

    public void OpenAllLevels()
    {
        LevelsManager levelsManager = FindFirstObjectByType<LevelsManager>();
        GamePlayManager gamePlayManager = FindFirstObjectByType<GamePlayManager>();
        SaveGame saveGame = FindFirstObjectByType<SaveGame>();

        for (int i = levelsManager.CurrentLevel.Number; i < levelsManager.CountLevels; i++)
        {
            saveGame.SaveCompleteLevel(i+1);
        }

        SceneManager.LoadScene(0);
    }
}