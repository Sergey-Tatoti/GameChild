using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager
{
    public static void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        Debug.Log(jsonDataString);
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static T Load<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);
            Debug.Log(loadedString);
            return JsonUtility.FromJson<T>(loadedString);
        }
        else
        {
            return new T();
        }
    }
}