using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerTouchTracker))]
[RequireComponent(typeof(PlayerInventory))]

public class Player : MonoBehaviour
{
    public static int MultiplyOrderInLayer = 100; // Множитель для слоя персонажа относительно тайлов

    [Header("Элементы персонажа")]
    [SerializeField] private SpriteRenderer _spriteCharacterHead;
    [SerializeField] private SpriteRenderer _spriteCharacterBody;
    [SerializeField] private SpriteRenderer _spriteCharacterEyes;
    [SerializeField] private SpriteRenderer _spriteCharacterArmLeft;
    [SerializeField] private SpriteRenderer _spriteCharacterArmRight;
    [SerializeField] private SpriteRenderer _spriteCharacterLegLeft;
    [SerializeField] private SpriteRenderer _spriteCharacterLegRight;
    [SerializeField] private SpriteRenderer _spriteShadow;
    [Header("Элементы одежды персонажа")]
    [SerializeField] private SpriteRenderer _spriteTop;
    [SerializeField] private SpriteRenderer _spriteTopArmLeft;
    [SerializeField] private SpriteRenderer _spriteTopArmRight;
    [SerializeField] private SpriteRenderer _spriteGlasses;
    [SerializeField] private SpriteRenderer _spriteHat;
    [Space]
    [SerializeField] private GameObject _character;
    [Tooltip("Продолжительность движения персонажа от блока к блоку")][SerializeField] private float _durationMove;
    [SerializeField] private SpriteRenderer _keySprite;

    private Vector3 _startPosition;
    private Coroutine _coroutineMove;
    private Animator _animator;
    private PlayerMove _playerMove;
    private PlayerTouchTracker _playerTouchTracker;
    private PlayerInventory _playerInventory;
    private List<Item> _items;

    public event UnityAction TouchedHitBox;
    public event UnityAction TouchedStarLevel;
    public event UnityAction TouchedTeleport;
    public event UnityAction ChangedPosition;
    public event UnityAction<int> TouchedStarExperience;

    private void OnDisable()
    {
        _playerTouchTracker.TouchedHitBox -= OnTouchedHitBox;
        _playerTouchTracker.TouchedStarLevel -= OnTouchedStarLevel;
        _playerTouchTracker.TouchedStarExperience -= OnTouchedStarExperience;
        _playerTouchTracker.TouchedTeleport -= OnTouchedTeleport;
        _playerMove.StepEnded -= OnStepEnded;
        _playerMove.ChangedTargetMove -= OnChangedTargetMove;
        _playerTouchTracker.TouchedKey -= OnTouchedKey;
        _playerTouchTracker.TouchedLock -= OnTouchedLock;
        _playerTouchTracker.ChangedLine -= _playerInventory.OnChangedLine;
    }

    public void SetValue(string name, List<Item> items)
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerTouchTracker = GetComponent<PlayerTouchTracker>();
        _playerInventory = GetComponent<PlayerInventory>();
        _animator = _character.GetComponent<Animator>();

        _playerInventory.SetValue(name, MultiplyOrderInLayer, items, _spriteCharacterBody, _spriteCharacterHead, _spriteCharacterEyes, 
                                  _spriteCharacterArmLeft, _spriteCharacterArmRight, _spriteCharacterLegLeft,
                                  _spriteCharacterLegRight, _spriteShadow, _spriteTop, _spriteTopArmLeft, _spriteTopArmRight,
                                  _spriteGlasses, _spriteHat, _keySprite);

        _playerMove.StepEnded += OnStepEnded;
        _playerMove.ChangedTargetMove += OnChangedTargetMove;
        _playerTouchTracker.TouchedHitBox += OnTouchedHitBox;
        _playerTouchTracker.TouchedStarLevel += OnTouchedStarLevel;
        _playerTouchTracker.TouchedStarExperience += OnTouchedStarExperience;
        _playerTouchTracker.TouchedTeleport += OnTouchedTeleport;
        _playerTouchTracker.TouchedKey += OnTouchedKey;
        _playerTouchTracker.TouchedLock += OnTouchedLock;
        _playerTouchTracker.ChangedLine += _playerInventory.OnChangedLine;
    }

    #region ----- ActionsInLevel -----
    public void UseMove(List<Vector3> directions)
    {
        _animator.SetBool("isRun", true);
        _coroutineMove = StartCoroutine(_playerMove.Move(directions));
    }

    public void OnActivatedLevel(float stepHorizontal, float stepVertical, Vector3 startPosition, bool isRightDirection)
    {
        _playerMove.SetValue(stepHorizontal, stepVertical, _durationMove, isRightDirection, _character);
        _startPosition = startPosition;

        transform.position = _startPosition;
    }

    private void StopedMove()
    {
        StopCoroutine(_coroutineMove);

        _playerMove.StopMove();
        _animator.SetBool("isRun", false);
    }

    private void OnStepEnded()
    {
        transform.position = _startPosition;
        _animator.SetBool("isRun", false);

        _playerInventory.ResetKey();
        TouchedHitBox?.Invoke();
    }

    private void OnChangedTargetMove(Vector3 position) => ChangedPosition?.Invoke();
    #endregion

    #region ----- ActionAfterTouchElements -----

    public void OnTouchedHitBox()
    {
        StopedMove();
        transform.position = _startPosition;

        _playerInventory.ResetKey();
        TouchedHitBox?.Invoke();
        
    }

    private void OnTouchedStarLevel()
    {
        StopCoroutine(_coroutineMove);
        _animator.SetBool("isRun", false);
        _playerInventory.ResetKey();
        TouchedStarLevel?.Invoke();
    }

    private void OnTouchedTeleport(Vector3 position)
    {
        StopCoroutine(_coroutineMove);
        TouchedTeleport?.Invoke();

        transform.DOScale(Vector3.zero, 1f).OnComplete(() =>
        {
            transform.position = position;
            transform.DOScale(Vector3.one, 1f).OnComplete(() => _coroutineMove = StartCoroutine(_playerMove.Move(null)));
        });
    }

    private void OnTouchedKey(GameKey gameKey)
    {
        _playerInventory.SetKey(gameKey);
    }

    private void OnTouchedLock(Lock locked)
    {
       if(!_playerInventory.CheckLock(locked))
           OnTouchedHitBox();
    }

    private void OnTouchedStarExperience(int experience) => TouchedStarExperience?.Invoke(experience);

    #endregion

    #region ----- ActionInventory -----

    public void ChangeName(string name) => _playerInventory.ChangeName(name);

    public void OnChangedCharacter(Item item) => _playerInventory.ChangeCharacterItem(item);

    #endregion
}