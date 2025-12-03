using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StarLevel : GameElement
{
    [Header("Разместить изначально кусочки в те места, куда они должны придти и размер")]
    [SerializeField] private List<SpriteRenderer> _piecesStar;
    [SerializeField] private Lock _lock;
    [SerializeField] private SpriteRenderer _shine;
    [Header("Настройки кусочков звезды")]
    [SerializeField] private float _durationMovePiecesPositions;
    [SerializeField] private float _durationMovePiecesPoint;
    [SerializeField] private float _durationChangeScale;
    [Header("Настройки кусочков звезды")]
    [SerializeField] private float _stepMoveStar;
    [SerializeField] private float _durationMoveStar;

    private SpriteRenderer _spriteStar;
    private Vector3 _startPosition;
    private bool _isActivateShine;
    private List<Vector3> _postitionsPiecesStar = new List<Vector3>();
    private List<Vector3> _scalesPiecesStar = new List<Vector3>();

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - _stepMoveStar / 2, transform.position.z);
        transform.DOMoveY(transform.position.y + _stepMoveStar, _durationMoveStar).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }

    public void SetValue(bool isHideShine)
    {
        _spriteStar = GetComponent<SpriteRenderer>();
        _startPosition = transform.position;

        for (int i = 0; i < _piecesStar.Count; i++)
        {
            _postitionsPiecesStar.Add(_piecesStar[i].transform.position);
            _scalesPiecesStar.Add(_piecesStar[i].transform.localScale);
            _piecesStar[i].transform.position = Vector3.zero;
            _piecesStar[i].gameObject.SetActive(false);
        }

        SetActivateShineStar(isHideShine);
    }

    public void SetActivateShineStar(bool isHideShine)
    {
        _isActivateShine = !isHideShine;
        ShowShineStar(false);
    }

    public void Activate()
    {
        ShowShineStar(false);
        ShowStar(false);

        if (!_isActivateShine)
            return;

        for (int i = 0; i < _piecesStar.Count; i++)
        {
            _piecesStar[i].gameObject.SetActive(true);
            MovePiece(_piecesStar[i], _postitionsPiecesStar[i]);
        }
    }

    public void ResetStar()
    {
        ShowShineStar(_isActivateShine);
        ShowStar(true);
        TryActivateLock(true);

        for (int i = 0; i < _piecesStar.Count; i++)
        {
            _piecesStar[i].gameObject.SetActive(false);
            _piecesStar[i].transform.localScale = _scalesPiecesStar[i];
            _piecesStar[i].transform.localPosition = Vector3.zero;
        }
    }

    private void MovePiece(SpriteRenderer piece, Vector3 positionPiece)
    {
        piece.transform.DOMove(positionPiece, _durationMovePiecesPositions).OnComplete(() =>
        {
            piece.transform.DOMove(RewardBoxes.PositionBoxReward, _durationMovePiecesPoint);
            piece.transform.DOScale(0, _durationChangeScale);
        });
    }

    private void ShowShineStar(bool isShow)
    {
        int alphaColor = isShow ? 1 : 0;

        _shine.color = new Color(_spriteStar.color.r, _spriteStar.color.g, _spriteStar.color.b, alphaColor);
    }

    private void ShowStar(bool isShow)
    {
        int alphaColor = isShow ? 1 : 0;

        _spriteStar.color = new Color(_spriteStar.color.r, _spriteStar.color.g, _spriteStar.color.b, alphaColor);
    }

    private void TryActivateLock(bool isActivate)
    {
        if (_lock != null)
            _lock.gameObject.SetActive(isActivate);
    }
}