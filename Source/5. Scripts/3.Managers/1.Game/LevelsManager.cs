using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelsManager : MonoBehaviour
{
    private List<Level> _levels;
    private Level _lastLevel;
    private Level _currentLevel;
    private Level _newLevel;

    private int _experience;

    public Level CurrentLevel => _currentLevel;
    public Level NewLevel => _newLevel;
    public int Experience => _experience;

    public event UnityAction<float, float, Vector3, bool> ActivatedLevel;

    public void SetLoadingValue(List<Level> levels, List<int> numbersCompleteLevels, int numberNewLevel, int experience)
    {
        _levels = levels;
        _experience = experience;
        _newLevel = GetLevelByNumberLevel(numberNewLevel);
        _currentLevel = _newLevel;

        for (int i = 0; i < _levels.Count; i++)
        {
            bool isComplete = false;

            for (int j = 0; j < numbersCompleteLevels.Count; j++)
            {
                if (_levels[i].Number == numbersCompleteLevels[j])
                    isComplete = true;
            }

            _levels[i].SetValue(isComplete);
        }
    }

    public void UpdateLevel() => _currentLevel.UpdateLevel();

    public void ChangeExperience(int experience) => _experience += experience;

    public void SetExperience(int experience) => _experience = experience;

    public void ChangeNextLevel()
    {
        if (!_currentLevel.IsCompleted)
            _currentLevel.CompleteLevel();

        _lastLevel = _currentLevel;
        _currentLevel = GetLevelByNumberLevel(_currentLevel.Number + 1);

        if (!_currentLevel.IsCompleted)
            _newLevel = _currentLevel;
    }

    public void ChooseLevel(int numberLevel)
    {
        _lastLevel = _currentLevel;

        ActivateLevel(numberLevel);
    }

    #region ----- Activate Level -----

    public void ActivateLevel(int numberLevel)
    {
        TryDeactivateLastLevel();

        _currentLevel = GetLevelByNumberLevel(numberLevel);
        _currentLevel.UpdateLevel();
        _currentLevel.gameObject.SetActive(true);

        ActivatedLevel?.Invoke(_currentLevel.PlayerStepHorizontal, _currentLevel.PlayerStepVertical,
                               _currentLevel.PlayerStartPosition, _currentLevel.IsRightPlayerDirection);
    }

    private void TryDeactivateLastLevel()
    {
        if (_lastLevel != null)
        {
            _lastLevel.gameObject.SetActive(false);
            _lastLevel.UpdateLevel();
        }
    }

    #endregion

    private Level GetLevelByNumberLevel(int numberLevel)
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            if (_levels[i].Number == numberLevel)
                return _levels[i];
        }

        return _levels[0];
    }
}