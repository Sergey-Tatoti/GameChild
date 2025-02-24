using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GamePlayManagerUI _gamePlayManagerUI;
    [SerializeField] private LevelsManager _levelManager;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private CloudsManager _cloudsManager;
    [SerializeField] private Player _player;
    [SerializeField] private SaveGame _saveGame;
    [SerializeField] public List<Item> _allItems;

    private int _countReward = 3;
    private int _maxExperience = 100;

    private void OnEnable()
    {
        _gamePlayManagerUI.CannedMovePlayer += OnCannedMovePlayer;
        _gamePlayManagerUI.CannedShowNextLevel += OnCannedShowNextLevel;
        _gamePlayManagerUI.ChoisenCardReward += OnChoisenCardRewardView;
        _gamePlayManagerUI.CompletedLevel += OnCompletedLevel;

        _player.TouchedHitBox += OnTouchedHitBox;
        _player.TouchedStarLevel += OnTouchedStarLevel;
        _player.TouchedStarExperience += OnTouchedStarExperience;
        _player.TouchedTeleport += OnTouchedTeleport;
        _player.TouchedKey += OnTouchedKey;
        _player.TouchedLock += OnTouchedLock;
        _player.ChangedPosition += OnChangedPosition;

        _levelManager.ActivatedLevel += OnActivatedLevel;
        _shopManager.ChangedCharacter += _player.OnChangedCharacter;
        _shopManager.ChangedIdSelectedItems += OnChangedIdSelectedItems;
        _shopManager.ClickedButton += _gamePlayManagerUI.OnClickedButton;
    }

    private void OnDisable()
    {
        _gamePlayManagerUI.CannedMovePlayer -= OnCannedMovePlayer;
        _gamePlayManagerUI.CannedShowNextLevel -= OnCannedShowNextLevel;
        _gamePlayManagerUI.ChoisenCardReward -= OnChoisenCardRewardView;
        _gamePlayManagerUI.CompletedLevel -= OnCompletedLevel;

        _player.TouchedHitBox -= OnTouchedHitBox;
        _player.TouchedStarLevel -= OnTouchedStarLevel;
        _player.TouchedStarExperience -= OnTouchedStarExperience;
        _player.TouchedKey -= OnTouchedKey;
        _player.TouchedLock -= OnTouchedLock;
        _player.TouchedTeleport -= OnTouchedTeleport;
        _player.ChangedPosition -= OnChangedPosition;

        _levelManager.ActivatedLevel -= OnActivatedLevel;
        _shopManager.ChangedCharacter -= _player.OnChangedCharacter;
        _shopManager.ChangedIdSelectedItems -= OnChangedIdSelectedItems;
        _shopManager.ClickedButton -= _gamePlayManagerUI.OnClickedButton;
    }

    private void Start() => _saveGame.LoadAll(this);

    public void SetLoadingValues(int experience, int numberLevel, List<int> openedIdItems,
                                 List<int> selectedIdItems, List<int> showedIdItems)
    {
        SetInfoItemsById(openedIdItems, selectedIdItems, showedIdItems);

        _player.SetValue(name, GetItemsById(selectedIdItems));
        _levelManager.SetLoadingValue(numberLevel, experience);
        _shopManager.SetValue(_allItems);
        _gamePlayManagerUI.SetStartValue(_soundManager, _levelManager.CurrentLevel, _allItems, _levelManager.Experience, _countReward);
        _soundManager.PlaySound(SoundManager.TypeSound.GameMusic);
        _cloudsManager.StartMoveClouds();
        _levelManager.ActivateLevel(numberLevel);
    }

    #region ----- ActionGame -----

    private void OnCompletedLevel()
    {
        _levelManager.ChangeNextLevel();
        _saveGame.SaveExperience(_levelManager.Experience, _levelManager.CurrentLevel.Number);
    }

    private void OnActivatedLevel(float stepHorizontal, float stepVertical, Vector3 startPosition, bool isRightDirection)
    {
        _player.OnActivatedLevel(stepHorizontal, stepVertical, startPosition, isRightDirection);
        _gamePlayManagerUI.StartLevel(_levelManager.CurrentLevel.Number);
    }

    private void ChangeExperience(int experience)
    {
        _levelManager.ChangeExperience(experience);
        _gamePlayManagerUI.ChangeExperience(_levelManager.Experience);
    }

    private void OnChoisenCardRewardView(Item item)
    {
        _levelManager.SetExperience(0);
        _gamePlayManagerUI.ChangeExperience(_levelManager.Experience);
        _shopManager.TakedItem(GetSelectedItemByType(item.TypeItem), item);
        _saveGame.SaveIdOpenedItem(item.Id);
    }

    private void OnCannedMovePlayer(List<Vector3> directions) => _player.UseMove(directions);

    private void OnCannedShowNextLevel() => _levelManager.ActivateLevel(_levelManager.CurrentLevel.Number + 1);

    private void OnChangedIdSelectedItems(List<int> idSelectedItems, Item item)
    {
        if (!item.IsShower)
        {
            item.SetShowerItem();
            _saveGame.SaveIdShowedItems(item.Id);
        }

        _saveGame.SaveIdSelectedItems(idSelectedItems);
        _soundManager.PlaySound(SoundManager.TypeSound.ClickItem);
    }

    private void OnChangedPosition() => _soundManager.PlaySound(SoundManager.TypeSound.UseStep);

    #endregion

    #region ----- ActionAfterTouchedElements -----

    private void OnTouchedHitBox()
    {
        _levelManager.UpdateLevel();
        _gamePlayManagerUI.ReloadLevel(_levelManager.CurrentLevel.Number);
        _soundManager.PlaySound(SoundManager.TypeSound.TouchedHitBox);
    }

    private void OnTouchedStarLevel()
    {
        bool isActivateReward = _levelManager.Experience >= _maxExperience && _player.Items.Count < _allItems.Count;
        ChangeExperience(_levelManager.CurrentLevel.CountExperience);

        _gamePlayManagerUI.EndLevel(isActivateReward);
        _soundManager.PlaySound(SoundManager.TypeSound.TouchedStar);
    }

    private void OnTouchedStarExperience(int experience)
    {
        ChangeExperience(experience);
        _soundManager.PlaySound(SoundManager.TypeSound.TouchedMiniStar);
    }

    private void OnTouchedLock()
    {
        _levelManager.UpdateLevel();
        _gamePlayManagerUI.ReloadLevel(_levelManager.CurrentLevel.Number);
        _soundManager.PlaySound(SoundManager.TypeSound.TouchedCloseLock);
    }

    private void OnTouchedTeleport() => _soundManager.PlaySound(SoundManager.TypeSound.TouchedTeleport);

    private void OnTouchedKey() => _soundManager.PlaySound(SoundManager.TypeSound.TouchedKey);


    #endregion

    #region ----- ActionItem -----

    private List<Item> GetItemsById(List<int> id)
    {
        List<Item> items = new List<Item>();

        for (int i = 0; i < _allItems.Count; i++)
        {
            for (int j = 0; j < id.Count; j++)
            {
                if (_allItems[i].Id == id[j])
                    items.Add(_allItems[i]);
            }
        }
        return items;
    }
    private Item GetSelectedItemByType(ItemInfo.Type type)
    {
        for (int i = 0; i < _allItems.Count; i++)
        {
            if (_allItems[i].IsSelected && _allItems[i].TypeItem == type)
                return _allItems[i];
        }
        return null;
    }

    private void SetInfoItemsById(List<int> idOpened, List<int> idSelected, List<int> idShowed)
    {
        for (int i = 0; i < _allItems.Count; i++)
        {
            for (int j = 0; j < idOpened.Count; j++)
            {
                if (_allItems[i].Id == idOpened[j])
                    _allItems[i].OpenItem(true);
            }

            for (int j = 0; j < idSelected.Count; j++)
            {
                if (_allItems[i].Id == idSelected[j])
                    _allItems[i].SelectItem(true);
            }

            for (int j = 0; j < idShowed.Count; j++)
            {
                if (_allItems[i].Id == idShowed[j])
                    _allItems[i].SetShowerItem();
            }
        }
    }

    #endregion
}