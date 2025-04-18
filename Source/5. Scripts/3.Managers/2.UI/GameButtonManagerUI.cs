using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameButtonManagerUI : MonoBehaviour
{
    [Header("Game Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _backShopButton;
    [SerializeField] private Button _resetStepButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _lampButton;
    [Header("Arrow Buttons")]
    [SerializeField] private Button _arrowRightButton;
    [SerializeField] private Button _arrowLeftButton;
    [SerializeField] private Button _arrowUpButton;
    [SerializeField] private Button _arrowDownButton;

    public Button ArrowRight => _arrowRightButton;
    public Button ArrowLeft => _arrowLeftButton;
    public Button ArrowDown => _arrowDownButton;
    public Button ArrowUp => _arrowUpButton;
    public Button StartSteps => _playButton;
    public Button ResetStepButton => _resetStepButton;
    public Button ShopButton => _shopButton;
    public Button BackMenuButton => _backMenuButton;
    public Button BackShopButton => _backShopButton;
    public Button LampButton => _lampButton;

    public event UnityAction ClickedButtonPlay;
    public event UnityAction ClickedButtonResetStep;
    public event UnityAction ClickedButtonBackMenu;
    public event UnityAction ClickedButtonLamp;
    public event UnityAction<bool> ClickedButtonShop;
    public event UnityAction<Vector3> ClickedButtonArrow;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(ClickedPlay);
        _backMenuButton.onClick.AddListener(() => ClickedButtonBackMenu?.Invoke());
        _lampButton.onClick.AddListener(() => ClickedButtonLamp?.Invoke());
        _shopButton.onClick.AddListener(() => ClickedButtonShop?.Invoke(true));
        _backShopButton.onClick.AddListener(() => ClickedButtonShop?.Invoke(false));
        _resetStepButton.onClick.AddListener(() => ClickedButtonResetStep?.Invoke());
        _arrowRightButton.onClick.AddListener(() => ClickedButtonArrow.Invoke(Vector3.right));
        _arrowLeftButton.onClick.AddListener(() => ClickedButtonArrow.Invoke(Vector3.left));
        _arrowUpButton.onClick.AddListener(() => ClickedButtonArrow.Invoke(Vector3.up));
        _arrowDownButton.onClick.AddListener(() => ClickedButtonArrow.Invoke(Vector3.down));
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(ClickedPlay);
        _backMenuButton.onClick.RemoveListener(() => ClickedButtonBackMenu?.Invoke());
        _lampButton.onClick.RemoveListener(() => ClickedButtonLamp?.Invoke());
        _shopButton.onClick.RemoveListener(() => ClickedButtonShop?.Invoke(true));
        _backShopButton.onClick.RemoveListener(() => ClickedButtonShop?.Invoke(false));
        _resetStepButton.onClick.RemoveListener(() => ClickedButtonResetStep?.Invoke());
        _arrowRightButton.onClick.RemoveListener(() => ClickedButtonArrow.Invoke(Vector3.right));
        _arrowLeftButton.onClick.RemoveListener(() => ClickedButtonArrow.Invoke(Vector3.left));
        _arrowUpButton.onClick.RemoveListener(() => ClickedButtonArrow.Invoke(Vector3.up));
        _arrowDownButton.onClick.RemoveListener(() => ClickedButtonArrow.Invoke(Vector3.down));
    }

    private void ClickedPlay() => ClickedButtonPlay?.Invoke();

    public void ActivateActionButton(bool isActivate)
    {
        _playButton.interactable = isActivate;
        _resetStepButton.interactable = isActivate;
        _arrowRightButton.interactable = isActivate;
        _arrowLeftButton.interactable = isActivate;
        _arrowUpButton.interactable = isActivate;
        _arrowDownButton.interactable = isActivate;
    }

    public void ActivateButtonLamp(bool isActivate)
    {
        _lampButton.gameObject.SetActive(isActivate);
        _lampButton.GetComponent<ButtonAnimation>().SetShining(isActivate);
    }
}