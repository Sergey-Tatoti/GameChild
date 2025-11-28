using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchLevelManagerUI : MonoBehaviour
{
    [Header("Cloud Image")]
    [SerializeField] private Image _cloudImage;
    [SerializeField] private Vector3 _startPositionCloud;
    [SerializeField] private Vector3 _centerPositionCloud;
    [SerializeField] private Vector3 _endPositionCloud;
    [SerializeField] private float _durationMoveCloud;
    [Header("Win Panel Image")]
    [SerializeField] private Image _panelWin;
    [SerializeField] private float _durationFadeWinPanel;
    [SerializeField] private float _maxValueFadeWinPanel;

    public event UnityAction CloudsFilledScene;

    public void UseSwitchAnimation(bool isUseRewardBox)
    {
        _panelWin.raycastTarget = true;
        _panelWin.DOFade(_maxValueFadeWinPanel, _durationFadeWinPanel);

        if (!isUseRewardBox)
            StartCoroutine(UseCloud());
    }

    private IEnumerator UseCloud()
    {
        _cloudImage.transform.localPosition = _startPositionCloud;
        _cloudImage.transform.DOLocalMove(_endPositionCloud, _durationMoveCloud);

        yield return new WaitForSeconds(_durationMoveCloud / 3);

        CloudsFilledScene?.Invoke();

        _panelWin.DOFade(0, _durationFadeWinPanel / 2);
        _panelWin.raycastTarget = false;
    }
}