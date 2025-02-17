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
    [SerializeField] private TutorialManagerUI _tutorialManagerUI;
    [Space]
    [SerializeField] private int _timeWaitShowNextLevel = 3;

    private SoundManager _soundManager;
    private int _countReward;

    public event UnityAction CannedShowNextLevel;
    public event UnityAction<Item> ChoisenCardReward;
    public event UnityAction<List<Vector3>> CannedMovePlayer;

    private void OnEnable()
    {
        _gameButtonManagerUI.ClickedButtonShop += OnClickedButtonShop;
        _gameButtonManagerUI.ClickedButtonArrow += OnClickedButtonArrow;
        _gameButtonManagerUI.ClickedButtonResetStep += OnClickedButtonResetStep;
        _gameButtonManagerUI.ClickedButtonPlay += OnClickedButtonPlay;

        _switchLevelManagerUI.CloudsFilledScene += OnCloudsFilledScene;

        _rewardManagerUI.OpenedBigBoxReward += OnOpenedBigBoxReward;
        _rewardManagerUI.ChoisenCardReward += OnChoisenCardRewardView;
    }

    private void OnDisable()
    {
        _gameButtonManagerUI.ClickedButtonShop -= OnClickedButtonShop;
        _gameButtonManagerUI.ClickedButtonArrow -= OnClickedButtonArrow;
        _gameButtonManagerUI.ClickedButtonResetStep -= OnClickedButtonResetStep;
        _gameButtonManagerUI.ClickedButtonPlay -= OnClickedButtonPlay;

        _switchLevelManagerUI.CloudsFilledScene -= OnCloudsFilledScene;

        _rewardManagerUI.OpenedBigBoxReward -= OnOpenedBigBoxReward;
        _rewardManagerUI.ChoisenCardReward -= OnChoisenCardRewardView;
    }

    public void SetStartValue(SoundManager soundManager, Level level, List<Item> items, int experience, int countReward)
    {
        _soundManager = soundManager;
        _countReward = countReward;

        _rewardManagerUI.SetValue(items, experience);
        _tutorialManagerUI.SetValue(_gameButtonManagerUI.ArrowLeft, _gameButtonManagerUI.ArrowRight, _gameButtonManagerUI.ArrowDown,
                                    _gameButtonManagerUI.ArrowUp, _gameButtonManagerUI.ResetStepButton, _gameButtonManagerUI.StartSteps,
                                    level);
    }

    #region ----- Stages Game -----

    public void StartLevel(int numberLevel)
    {
        _stepManagerUI.ResetSteps();
        _gameButtonManagerUI.ActivateActionButton(true);
        _tutorialManagerUI.SetNextLevel(numberLevel);
    }

    public void ReloadLevel(int numberLevel)
    {
        _stepManagerUI.ResetSteps();
        _tutorialManagerUI.SetNextLevel(numberLevel);
    }

    public void EndLevel(bool isActivateReward)
    {
        _gameButtonManagerUI.ActivateActionButton(false);

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
        StartCoroutine(ShowWinPanel(false));

        ChoisenCardReward?.Invoke(item);
    }

    private void OnCloudsFilledScene() => CannedShowNextLevel?.Invoke();

    private void OnOpenedBigBoxReward() => _soundManager.PlaySound(SoundManager.TypeSound.ClickRewardBox);

    private IEnumerator ShowWinPanel(bool isShowSmile)
    {
        yield return new WaitForSeconds(_timeWaitShowNextLevel);

        _switchLevelManagerUI.UseSwitchAnimation(isShowSmile);
    }

    #endregion

    #region ----- Clicked Buttons -----

    private void OnClickedButtonPlay()
    {
        _tutorialManagerUI.StopActions();

        if (_stepManagerUI.GetDirections().Count > 0)
            CannedMovePlayer?.Invoke(_stepManagerUI.GetDirections());
    }

    private void OnClickedButtonShop(bool isShow)
    {
        _panelShop.SetActive(isShow);
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
    }

    private void OnClickedButtonArrow(Vector3 direction)
    {
        _stepManagerUI.OnClickedButtonArrow(direction);
        _tutorialManagerUI.ClickedButton(direction);
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
    }

    private void OnClickedButtonResetStep()
    {
        _stepManagerUI.OnClickedButtonResetStep();
        _tutorialManagerUI.ClickedButton(Vector3.zero);
    }

    #endregion
}