using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardScaleAnimation : MonoBehaviour
{
    [SerializeField] private float _startTillingY;
    [SerializeField] private float _maxTillingY;
    [SerializeField] private float _durationTilling;

    private Material _material;

    private void Start()
    {
        Material _material = GetComponent<Image>().material;

        _material.DOTiling(new Vector2(1, _startTillingY), 0);
        _material.DOTiling(new Vector2(1, _maxTillingY), _durationTilling).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
