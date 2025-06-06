    using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("��������� ������� ��� ��������� �� ������")]
    [SerializeField] private Vector3 _scale;
    [SerializeField] private float _durationChangeScale = 0.5f;
    [SerializeField] private bool _canChangeScale = true;
    [Header("��������� ������� ��� ��������� �� ������")]
    [SerializeField] private Vector3 _position;
    [SerializeField] private float _durationChangePosition = 0.5f;
    [SerializeField] private bool _canChangePosition;
    [Header("������ ��������")]
    [SerializeField] private GameObject _finger;
    [SerializeField] private Vector3 _scaleSmall;
    [SerializeField] private Vector3 _scaleBig;
    [SerializeField] private float _durationShine;

    private Vector3 _startPosition;
    private Vector3 _startScale;

    private void Awake()
    {
        _startPosition = transform.localPosition;
        _startScale = transform.localScale;
    }

    public void ResetAnimation() => DOTween.Kill(this);

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canChangeScale) { transform.DOScale(_startScale, _durationChangeScale); }
        if (_canChangePosition) { transform.DOLocalMove(_startPosition, _durationChangePosition); }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_canChangeScale) { transform.DOScale(_scale, _durationChangeScale); }
        if (_canChangePosition) { transform.DOLocalMove(_position, _durationChangePosition); }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_canChangeScale) { transform.DOScale(_startScale, _durationChangeScale); }
        if (_canChangePosition) { transform.DOLocalMove(_startPosition, _durationChangePosition); }
    }

    public void SetShining(bool shining)
    {
        if (_finger != null)
            ShineFinger(shining);
        else
            ShineButton(shining);
    }

    private void ShineFinger(bool shining)
    {
        _finger.SetActive(shining);
        _finger.transform.localScale = _scaleSmall;

        if (shining)
            _finger.transform.DOScale(_scaleBig, _durationShine).SetLoops(-1, LoopType.Yoyo);
        else
            _finger.transform.DOKill(this);
    }

    private void ShineButton(bool shining)
    {
        transform.localScale = _scaleSmall;

        if (shining)
            transform.DOScale(_scaleBig, _durationShine).SetLoops(-1, LoopType.Yoyo);
        else
            transform.DOKill(this);
    }
}