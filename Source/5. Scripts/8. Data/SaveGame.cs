using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private const string SaveKey = "GameSave";

    private SaveData.GameData _data;

    #region SaveValue

    public void SaveExperience(int experience) => SaveManager.Save(SaveKey, GetSaveExperienceSnapshot(experience));

    public void SaveNewLevel(int numberLevel) => SaveManager.Save(SaveKey, GetSaveNewLevelSnapshot(numberLevel));

    public void SaveCompleteLevel(int numberLevel) => SaveManager.Save(SaveKey, GetSaveCompleteLevelSnapshot(numberLevel));

    public void SaveIdOpenedItem(int id) => SaveManager.Save(SaveKey, GetSaveIdOpenItemSnapshot(id));

    public void SaveIndexGroundAvatarItem(int index) => SaveManager.Save(SaveKey, GetSaveIndexGroundAvatarSnapshot(index));

    public void SaveIdSelectedItems(List<int> id) => SaveManager.Save(SaveKey, GetSaveIdSelectedItemsSnapshot(id));

    public void SaveIdShowedItems(int id) => SaveManager.Save(SaveKey, GetSaveIdShowedItemsSnapshot(id));

    SaveData.GameData GetSaveExperienceSnapshot(int experience) { _data.Experience = experience; return _data; }

    SaveData.GameData GetSaveNewLevelSnapshot(int numberLevel) { _data.NumberNewLevel = numberLevel; return _data; }

    SaveData.GameData GetSaveCompleteLevelSnapshot(int numberLevel) { _data.NumbersCompleteLevels.Add(numberLevel); return _data; }

    SaveData.GameData GetSaveIdOpenItemSnapshot(int id) { _data.IdOpenItems.Add(id); return _data; }

    SaveData.GameData GetSaveIndexGroundAvatarSnapshot(int index) { _data.IndexGroundAvatar = index; return _data; }

    SaveData.GameData GetSaveIdSelectedItemsSnapshot(List<int> id) { _data.IdSelectedItems = id; return _data; }

    SaveData.GameData GetSaveIdShowedItemsSnapshot(int id) { _data.IdShowedItems.Add(id); return _data; }

    #endregion

    #region LoadeAll
    public void LoadAll(MainManager mainManager)
    {
        _data = SaveManager.Load<SaveData.GameData>(SaveKey);

        mainManager.SetLoadingValues(_data.Experience, _data.NumbersCompleteLevels, _data.NumberNewLevel, _data.IdOpenItems, 
                                         _data.IdSelectedItems, _data.IdShowedItems, _data.IndexGroundAvatar);
    }
    #endregion
}