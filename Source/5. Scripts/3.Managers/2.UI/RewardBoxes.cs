using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardBoxes : MonoBehaviour
{
    private const string NameAnimationMoveBox = "Move";
    private const string NameAnimationWaitBigBox = "Wait";
    private const string NameAnimationOpenBigBox = "Open";
    private const string NameAnimationCloseBigBox = "Close";
    private const string NameAnimationShowRewardBigBox = "ShowReward";

    [Header("Reward Big Box")]
    [SerializeField] private GameObject _panelReward;
    [SerializeField] private RewardBigBox _bigBoxReward;
    [SerializeField] private Button _bigBoxRewardButton;
    [Header("Reward Box")]
    [SerializeField] private Button _boxRewardButton;
    [SerializeField] private Image _boxShineImage;

    private Animator _animatorBoxReward;
    private Animator _animatorBigBoxReward;
    private Vector3 _endScaleBigRewardBox;

    public event UnityAction ClickedBigBoxReward;
    public event UnityAction PlayedAnimationBigBoxReward;

    private void OnEnable()
    {
        _bigBoxRewardButton.onClick.AddListener(OnClickedBigBoxRewardButton);
        _bigBoxReward.RestartedAnimationWait += OnRestartedAnimationWait;
    }

    private void OnDisable()
    {
        _bigBoxRewardButton.onClick.AddListener(OnClickedBigBoxRewardButton);
        _bigBoxReward.RestartedAnimationWait -= OnRestartedAnimationWait;
    }

    public void SetValue()
    {
        _animatorBoxReward = _boxRewardButton.GetComponent<Animator>();
        _animatorBigBoxReward = _bigBoxReward.GetComponent<Animator>();

        _endScaleBigRewardBox = _bigBoxReward.transform.localScale;
    }

    public void ActivateBoxesRewards(float durationChangeScaleBox)
    {
        _boxShineImage.gameObject.SetActive(true);
        _animatorBoxReward.SetTrigger(NameAnimationMoveBox);

        StartCoroutine(ShowBigBoxReward(durationChangeScaleBox));
    }

    public void PlayAnimationShowCard()
    {
        _animatorBigBoxReward.Play(NameAnimationShowRewardBigBox, -1, 0f);
    }

    public void HideRewards(float durationChangeScaleBox)
    {
        _animatorBigBoxReward.SetTrigger(NameAnimationCloseBigBox);

        StartCoroutine(WaitCloseBigBoxReward());
    }

    private void OnRestartedAnimationWait() => PlayedAnimationBigBoxReward?.Invoke();

    private IEnumerator ShowBigBoxReward(float durationChangeScaleBox)
    {
        yield return new WaitForSeconds(_animatorBoxReward.GetCurrentAnimatorClipInfo(0).Length);

        _panelReward.SetActive(true);
        _bigBoxRewardButton.interactable = true;
        _bigBoxReward.gameObject.SetActive(true);
    }

    private void OnClickedBigBoxRewardButton()
    {
        _bigBoxRewardButton.interactable = false;
        _animatorBigBoxReward.SetTrigger(NameAnimationOpenBigBox);
        _boxShineImage.gameObject.SetActive(false);

        StartCoroutine(WaitOpenBigBoxReward());
    }

    private IEnumerator WaitOpenBigBoxReward()
    {
        yield return new WaitForSeconds(_animatorBigBoxReward.GetCurrentAnimatorClipInfo(0).Length);

        ClickedBigBoxReward?.Invoke();
    }

    private IEnumerator WaitCloseBigBoxReward()
    {
        yield return new WaitForSeconds(_animatorBigBoxReward.GetCurrentAnimatorClipInfo(0).Length);

        _panelReward.SetActive(false);
        _bigBoxReward.gameObject.SetActive(false);
    }
}