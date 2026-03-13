using GamePush;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ADScontroller : MonoBehaviour
{
    [SerializeField] private Button _adsButtonMenu;
    [SerializeField] private Button _adsButtonMap;
    [Header("Меню с кнопкой покупки")]
    [SerializeField] private GameObject _adsBuyPanel;
    [SerializeField] private Button _adsBuyButton;
    [SerializeField] private Button _adsReturnButton;
    [SerializeField] private TMP_Text _textPrice;
    [Header("Меню с проверкой")]
    [SerializeField] private GameObject _adsCheckPanel;
    [SerializeField] private Button _adsCheckButtonBuy;
    [SerializeField] private Button _adsCheckButtonReturn;
    [SerializeField] private TMP_InputField _adsInputField;
    [SerializeField] private string _resultCheck;

    private SoundManager _soundManager;

    public event UnityAction ClickedBuyAds;

    private void OnEnable()
    {
        _adsButtonMenu.onClick.AddListener(() => ShowAdsBuyPanel(true));
        _adsButtonMap.onClick.AddListener(() => ShowAdsBuyPanel(true));
        _adsBuyButton.onClick.AddListener(AttemptBuyAds);

        _adsReturnButton.onClick.AddListener(() => ShowAdsBuyPanel(false));
        _adsCheckButtonBuy.onClick.AddListener(AttemptBuyAds);
        _adsCheckButtonReturn.onClick.AddListener(() => ShowCheckPanel(false));
    }

    private void OnDisable()
    {
        _adsButtonMenu.onClick.RemoveListener(() => ShowAdsBuyPanel(true));
        _adsButtonMap.onClick.RemoveListener(() => ShowAdsBuyPanel(true));
        _adsBuyButton.onClick.RemoveListener(AttemptBuyAds);

        _adsReturnButton.onClick.RemoveListener(() => ShowAdsBuyPanel(false));
        _adsCheckButtonBuy.onClick.RemoveListener(AttemptBuyAds);
        _adsCheckButtonReturn.onClick.RemoveListener(() => ShowCheckPanel(false));
    }

    public void SetBaseValues(SoundManager soundManager) => _soundManager = soundManager;

    public void SetPrice(string price)
    {
        if (GP_Platform.Type() == Platform.VK)
            _textPrice.text = price + " гол.";
        else
            _textPrice.text = price;
    }

    public void SetLoadingValues(bool isBuyAds)
    {
        _adsButtonMenu.gameObject.SetActive(!isBuyAds);
        _adsButtonMap.gameObject.SetActive(!isBuyAds);

        _adsBuyPanel.gameObject.SetActive(false);
        _adsCheckPanel.gameObject.SetActive(false);
    }

    private void ShowAdsBuyPanel(bool isShow)
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
        _adsBuyPanel.SetActive(isShow);
    }

    private void ShowCheckPanel(bool isShow)
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);
        _adsCheckPanel.SetActive(isShow);
    }

    private void AttemptBuyAds()
    {
        _soundManager.PlaySound(SoundManager.TypeSound.ClickButton);

        if (_resultCheck == _adsInputField.text)
        {
            ClickedBuyAds?.Invoke();
        }
    }
}
