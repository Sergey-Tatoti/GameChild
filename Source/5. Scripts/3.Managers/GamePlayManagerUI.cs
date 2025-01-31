using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayManagerUI : MonoBehaviour
{
    [Header("Game Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _backShopButton;
    [SerializeField] private Button _resetStepButton;
    [Header("Arrow Buttons")]
    [SerializeField] private Button _arrowRightButton;
    [SerializeField] private Button _arrowLeftButton;
    [SerializeField] private Button _arrowUpButton;
    [SerializeField] private Button _arrowDownButton;
    [Header("Reward Buttons")]
    [SerializeField] private Button _boxRewardButton;
    [SerializeField] private Button _bigBoxRewardButton;
    [SerializeField] private Image _boxShineImage;
    [Tooltip(" онечный размер большого подарка")][SerializeField] private Vector3 _endScaleBigRewardBox;
    [Tooltip("ѕродолжительность роста подарка от 0 до end")][SerializeField] private float _durationChangeScaleRewardBox;
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
    [Header("Panels")]
    [SerializeField] private GameObject _panelReward;
    [SerializeField] private Image _panelWin;
    [SerializeField] private GameObject _panelShop;

    public Button ArrowRight => _arrowRightButton;
    public Button ArrowLeft => _arrowLeftButton;
    public Button ArrowDown => _arrowDownButton;
    public Button ArrowUp => _arrowUpButton;
    public Button StartSteps => _playButton;
    public Button ResetStepButton => _resetStepButton;

    public event UnityAction ClickedButton;
    public event UnityAction ClickedButtonPlay;
    public event UnityAction ClickedButtonShop;
    public event UnityAction ClickedButtonResetStep;
    public event UnityAction ClickedRewardBox;
    public event UnityAction<Vector3> ClickedButtonArrow;
    public event UnityAction CannedShowNextLevel;
    public event UnityAction ClickedButtonMainMenu;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(() => OnClickedButtonPlay());
        _shopButton.onClick.AddListener(() => ShowShopPanel(true));
        _backShopButton.onClick.AddListener(() => ShowShopPanel(false));
        _resetStepButton.onClick.AddListener(() => OnClickedButtonResetStep());
        _arrowRightButton.onClick.AddListener(() => OnClickedButtonArrow(Vector3.right));
        _arrowLeftButton.onClick.AddListener(() => OnClickedButtonArrow(Vector3.left));
        _arrowUpButton.onClick.AddListener(() => OnClickedButtonArrow(Vector3.up));
        _arrowDownButton.onClick.AddListener(() => OnClickedButtonArrow(Vector3.down));

        _bigBoxRewardButton.onClick.AddListener(OnClickedBigBoxRewardButton);
        _boxRewardButton.onClick.AddListener(() => ShowRewardPanel());
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(() => OnClickedButtonPlay());
        _shopButton.onClick.RemoveListener(() => ShowShopPanel(true));
        _backShopButton.onClick.AddListener(() => ShowShopPanel(false));
        _resetStepButton.onClick.RemoveListener(() => OnClickedButtonResetStep());
        _arrowRightButton.onClick.RemoveListener(() => OnClickedButtonArrow(Vector3.right));
        _arrowLeftButton.onClick.RemoveListener(() => OnClickedButtonArrow(Vector3.left));
        _arrowUpButton.onClick.RemoveListener(() => OnClickedButtonArrow(Vector3.up));
        _arrowDownButton.onClick.RemoveListener(() => OnClickedButtonArrow(Vector3.down));

        _bigBoxRewardButton.onClick.AddListener(OnClickedBigBoxRewardButton);
        _boxRewardButton.onClick.AddListener(() => ShowRewardPanel());
    }

    public void ActivateBoxRewardButton(bool isActivate)
    {
        _playButton.interactable = !isActivate;
        _boxRewardButton.interactable = isActivate;
        _boxShineImage.gameObject.SetActive(isActivate);
        _boxRewardButton.GetComponent<Animator>().SetBool("IsOpenReward", isActivate);
    }

    #region ----- ShowPanels -----

    public IEnumerator ShowWinPanel(int timeShow, bool isShowSmile)
    {
        yield return new WaitForSeconds(timeShow);

        _panelReward.SetActive(false);
        _panelWin.raycastTarget = true;
        _bigBoxRewardButton.interactable = true;
        _panelWin.DOFade(_maxValueFadeWinPanel, _durationFadeWinPanel);

        TryUseSmile(isShowSmile);
    }

    public void ShowRewardPanel()
    {
        ActivateBoxRewardButton(false);

        _panelReward.SetActive(true);
        _bigBoxRewardButton.transform.DOScale(_endScaleBigRewardBox, _durationChangeScaleRewardBox);
        ClickedButton?.Invoke();
    }

    private void ShowShopPanel(bool isShow)
    {
        _panelShop.SetActive(isShow);
        ClickedButton?.Invoke();
    }
    #endregion

    #region ----- ClickedButtons -----

    private void OnClickedButtonArrow(Vector3 direction)
    {
        ClickedButtonArrow.Invoke(direction);
        ClickedButton?.Invoke();
    }

    private void OnClickedButtonResetStep()
    {
        ClickedButtonResetStep.Invoke();
        ClickedButton?.Invoke();
    }

    private void OnClickedButtonPlay()
    {
        ClickedButtonPlay.Invoke();
        ClickedButton?.Invoke();
    }

    private void OnClickedBigBoxRewardButton()
    {
        _bigBoxRewardButton.interactable = false;
        _bigBoxRewardButton.transform.DOScale(Vector3.zero, _durationChangeScaleRewardBox);
        ClickedRewardBox?.Invoke();
    }
    #endregion

    #region ----- UseAnimation -----

    private void UseCloud()
    {
        _cloudImage.transform.localPosition = _startPositionCloud;
        _cloudImage.transform.DOLocalMove(_centerPositionCloud, _durationMoveCloud / 2).OnComplete(() =>
        {
            CannedShowNextLevel?.Invoke();

            _panelWin.DOFade(0, _durationFadeWinPanel / 2);
            _panelWin.raycastTarget = false;
            _cloudImage.transform.DOLocalMove(_endPositionCloud, _durationMoveCloud);
        });
    }

    private void TryUseSmile(bool isShowSmile)
    {
        _smileImage.rectTransform.localPosition = _startPositionSmile;

        if (isShowSmile)
            _smileImage.transform.DOLocalMove(_endPositionSmile, _durationMoveSmile).OnComplete(() => UseCloud());
        else
            UseCloud();
    }

    #endregion
}