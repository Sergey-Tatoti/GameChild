using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private const string SaveKey = "GameSave";

    private SaveData.GameData _data;

    #region SaveValue

    public void SaveName(string name) => SaveManager.Save(SaveKey, GetSaveNameSnapshot(name));

    public void SaveExperience(int experience, int level) => SaveManager.Save(SaveKey, GetSaveExperienceSnapshot(experience, level));

    public void SaveIdOpenedItem(int id) => SaveManager.Save(SaveKey, GetSaveIdOpenItemSnapshot(id));

    public void SaveIdSelectedItems(List<int> id) => SaveManager.Save(SaveKey, GetSaveIdSelectedItemsSnapshot(id));

    public void SaveIdShowedItems(int id) => SaveManager.Save(SaveKey, GetSaveIdShowedItemsSnapshot(id));

    SaveData.GameData GetSaveNameSnapshot(string name) { _data.Name = name; return _data; }

    SaveData.GameData GetSaveExperienceSnapshot(int experience, int level) { _data.Experience = experience; _data.Level = level; return _data; }

    SaveData.GameData GetSaveIdOpenItemSnapshot(int id) { _data.IdOpenItems.Add(id); return _data; }

    SaveData.GameData GetSaveIdSelectedItemsSnapshot(List<int> id) { _data.IdSelectedItems = id; return _data; }

    SaveData.GameData GetSaveIdShowedItemsSnapshot(int id) { _data.IdShowedItems.Add(id); return _data; }

    #endregion

    #region LoadeAll
    public void LoadAll(GamePlayManager gamePlayManager)
    {
        _data = SaveManager.Load<SaveData.GameData>(SaveKey);

        gamePlayManager.SetLoadingValues(_data.Name, _data.Experience, _data.Level, _data.IdOpenItems, 
                                         _data.IdSelectedItems, _data.IdShowedItems);
    }
    #endregion
}