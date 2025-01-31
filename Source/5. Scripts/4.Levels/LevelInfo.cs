using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Create/new LevelInfo", order = -51)]

public class LevelInfo : ScriptableObject
{
    [Tooltip("Номер уровня")]
    [SerializeField] private int _number = 1;
    [Tooltip("Кол-во опыта за прохождение")]
    [SerializeField] private int _countExperience;
    [Tooltip("Шаг персонажа от блока к блоку")]
    [SerializeField] private float _playerStepHorizontal;
    [SerializeField] private float _playerStepVertical;
    [Tooltip("Стартовая позиция спавна на уровне")]
    [SerializeField] private Vector3 _playerStartPosition;
    [Tooltip("Куда смотрит игрок вначале уровня")]
    [SerializeField] private bool _isRightPlayerDirection;

    public int Number => _number;
    public int CountExperience => _countExperience;
    public float PlayerStepHorizontal => _playerStepHorizontal;
    public float PlayerStepVertical => _playerStepVertical;
    public bool IsRightPlayerDirection => _isRightPlayerDirection;
    public Vector3 PlayerStartPosition => _playerStartPosition;
}