using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchLevelManagerUI : MonoBehaviour
{
    [SerializeField] private Image _panelWin;
    [Header("Cloud Image")]
    [SerializeField] private Image _cloudImage;
    [SerializeField] private Vector3 _startPositionCloud;
    [SerializeField] private Vector3 _centerPositionCloud;
    [SerializeField] private Vector3 _endPositionCloud;
    [SerializeField] private float _durationMoveCloud;
    [Header("Smile Image")]
    [SerializeField] private Image _smileImage;
    [SerializeField] private Vector3 _startPositionSmile;
    [SerializeField] private Vector3 _endPositionSmile;
    [SerializeField] private float _durationMoveSmile;
    [SerializeField] private float _durationFadeWinPanel;
    [SerializeField] private float _maxValueFadeWinPanel;

    public event UnityAction CloudsFilledScene;

    public void UseSwitchAnimation(bool isShowSmile)
    {
        _panelWin.raycastTarget = true;
        _panelWin.DOFade(_maxValueFadeWinPanel, _durationFadeWinPanel);

        if (isShowSmile)
            UseSmile();
        else
            UseCloud();
    }

    private void UseCloud()
    {
        _cloudImage.transform.localPosition = _startPositionCloud;
        _cloudImage.transform.DOLocalMove(_centerPositionCloud, _durationMoveCloud / 2).OnComplete(() =>
        {
            CloudsFilledScene?.Invoke();

            _panelWin.DOFade(0, _durationFadeWinPanel / 2);
            _panelWin.raycastTarget = false;
            _cloudImage.transform.DOLocalMove(_endPositionCloud, _durationMoveCloud);
        });
    }

    private void UseSmile()
    {
        _smileImage.rectTransform.localPosition = _startPositionSmile;
        _smileImage.transform.DOLocalMove(_endPositionSmile, _durationMoveSmile).OnComplete(() => UseCloud());
    }
}