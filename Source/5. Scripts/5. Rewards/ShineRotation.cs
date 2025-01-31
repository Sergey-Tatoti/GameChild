using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineRotation : MonoBehaviour
{
    [SerializeField] private float _durationRotate = 1f;

    private void OnEnable()
    {
        UseRotate();
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }

    private void UseRotate()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360), _durationRotate, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }
}