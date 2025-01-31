using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GamePlayManagerUI _gamePlayManagerUI;
    [SerializeField] private StepManagerUI _stepManagerUI;
    [SerializeField] private LevelsManager _levelManager;
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private TutorialManagerUI _tutorialManagerUI;
    [SerializeField] private Player _player;
    [SerializeField] private SaveGame _saveGame;
    [SerializeField] private List<Item> _allItems;
    [Space]
    [SerializeField] private int _timeWaitShowNextLevel = 3;

    private void OnEnable()
    {
        _gamePlayManagerUI.ClickedButtonArrow += OnClickedButtonArrow;
        _gamePlayManagerUI.ClickedButtonResetStep += OnClickedButtonResetStep;
        _gamePlayManagerUI.ClickedButtonPlay += OnClickedButtonPlay;
        _gamePlayManagerUI.ClickedButtonMainMenu += OnClickedButtonMainMenu;
        _gamePlayManagerUI.CannedShowNextLevel += OnCannedShowNextLevel;
        _gamePlayManagerUI.ClickedRewardBox += OnClickedRewardBox;
        _gamePlayManagerUI.ClickedButton += OnClickedButton;

        _player.TouchedHitBox += OnTouchedHitBox;
        _player.TouchedStarLevel += OnTouchedStarLevel;
        _player.TouchedStarExperience += OnTouchedStarExperience;
        _player.TouchedTeleport += OnTouchedTeleport;
        _player.ChangedPosition += OnChangedPosition;

        _levelManager.ActivatedLevel += OnActivatedLevel;
        _rewardManager.ChoisenCardReward += OnChoisenCardRewardView;

        _shopManager.ChangedName += OnChangedName;
        _shopManager.ChangedCharacter += _player.OnChangedCharacter;
        _shopManager.ChangedIdSelectedItems += OnChangedIdSelectedItems;
    }

    private void OnDisable()
    {
        _gamePlayManagerUI.ClickedButtonArrow -= OnClickedButtonArrow;
        _gamePlayManagerUI.ClickedButtonResetStep -= OnClickedButtonResetStep;
        _gamePlayManagerUI.ClickedButtonPlay -= OnClickedButtonPlay;
        _gamePlayManagerUI.ClickedButtonMainMenu -= OnClickedButtonMainMenu;
        _gamePlayManagerUI.CannedShowNextLevel -= OnCannedShowNextLevel;
        _gamePlayManagerUI.ClickedRewardBox -= OnClickedRewardBox;
        _gamePlayManagerUI.ClickedButton -= OnClickedButton;

        _player.TouchedHitBox -= OnTouchedHitBox;
        _player.TouchedStarLevel -= OnTouchedStarLevel;
        _player.TouchedStarExperience -= OnTouchedStarExperience;
        _player.TouchedTeleport -= OnTouchedTeleport;
        _player.ChangedPosition -= OnChangedPosition;

        _levelManager.ActivatedLevel -= OnActivatedLevel;

        _rewardManager.ChoisenCardReward -= OnChoisenCardRewardView;

        _shopManager.ChangedName -= OnChangedName;
        _shopManager.ChangedCharacter -= _player.OnChangedCharacter;
        _shopManager.ChangedIdSelectedItems -= OnChangedIdSelectedItems;
    }

    private void Start()
    {
        _saveGame.LoadAll(this);
        _soundManager.PlaySound(SoundManager.TypeSound.GameMusic);
    }

    public void SetLoadingValues(string name, int experience, int numberLevel, List<int> openedIdItems, 
                                 List<int> selectedIdItems, List<int> showedIdItems)
    {
        SetInfoItemsById(openedIdItems, selectedIdItems, showedIdItems);

        _player.SetValue(name, GetItemsById(selectedIdItems));
        _rewardManager.SetValue(_allItems, experience);
        _levelManager.ActivateLevel(numberLevel);
        _shopManager.SetValue(_allItems, name);
        _tutorialManagerUI.SetValue(_gamePlayManagerUI.ArrowLeft, _gamePlayManagerUI.ArrowRight, _gamePlayManagerUI.ArrowDown, 
                                    _gamePlayManagerUI.ArrowUp, _gamePlayManagerUI.ResetStepButton, _gamePlayManagerUI.StartSteps, 
                                    _levelManager.CurrentLevel);
    }

    private void OnActivatedLevel(float stepHorizontal, float stepVertical, Vector3 startPosition, bool isRightDirection)
    {
        _saveGame.SaveExperience(_rewardManager.Experience, _levelManager.CurrentLevel.Number);
        _player.OnActivatedLevel(stepHorizontal, stepVertical, startPosition, isRightDirection);
    }

    private void OnChangedName(string name) { _player.ChangeName(name); _saveGame.SaveName(name); }

    private void OnChangedPosition() => _soundManager.PlaySound(SoundManager.TypeSound.Step);

    private void OnChangedIdSelectedItems(List<int> idSelectedItems, Item item)
    {
        if(!item.IsShower)
        {
            item.SetShowerItem();
            _saveGame.SaveIdShowedItems(item.Id);
        }

        _saveGame.SaveIdSelectedItems(idSelectedItems);
        _soundManager.PlaySound(SoundManager.TypeSound.ChangeClothes);
    }

    #region ----- ActionReward -----

    private void OnChoisenCardRewardView(Item item)
    {
        _shopManager.TakedItem(GetSelectedItemByType(item.TypeItem), item);
        _saveGame.SaveExperience(0, _levelManager.CurrentLevel.Number);
        _saveGame.SaveIdOpenedItem(item.Id);

        StartCoroutine(_gamePlayManagerUI.ShowWinPanel(_timeWaitShowNextLevel, false));
    }

    private void OnClickedRewardBox()
    {
        _rewardManager.FillCardViewRewards();
        _soundManager.PlaySound(SoundManager.TypeSound.ClickRewardBox);
    }

    #endregion

    #region ----- ActionAfterClickedButtons -----

    private void OnClickedButtonPlay()
    {
        _tutorialManagerUI.StopActions();

        if (_stepManagerUI.GetDirections().Count > 0)
            _player.UseMove(_stepManagerUI.GetDirections());
    }

    private void OnCannedShowNextLevel()
    {
        _stepManagerUI.ResetSteps();
        _rewardManager.ResetCardViews();
        _levelManager.ActivateLevel(_levelManager.CurrentLevel.Number + 1);
        _tutorialManagerUI.SetNextLevel(_levelManager.CurrentLevel.Number);
    }

    private void OnClickedButtonArrow(Vector3 direction)
    {
        _stepManagerUI.OnClickedButtonArrow(direction);
        _tutorialManagerUI.ClickedButton(direction);
    }

    private void OnClickedButtonResetStep()
    {
        _stepManagerUI.OnClickedButtonResetStep();
        _tutorialManagerUI.ClickedButton(Vector3.zero);
    }

    private void OnClickedButton() => _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);

    private void OnClickedButtonMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region ----- ActionAfterTouchedElements -----

    private void OnTouchedHitBox()
    {
        _stepManagerUI.ResetSteps();
        _levelManager.UpdateLevel();
        _tutorialManagerUI.SetNextLevel(_levelManager.CurrentLevel.Number);
        _soundManager.PlaySound(SoundManager.TypeSound.HitBox);
    }

    private void OnTouchedStarLevel()
    {
        _rewardManager.ChangeExperience(_levelManager.CurrentLevel.CountExperience);
        _soundManager.PlaySound(SoundManager.TypeSound.WinLevel);

        if (_rewardManager.Experience >= _rewardManager.MaxExperience)
            _gamePlayManagerUI.ActivateBoxRewardButton(true);
        else
            StartCoroutine(_gamePlayManagerUI.ShowWinPanel(_timeWaitShowNextLevel, true));
    }

    private void OnTouchedStarExperience(int experience) => _rewardManager.ChangeExperience(experience);

    private void OnTouchedTeleport() => _soundManager.PlaySound(SoundManager.TypeSound.Teleport);


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