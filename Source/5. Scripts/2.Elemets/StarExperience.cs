using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarExperience : GameElement
{
    [SerializeField] private int _countExperience;
    [SerializeField] private float _stepMoveStar;
    [SerializeField] private float _durationMoveStar;

    private Vector3 _startPosition;

    public int CountExperience => _countExperience;

    private void Awake() => _startPosition = transform.position;

    private void OnEnable()
    {
        transform.position = new Vector3(_startPosition.x, _startPosition.y - _stepMoveStar / 2, _startPosition.z);
        transform.DOMoveY(transform.position.y + _stepMoveStar, _durationMoveStar).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}