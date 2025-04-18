using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLevelSteps : Tutorial
{
    [Header("Список направлений для прохождения уровня")]
    [SerializeField] private List<Vector3> _stepsLevel;

    private Coroutine _coroutineResetStep;
    private Coroutine _coroutineNextShineStep;
    private List<Vector3> _chosenSteps = new List<Vector3>();
    private int _currentStepIndex = 0;

    public override void ActivateTutroialSteps()
    {
        base.ActivateTutroialSteps();

        _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(true));
    }

    public override void DeactivateTutorialSteps()
    {
        base.DeactivateTutorialSteps();

        _currentStepIndex = 0;
        _chosenSteps.Clear();
        _level.HideAllArrows();

        ResetShineButtons();
    }

    public override void ShowButtonArrows(Vector3 direction)
    {
        base.ShowButtonArrows(direction);

        ResetShineButtons();
        ChangeCountCurrentSteps(direction);
        StartShineButton();
    }

    private void ChangeCountCurrentSteps(Vector3 direction)
    {
        if (direction == Vector3.zero && _chosenSteps.Count > 0)
        {
            _chosenSteps.RemoveAt(_chosenSteps.Count - 1);
            _currentStepIndex--;
            _level.ShowArrow(false, _currentStepIndex, direction);
            ActivateArrows(true);
        }
        else if (direction != Vector3.zero)
        {
            _chosenSteps.Add(direction);
            _level.ShowArrow(true, _currentStepIndex, direction);
            _currentStepIndex++;
            ActivateArrows(_chosenSteps.Count < _stepsLevel.Count);
        }
    }

    private void ActivateArrows(bool isActivate)
    {
        _buttonAnimationUp.GetComponent<Button>().interactable = isActivate;
        _buttonAnimationDown.GetComponent<Button>().interactable = isActivate;
        _buttonAnimationRight.GetComponent<Button>().interactable = isActivate;
        _buttonAnimationLeft.GetComponent<Button>().interactable = isActivate;
    }

    private void StartShineButton()
    {
        if (_chosenSteps.Count <= _stepsLevel.Count)
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

        if (_chosenSteps[_currentStepIndex - 1] == _stepsLevel[_currentStepIndex - 1])
            _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(true));
        else
            _coroutineNextShineStep = StartCoroutine(SetCurrentButtonShine(false));
    }

    private void ResetShineButtons()
    {
        _buttonAnimationStart.SetShining(false);
        _buttonAnimationBackStep.SetShining(false);
        _buttonAnimationUp.SetShining(false);
        _buttonAnimationDown.SetShining(false);
        _buttonAnimationRight.SetShining(false);
        _buttonAnimationLeft.SetShining(false);

        if (_coroutineNextShineStep != null)
            StopCoroutine(_coroutineNextShineStep);
    }

    private IEnumerator SetCurrentButtonShine(bool isNextShowButton)
    {
        yield return new WaitForSeconds(_timeUntilShine);

        if (!isNextShowButton)
            _buttonAnimationBackStep.SetShining(true);
        else if (_chosenSteps.Count == _stepsLevel.Count)
            _buttonAnimationStart.SetShining(true);
        else
            ShineStepNextDirection();
    }

    private void ShineStepNextDirection()
    {
        Vector3 directionStep = _stepsLevel[_currentStepIndex];

        if (directionStep == Vector3.left)
            _buttonAnimationLeft.SetShining(true);
        else if (directionStep == Vector3.right)
            _buttonAnimationRight.SetShining(true);
        else if (directionStep == Vector3.down)
            _buttonAnimationDown.SetShining(true);
        else if (directionStep == Vector3.up)
            _buttonAnimationUp.SetShining(true);
    }
}