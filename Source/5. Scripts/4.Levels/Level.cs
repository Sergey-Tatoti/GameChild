using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private StarLevel _starLevel;
    [SerializeField] private LevelInfo _levelInfo;
    [Header("Если на уровнях есть ключи, замки, стрелки: ")]
    [SerializeField] private List<GameKey> _keys;
    [SerializeField] private List<Lock> _locks;
    [SerializeField] private List<TutorialArrow> _tutorialArrows;


    public int Number => _levelInfo.Number;
    public int CountExperience => _levelInfo.CountExperience;
    public float PlayerStepHorizontal => _levelInfo.PlayerStepHorizontal;
    public float PlayerStepVertical => _levelInfo.PlayerStepVertical;
    public bool IsRightPlayerDirection => _levelInfo.IsRightPlayerDirection;
    public Vector3 PlayerStartPosition => _levelInfo.PlayerStartPosition;

    public void SetValue()
    {
        _starLevel.SetValue();
    }

    public void UpdateLevel()
    {
        _starLevel.ResetStar();

        ActivatorKey();
        ActivatorLock();
    }

    public void ShowArrow(bool isShow, int index, Vector2 direction)
    {
        if (_tutorialArrows.Count > 0)
            _tutorialArrows[index].ShowArrow(isShow, direction);
    }

    public void HideAllArrows()
    {
        for (int i = 0; i < _tutorialArrows.Count; i++) { _tutorialArrows[i].gameObject.SetActive(false); }
    }

    private void ActivatorKey()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            _keys[i].gameObject.SetActive(true);
            _keys[i].SetValue();
        }
    }

    private void ActivatorLock()
    {
        for (int i = 0; i < _locks.Count; i++)
        {
            _locks[i].gameObject.SetActive(true);
        }
    }
}