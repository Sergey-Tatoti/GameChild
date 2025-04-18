using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GamePlayManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelShop;
    [SerializeField] private SwitchLevelManagerUI _switchLevelManagerUI;
    [SerializeField] private RewardManagerUI _rewardManagerUI;
    [SerializeField] private GameButtonManagerUI _gameButtonManagerUI;
    [SerializeField] private StepManagerUI _stepManagerUI;
    [Space]
    [SerializeField] private float _timeWaitShowNextLevel = 3;
    [SerializeField] private int _countAttemptByShowTutrial = 3;

    private List<Tutorial> _tutorials;
    private SoundManager _soundManager;
    private int _countReward;
    private int _levelNumber;
    private int _currentAttempt;

    public event UnityAction CannedShowNextLevel;
    public event UnityAction CompletedLevel;
    public event UnityAction ClickedButtonBackMenu;
    public event UnityAction<Item> ChoisenCardReward;
    public event UnityAction<List<Vector3>> CannedMovePlayer;

    private void OnEnable()
    {
        _gameButtonManagerUI.ClickedButtonShop += OnClickedButtonShop;
        _gameButtonManagerUI.ClickedButtonArrow += OnClickedButtonArrow;
        _gameButtonManagerUI.ClickedButtonResetStep += OnClickedButtonResetStep;
        _gameButtonManagerUI.ClickedButtonPlay += OnClickedButtonPlay;
        _gameButtonManagerUI.ClickedButtonBackMenu += OnClickedButtonBackMenu;
        _gameButtonManagerUI.ClickedButtonLamp += OnClickedButtonLamp;

        _switchLevelManagerUI.CloudsFilledScene += OnCloudsFilledScene;

        _rewardManagerUI.OpenedBigBoxReward += OnOpenedBigBoxReward;
        _rewardManagerUI.ChoisenCardReward += OnChoisenCardRewardView;
        _rewardManagerUI.MovedWaitBigBoxReward += OnMovedWaitBigBoxReward;
    }

    private void OnDisable()
    {
        _gameButtonManagerUI.ClickedButtonShop -= OnClickedButtonShop;
        _gameButtonManagerUI.ClickedButtonArrow -= OnClickedButtonArrow;
        _gameButtonManagerUI.ClickedButtonResetStep -= OnClickedButtonResetStep;
        _gameButtonManagerUI.ClickedButtonPlay -= OnClickedButtonPlay;
        _gameButtonManagerUI.ClickedButtonBackMenu -= OnClickedButtonBackMenu;
        _gameButtonManagerUI.ClickedButtonLamp -= OnClickedButtonLamp;

        _switchLevelManagerUI.CloudsFilledScene -= OnCloudsFilledScene;

        _rewardManagerUI.OpenedBigBoxReward -= OnOpenedBigBoxReward;
        _rewardManagerUI.ChoisenCardReward -= OnChoisenCardRewardView;
        _rewardManagerUI.MovedWaitBigBoxReward -= OnMovedWaitBigBoxReward;
    }

    public void SetStartValue(SoundManager soundManager, Level level, List<Item> items, int experience, int countReward,
                              List<Tutorial> tutorials)
    {
        _soundManager = soundManager;
        _countReward = countReward;
        _tutorials = tutorials;
        _rewardManagerUI.SetValue(items, experience);

        for (int i = 0; i < _tutorials.Count; i++)
        {
            _tutorials[i].SetButtonsGame(_gameButtonManagerUI.ArrowLeft, _gameButtonManagerUI.ArrowRight, _gameButtonManagerUI.ArrowDown,
                                    _gameButtonManagerUI.ArrowUp, _gameButtonManagerUI.ResetStepButton, _gameButtonManagerUI.StartSteps,
                                    _gameButtonManagerUI.ShopButton, _gameButtonManagerUI.BackMenuButton, _gameButtonManagerUI.LampButton,
                                    _gameButtonManagerUI.BackShopButton, level);
        }
    }

    #region ----- Stages Game -----

    public void StartLevel(int numberLevel)
    {
        _levelNumber = numberLevel;
        _stepManagerUI.ResetSteps();
        _gameButtonManagerUI.ActivateActionButton(true);

        UpdateTutorials();
        TryUseTutorialSteps(numberLevel);
    }

    public void ReloadLevel()
    {
        _currentAttempt++;
        _stepManagerUI.ResetSteps();
        _gameButtonManagerUI.ActivateActionButton(true);
        TryUseTutorialSteps(_levelNumber);

        if (_currentAttempt == _countAttemptByShowTutrial)
            _gameButtonManagerUI.ActivateButtonLamp(true);
    }

    public void EndLevel(bool isActivateReward)
    {
        _currentAttempt = 0;
        _gameButtonManagerUI.ActivateActionButton(false);
        ResetTutorial();

        if (isActivateReward)
            ActivateReward();
        else
            StartCoroutine(ShowWinPanel(false));
    }

    public void ActivateReward()
    {
        _rewardManagerUI.ActivateReward(_countReward);
    }

    #endregion

    #region ----- Action Games -----

    public void ChangeExperience(int experience) => _rewardManagerUI.ChangeExperience(experience);

    private void OnChoisenCardRewardView(Item item)
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);

        ChoisenCardReward?.Invoke(item);

        StartCoroutine(ShowWinPanel(false));
        TryUseTutorialShop(item);
    }

    private void OnCloudsFilledScene() => CannedShowNextLevel?.Invoke();

    private void OnOpenedBigBoxReward() => _soundManager.PlaySound(SoundManager.TypeSound.OpenRewardBox);

    private void OnMovedWaitBigBoxReward() => _soundManager.PlaySound(SoundManager.TypeSound.MoveRewardBox);

    private void TryUseTutorialSteps(int numberLevel)
    {
        for (int i = 0; i < _tutorials.Count; i++)
        {
            _tutorials[i].TryActivateTutorialSteps(numberLevel);
        }
    }

    private void UpdateTutorials()
    {
        for (int i = 0; i < _tutorials.Count; i++)
        {
            _tutorials[i].UpdateTutorial();
        }
    }

    private void TurnTutorial(bool isTurn)
    {
        for (int i = 0; i < _tutorials.Count; i++)
        {
            _tutorials[i].TurnTutorial(isTurn);
        }
    }

    private void ResetTutorial()
    {
        for (int i = 0; i < _tutorials.Count; i++)
        {
            _tutorials[i].DeactivateTutorialSteps();
        }
    }

    private void TryUseTutorialShop(Item item)
    {
        for (int i = 0; i < _tutorials.Count; i++)
        {
            if (_tutorials[i].Type == Tutorial.TypeTutorial.Shop)
                _tutorials[i].TryActivateTutorialShop(_levelNumber, item);
        }
    }

    private IEnumerator ShowWinPanel(bool isShowSmile)
    {
        CompletedLevel?.Invoke();

        yield return new WaitForSeconds(_timeWaitShowNextLevel);

        _soundManager.PlaySound(SoundManager.TypeSound.SwitchLevel);
        _switchLevelManagerUI.UseSwitchAnimation(isShowSmile);
    }

    #endregion

    #region ----- Clicked Buttons -----

    public void OnClickedButton() => _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);

    private void OnClickedButtonPlay()
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButtonPlay);

        if (_stepManagerUI.CountActiveDirections > 0)
        {
            _gameButtonManagerUI.ActivateActionButton(false);
            Invoke(nameof(CanMove), 0.1f);
        }
    }

    private void CanMove() => CannedMovePlayer?.Invoke(_stepManagerUI.GetDirections());

    private void OnClickedButtonShop(bool isShow)
    {
        _panelShop.SetActive(isShow);
        OnClickedButton();
    }

    private void OnClickedButtonArrow(Vector3 direction)
    {
        _stepManagerUI.OnClickedButtonArrow(direction);

        for (int i = 0; i < _tutorials.Count; i++) { _tutorials[i].ClickedButtonArrow(direction); }

        OnClickedButton();
    }

    private void OnClickedButtonResetStep()
    {
        _stepManagerUI.OnClickedButtonResetStep();

        for (int i = 0; i < _tutorials.Count; i++) { _tutorials[i].ClickedButtonArrow(Vector3.zero); }

        OnClickedButton();
    }

    private void OnClickedButtonBackMenu() { OnClickedButton(); ClickedButtonBackMenu?.Invoke(); }

    private void OnClickedButtonLamp()
    {
        OnClickedButton();
        TurnTutorial(true);
        TryUseTutorialSteps(_levelNumber);

        _gameButtonManagerUI.ActivateButtonLamp(false);
    }

    #endregion
}