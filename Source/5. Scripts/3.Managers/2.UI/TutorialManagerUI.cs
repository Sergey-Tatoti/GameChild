using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerUI : MonoBehaviour
{
    [SerializeField] private float _timeUntilShine;
    [Header("Список направлений для прохождения уровня")]
    [SerializeField] private List<Vector3> _stepsLevel1;
    [SerializeField] private List<Vector3> _stepsLevel2;
    [SerializeField] private List<Vector3> _stepsLevel3;

    private ButtonAnimation _buttonAnimationLeft;
    private ButtonAnimation _buttonAnimationRight;
    private ButtonAnimation _buttonAnimationUp;
    private ButtonAnimation _buttonAnimationDown;
    private ButtonAnimation _buttonAnimationStart;
    private ButtonAnimation _buttonAnimationBack;

    private Coroutine _coroutineResetStep;
    private Coroutine _coroutineNextShineStep;
    private List<Vector3> _chosenSteps = new List<Vector3>();
    private List<Vector3> _currentStepsLevel;

    private int _currentStepIndex = 0;

    public void SetValue(Button arrowLeft, Button arrowRight, Button arrowDown, Button arrowUp, Button resetStep,
                         Button startSteps, Level currentLevel)
    {
        _buttonAnimationLeft = arrowLeft.GetComponent<ButtonAnimation>();
        _buttonAnimationDown = arrowDown.GetComponent<ButtonAnimation>();
        _buttonAnimationUp = arrowUp.GetComponent<ButtonAnimation>();
        _buttonAnimationRight = arrowRight.GetComponent<ButtonAnimation>();
        _buttonAnimationStart = startSteps.GetComponent<ButtonAnimation>();
        _buttonAnimationBack = resetStep.GetComponent<ButtonAnimation>();

        SetNextLevel(currentLevel.Number);
    }

    #region ----- TrySetNewStartTutorial -----

    public void StopActions()
    {
        _currentStepIndex = 0;
        _chosenSteps.Clear();

        ResetShineButtons();
    }

    public void SetNextLevel(int level)
    {
        ResetShineButtons();
        SetCurrentStepsLevel(level);

        if (_currentStepsLevel != null)
            _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(true));
    }

    private void SetCurrentStepsLevel(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                _currentStepsLevel = _stepsLevel1;
                break;
            case 2:
                _currentStepsLevel = _stepsLevel2;
                break;
            case 3:
                _currentStepsLevel = _stepsLevel3;
                break;
            default:
                _currentStepsLevel = null;
                break;
        }
    }

    #endregion

    #region ----- UseTutorialShineButton -----

    public void ClickedButton(Vector3 direction)
    {
        if (_currentStepsLevel != null)
        {
            ResetShineButtons();
            ChangeCountCurrentSteps(direction);
            StartShineButton();
        }
    }

    #region ----- UpdateShineSteps -----

    private void ResetShineButtons()
    {
        _buttonAnimationStart.SetShining(false);
        _buttonAnimationBack.SetShining(false);
        _buttonAnimationUp.SetShining(false);
        _buttonAnimationDown.SetShining(false);
        _buttonAnimationRight.SetShining(false);
        _buttonAnimationLeft.SetShining(false);

        if (_coroutineNextShineStep != null)
            StopCoroutine(_coroutineNextShineStep);
    }

    private void ChangeCountCurrentSteps(Vector3 direction)
    {
        if (direction == Vector3.zero && _chosenSteps.Count > 0)
        {
            _chosenSteps.RemoveAt(_chosenSteps.Count - 1);
            _currentStepIndex--;
        }
        else if (direction != Vector3.zero)
        {
            _chosenSteps.Add(direction);
            _currentStepIndex++;
        }
    }

    #endregion

    #region ----- SetCurrentButtonShine -----

    private void StartShineButton()
    {
        if (_chosenSteps.Count <= _currentStepsLevel.Count)
            TryShineNextStep();
        else
            _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(false));
    }

    private void TryShineNextStep()
    {
        if (_currentStepIndex - 1 < 0 || _currentStepIndex == 0)
        {
            _currentStepIndex = 0;
            _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(true));
            return;
        }

        if (_chosenSteps[_currentStepIndex - 1] == _currentStepsLevel[_currentStepIndex - 1])
            _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(true));
        else
            _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(false));
    }
    
    private IEnumerator SetCurrentButtonShine(bool isNextShowButton)
    {
        yield return new WaitForSeconds(_timeUntilShine);

        if (!isNextShowButton)
            _buttonAnimationBack.SetShining(true);
        else if (_chosenSteps.Count == _currentStepsLevel.Count)
            _buttonAnimationStart.SetShining(true);
        else
            ShineStepNextDirection();
    }

    private void ShineStepNextDirection()
    {
        Vector3 directionStep = _currentStepsLevel[_currentStepIndex];

        if (directionStep == Vector3.left)
            _buttonAnimationLeft.SetShining(true);
        else if (directionStep == Vector3.right)
            _buttonAnimationRight.SetShining(true);
        else if (directionStep == Vector3.down)
            _buttonAnimationDown.SetShining(true);
        else if (directionStep == Vector3.up)
            _buttonAnimationUp.SetShining(true);
    }

    #endregion

    #endregion
}