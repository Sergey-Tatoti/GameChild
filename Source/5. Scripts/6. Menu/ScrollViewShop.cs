using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollViewShop : MonoBehaviour
{
    [SerializeField] private ItemInfo.Type _typeShop;
    [Space]
    [SerializeField] private Button _buttonShop;
    [SerializeField] private Button _buttonArrowUp;
    [SerializeField] private Button _buttonArrowDown;
    [Space]
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _panelShop;
    [SerializeField] private Image _imageMarkNewItem;
    [Space]
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private Scrollbar _scrollbarButaforia;
    [Space]
    [SerializeField] private float _stepChangeScroll = 0.2f;
    [SerializeField] private Sprite _spriteCloseCard;
    [SerializeField] private Sprite _choosedButtonSprite;
    [SerializeField] private Sprite _standartButtonSprite;

    public ItemInfo.Type TypeShop => _typeShop;
    public Button ButtonShop => _buttonShop;

    public event UnityAction<ScrollViewShop> ClickedButtonShop;


    private void OnEnable()
    {
        _buttonShop.onClick.AddListener(() => ClickedButtonShop?.Invoke(this));
        _buttonArrowUp.onClick.AddListener(() => OnClicedButtonArrow(true));
        _buttonArrowDown.onClick.AddListener(() => OnClicedButtonArrow(false));
        _scrollbarButaforia.onValueChanged.AddListener(ChangedValueSlider);
    }

    private void OnDisable()
    {
        _buttonShop.onClick.RemoveListener(() => ClickedButtonShop?.Invoke(this));
        _buttonArrowUp.onClick.RemoveListener(() => OnClicedButtonArrow(true));
        _buttonArrowDown.onClick.RemoveListener(() => OnClicedButtonArrow(false));
        _scrollbarButaforia.onValueChanged.RemoveListener(ChangedValueSlider);

        _scrollbarButaforia.value = 1;
    }

    public void ShowPanel(bool isShow)
    {
        _panelShop.SetActive(isShow);
        GetComponent<Image>().sprite = isShow ? _choosedButtonSprite : _standartButtonSprite;
    }

    public ShopCardView CreateItems(Item item, ShopCardView shopCardPrefab)
    {
        ShopCardView shopCardView = Instantiate(shopCardPrefab, _container.transform);

        shopCardView.SetValue(item, _spriteCloseCard);

        return shopCardView;
    }

    public void UpdateViewShop(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].TypeItem == _typeShop && !items[i].IsShower && items[i].IsOpened)
            {
                ShowMarkNewItem(true);
                return;
            }
        }

        ShowMarkNewItem(false);
    }

    private void ShowMarkNewItem(bool isShow)
    {
        _imageMarkNewItem.gameObject.SetActive(isShow);
        _imageMarkNewItem.GetComponent<ButtonAnimation>().SetShining(isShow);
    }

    private void OnClicedButtonArrow(bool isDown)
    {
        _scrollbar.value = isDown ? _scrollbar.value - _stepChangeScroll : _scrollbar.value + _stepChangeScroll;
    }

    private void ChangedValueSlider(float value) => _scrollbar.value = value;
}