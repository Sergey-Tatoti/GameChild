using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private StarLevel _starLevel;
    [SerializeField] private LevelInfo _levelInfo;
    [Header("Если на уровнях есть ключи и замки: ")]
    [SerializeField] private List<GameKey> _keys;
    [SerializeField] private List<Lock> _locks;


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