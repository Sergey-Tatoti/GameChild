using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewShop : MonoBehaviour
{
    [SerializeField] private ItemInfo.Type _typeShop;
    [SerializeField] private Button _buttonArrowUp;
    [SerializeField] private Button _buttonArrowDown;
    [SerializeField] private Image _imageMarkNewItem;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private Scrollbar _scrollbarButaforia;
    [SerializeField] private GameObject _container;
    [Space]
    [SerializeField] private float _stepChangeScroll = 0.2f;
    [SerializeField] private Sprite _spriteCloseCard;

    private void OnEnable()
    {
        _buttonArrowUp.onClick.AddListener(() => OnClicedButtonArrow(true));
        _buttonArrowDown.onClick.AddListener(() => OnClicedButtonArrow(false));
        _scrollbarButaforia.onValueChanged.AddListener(ChangedValueSlider);
    }

    private void OnDisable()
    {
        _buttonArrowUp.onClick.RemoveListener(() => OnClicedButtonArrow(true));
        _buttonArrowDown.onClick.RemoveListener(() => OnClicedButtonArrow(false));
        _scrollbarButaforia.onValueChanged.RemoveListener(ChangedValueSlider);

        _scrollbarButaforia.value = 1;
    }

    public ShopCardView CreateItems(Item item, ShopCardView shopCardPrefab)
    {
        ShopCardView shopCardView = Instantiate(shopCardPrefab, _container.transform);

        shopCardView.SetValue(item, _spriteCloseCard);

        ShowMarkNewItem(!item.IsShower);

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

    private void ShowMarkNewItem(bool isShow) => _imageMarkNewItem.gameObject.SetActive(isShow);

    private void OnClicedButtonArrow(bool isDown)
    {
        _scrollbar.value = isDown ? _scrollbar.value - _stepChangeScroll : _scrollbar.value + _stepChangeScroll;
    }

    private void ChangedValueSlider(float value) => _scrollbar.value = value;
}