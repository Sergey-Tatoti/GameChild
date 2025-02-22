using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] public List<Level> _levels;

    private Level _lastLevel;
    private Level _currentLevel;

    private int _experience;

    public Level CurrentLevel => _currentLevel;
    public int Experience => _experience;

    public event UnityAction<float, float, Vector3, bool> ActivatedLevel;

    public void SetLoadingValue(int numberLevel, int experience)
    {
        _experience = experience;
        _currentLevel = GetLevelByNumberLevel(numberLevel);
    }

    public void UpdateLevel() => _currentLevel.UpdateLevel();

    public void ChangeExperience(int experience) => _experience += experience;
    public void SetExperience(int experience) => _experience = experience;

    public void ChangeNextLevel() => _currentLevel = GetLevelByNumberLevel(_currentLevel.Number + 1);

    #region ----- Activate Level -----

    public void ActivateLevel(int numberLevel)
    {
        TryDeactivateLastLevel();

        _lastLevel = _currentLevel;
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