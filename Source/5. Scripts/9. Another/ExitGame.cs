using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
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
}
