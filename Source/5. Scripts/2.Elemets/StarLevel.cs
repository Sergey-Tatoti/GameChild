using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StarLevel : GameElement
{
    [Header("Разместить изначально кусочки в те места, куда они должны придти и размер")]
    [SerializeField] private List<SpriteRenderer> _piecesStar;
    [SerializeField] private SpriteRenderer _shine;
    [SerializeField] private float _durationMovePiecesPositions;
    [SerializeField] private float _durationMovePiecesPoint;
    [SerializeField] private float _durationChangeScale;

    private SpriteRenderer _spriteStar;
    private Vector3 _pointMovePieces;
    private List<Vector3> _postitionsPiecesStar = new List<Vector3>();
    private List<Vector3> _scalesPiecesStar = new List<Vector3>();

    public void SetValue()
    {
        _spriteStar = GetComponent<SpriteRenderer>();

        SetPointMovePieces();

        for (int i = 0; i < _piecesStar.Count; i++)
        {
            _postitionsPiecesStar.Add(_piecesStar[i].transform.position);
            _scalesPiecesStar.Add(_piecesStar[i].transform.localScale);
            _piecesStar[i].transform.position = Vector3.zero;
        }
    }

    public void Activate()
    {
        _spriteStar.color = new Color(_spriteStar.color.r, _spriteStar.color.g, _spriteStar.color.b, 0);
        _shine.color = new Color(_spriteStar.color.r, _spriteStar.color.g, _spriteStar.color.b, 0);

        for (int i = 0; i < _piecesStar.Count; i++)
        {
            MovePiece(_piecesStar[i], _postitionsPiecesStar[i]);
        }
    }

    public void ResetStar()
    {
        _spriteStar.color = new Color(_spriteStar.color.r, _spriteStar.color.g, _spriteStar.color.b, 1);
        _shine.color = new Color(_spriteStar.color.r, _spriteStar.color.g, _spriteStar.color.b, 1);

        for (int i = 0; i < _piecesStar.Count; i++)
        {
            _piecesStar[i].transform.localScale = _scalesPiecesStar[i];
            _piecesStar[i].transform.localPosition = Vector3.zero;
        }
    }

    private void MovePiece(SpriteRenderer piece, Vector3 positionPiece)
    {
        piece.transform.DOMove(positionPiece, _durationMovePiecesPositions).OnComplete(() =>
        {
            piece.transform.DOMove(_pointMovePieces, _durationMovePiecesPoint);
            piece.transform.DOScale(0, _durationChangeScale);
        });
    }

    private void SetPointMovePieces()
    {
        Vector3 rightCenterScreenPoint = new Vector3(Screen.width, Screen.height / 2, 0);

        _pointMovePieces = Camera.main.ScreenToWorldPoint(rightCenterScreenPoint);
        _pointMovePieces.z = 0; // Установка Z координаты в 0, если это 2D
    }
}