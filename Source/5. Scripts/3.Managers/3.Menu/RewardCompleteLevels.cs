using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GamePush;

public class RewardCompleteLevels : MonoBehaviour
{
    [SerializeField] private GameObject _panelReward;
    [SerializeField] private Image _finishReward;
    [SerializeField] private TMP_Text _textReward;
    [SerializeField] private Button _buttonCloseReward;
    [SerializeField] private Scrollbar _scrollbarCrossRoad;
    [Space]
    [SerializeField] private Vector3 _scaleActivateReward;
    [SerializeField][TextArea(1, 3)] private string _russianText;
    [SerializeField][TextArea(1, 3)] private string _englishText;

    private void OnEnable()
    {
        _buttonCloseReward.onClick.AddListener(HidePanelReward);

        if(GP_Language.Current() == Language.Russian)
            _textReward.text = _russianText;
        else
            _textReward.text = _englishText;
    }

    private void OnDisable()
    {
        _buttonCloseReward.onClick.RemoveListener(HidePanelReward);
    }

    public void ActivateRewardPanel()
    {
        OpenFinishReward();

        _panelReward.gameObject.SetActive(true);
        _scrollbarCrossRoad.value = 1;
    }

    public void OpenFinishReward()
    {
        _finishReward.transform.localScale = _scaleActivateReward;
        _finishReward.color = Color.white;
    }

    private void HidePanelReward() => _panelReward.gameObject.SetActive(false);
}