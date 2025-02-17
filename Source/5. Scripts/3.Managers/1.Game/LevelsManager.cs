using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    private Level _currentLevel;

    public Level CurrentLevel => _currentLevel;

    public event UnityAction<float, float, Vector3, bool> ActivatedLevel;

    public void SetLoadingValue(int numberLevel) => SetCurrentLevel(numberLevel);
    public void UpdateLevel() => _currentLevel.UpdateLevel();

    #region ----- Activate Level -----
    public void ActivateLevel(int numberLevel)
    {
        TryDeactivateLastLevel();
        SetCurrentLevel(numberLevel);

        _currentLevel.gameObject.SetActive(true);

        ActivatedLevel?.Invoke(_currentLevel.PlayerStepHorizontal, _currentLevel.PlayerStepVertical,
                               _currentLevel.PlayerStartPosition, _currentLevel.IsRightPlayerDirection);
    }

    private void TryDeactivateLastLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.gameObject.SetActive(false);
            _currentLevel.UpdateLevel();
        }
    }

    private void SetCurrentLevel(int numberLevel)
    {
        if (numberLevel > _levels.Count)
            _currentLevel = _levels[0];
        else
            _currentLevel = _levels[numberLevel - 1];

        _currentLevel.UpdateLevel();
    }
    #endregion
}