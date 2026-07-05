using GamePush;
using UnityEngine;

public static class SaveManager
{
    public static void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        Debug.Log(jsonDataString);
        //PlayerPrefs.SetString(key, jsonDataString);
        GP_Player.Set(key, jsonDataString);
        GP_Player.Sync();
    }

    public static T Load<T>(string key) where T : new()
    {
        if (GP_Player.Has(key))
        {
            string loadedString = GP_Player.GetString(key);
            Debug.Log(loadedString);

            if (!string.IsNullOrEmpty(loadedString) && loadedString.Trim().StartsWith("{"))
            {
                return JsonUtility.FromJson<T>(loadedString);
            }
            else
            {
                Debug.LogWarning($"[GamePush Load] ������� ������ ��� ����� ������ ��� ����� {key}. ������� ����� ������.");
                return new T();
            }
        }
        else
        {
            Debug.Log($"[GamePush Load] Key {key} not found. Creating new instance.");
            return new T();
        }
    }
}