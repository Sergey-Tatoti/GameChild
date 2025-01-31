using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Create/new LevelInfo", order = -51)]

public class LevelInfo : ScriptableObject
{
    [Tooltip("����� ������")]
    [SerializeField] private int _number = 1;
    [Tooltip("���-�� ����� �� �����������")]
    [SerializeField] private int _countExperience;
    [Tooltip("��� ��������� �� ����� � �����")]
    [SerializeField] private float _playerStepHorizontal;
    [SerializeField] private float _playerStepVertical;
    [Tooltip("��������� ������� ������ �� ������")]
    [SerializeField] private Vector3 _playerStartPosition;
    [Tooltip("���� ������� ����� ������� ������")]
    [SerializeField] private bool _isRightPlayerDirection;

    public int Number => _number;
    public int CountExperience => _countExperience;
    public float PlayerStepHorizontal => _playerStepHorizontal;
    public float PlayerStepVertical => _playerStepVertical;
    public bool IsRightPlayerDirection => _isRightPlayerDirection;
    public Vector3 PlayerStartPosition => _playerStartPosition;
}