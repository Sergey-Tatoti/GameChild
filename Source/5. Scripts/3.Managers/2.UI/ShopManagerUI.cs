using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] private List<Avatar> _avatars;
    [Space]
    [SerializeField] private Button _buttonCharacter;
    [SerializeField] private Button _buttonTop;
    [SerializeField] private Button _buttonGlasses;
    [SerializeField] private Button _buttonCap;
    [SerializeField] private Button _buttonRamka;
    [SerializeField] private Button _buttonGround;
    [Space]
    [SerializeField] private Sprite _choosedButtonSprite;
    [SerializeField] private Sprite _standartButtonSprite;
    [Space]
    [SerializeField] private ScrollViewShop _scrollViewCharacter;
    [SerializeField] private ScrollViewShop _scrollViewTop;
    [SerializeField] private ScrollViewShop _scrollViewGlasses;
    [SerializeField] private ScrollViewShop _scrollViewCap;
    [SerializeField] private ScrollViewShop _scrollViewRamka;
    [SerializeField] private ScrollViewShop _scrollViewGround;

    private List<ShopCardView> _shopCardViews = new List<ShopCardView>();
    private List<ShopCardView> _currentsCardView = new List<ShopCardView>();

    public event UnityAction<Item, Item> ChangedSelectedItem;
    public event UnityAction ClickedButtonShowScrollView;

    private void OnEnable()
    {
        _buttonCharacter.onClick.AddListener(() => ShowScrollViews(true, false, false, false, false, false));
        _buttonTop.onClick.AddListener(() => ShowScrollViews(false, true, false, false, false, false));
        _buttonGlasses.onClick.AddListener(() => ShowScrollViews(false, false, true, false, false, false));
        _buttonCap.onClick.AddListener(() => ShowScrollViews(false, false, false, true, false, false));
        _buttonRamka.onClick.AddListener(() => ShowScrollViews(false, false, false, false, true, false));
        _buttonGround.onClick.AddListener(() => ShowScrollViews(false, false, false, false, false, true));
    }

    private void OnDisable()
    {
        _buttonCharacter.onClick.RemoveListener(() => ShowScrollViews(true, false, false, false, false, false));
        _buttonTop.onClick.RemoveListener(() => ShowScrollViews(false, true, false, false, false, false));
        _buttonGlasses.onClick.RemoveListener(() => ShowScrollViews(false, false, true, false, false, false));
        _buttonCap.onClick.RemoveListener(() => ShowScrollViews(false, false, false, true, false, false));
        _buttonRamka.onClick.RemoveListener(() => ShowScrollViews(false, false, false, false, true, false));
        _buttonGround.onClick.RemoveListener(() => ShowScrollViews(false, false, false, false, false, true));
    }

    public void SetValue(List<Item> items, ShopCardView shopCardView)
    {
        _scrollViewCharacter.gameObject.SetActive(true);
        _buttonCharacter.GetComponent<Image>().sprite = _choosedButtonSprite;

        FillListShopCards(items, shopCardView);
    }

    public void ChangeSpriteItem(Item item)
    {
        for (int i = 0; i < _avatars.Count; i++) { _avatars[i].ChangeSpriteItem(item); }
    }

    #region ----- ActionsShopCards -----

    public void UpdateShopCards(List<Item> items)
    {
        UpdatesShopCardViews();
        UpdateAvatars(items);

        _scrollViewCharacter.UpdateViewShop(items);
        _scrollViewTop.UpdateViewShop(items);
        _scrollViewCap.UpdateViewShop(items);
        _scrollViewGlasses.UpdateViewShop(items);
        _scrollViewRamka.UpdateViewShop(items);
        _scrollViewGround.UpdateViewShop(items);
    }

    private void UpdatesShopCardViews()
    {
        for (int i = 0; i < _shopCardViews.Count; i++)
        {
            _shopCardViews[i].UpdateInfo();
        }
    }

    private void UpdateAvatars(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].IsShower && items[i].IsOpened)
            {
                ShowMarkNewItemAvatars(true);
                return;
            }
        }

        ShowMarkNewItemAvatars(false);
    }

    private void ShowMarkNewItemAvatars(bool isShowMark)
    {
        for (int i = 0; i < _avatars.Count; i++)
        {
            _avatars[i].ShowMarkNewitem(isShowMark);
        }
    }

    private void FillListShopCards(List<Item> items, ShopCardView shopCardView)
    {
        for (int i = 0; i < items.Count; i++)
        {
            ShopCardView shopCard = null;

            switch (items[i].TypeItem)
            {
                case ItemInfo.Type.Character:
                    shopCard = _scrollViewCharacter.CreateItems(items[i], shopCardView);
                    _shopCardViews.Add(shopCard);
                    break;
                case ItemInfo.Type.Top:
                    shopCard = _scrollViewTop.CreateItems(items[i], shopCardView);
                    _shopCardViews.Add(shopCard);
                    break;
                case ItemInfo.Type.Glasses:
                    shopCard = _scrollViewGlasses.CreateItems(items[i], shopCardView);
                    _shopCardViews.Add(shopCard);
                    break;
                case ItemInfo.Type.Hat:
                    shopCard = _scrollViewCap.CreateItems(items[i], shopCardView);
                    _shopCardViews.Add(shopCard);
                    break;
                case ItemInfo.Type.Ramka:
                    shopCard = _scrollViewRamka.CreateItems(items[i], shopCardView);
                    _shopCardViews.Add(shopCard);
                    break;
                case ItemInfo.Type.Ground:
                    shopCard = _scrollViewGround.CreateItems(items[i], shopCardView);
                    _shopCardViews.Add(shopCard);
                    break;
            }

            if (items[i].IsSelected)
                AddSelectedShopCard(shopCard);

            shopCard.ClickedButtonCard += OnClickedButtonCard;
        }
    }

    private void ChangeCurrentShopCard(ShopCardView currentCardView)
    {
        for (int i = 0; i < _currentsCardView.Count; i++)
        {
            if (currentCardView.item.TypeItem == _currentsCardView[i].item.TypeItem)
            {
                ChangedSelectedItem?.Invoke(_currentsCardView[i].item, currentCardView.item);
                AddSelectedShopCard(currentCardView);

                _currentsCardView[i].ChangeSelect(false);
                _currentsCardView.Remove(_currentsCardView[i]);
                break;
            }
        }
    }

    private void AddSelectedShopCard(ShopCardView shopCard)
    {
        _currentsCardView.Add(shopCard);
        ChangeSpriteItem(shopCard.item);
    }

    private ShopCardView GetCardViewByItem(Item item)
    {
        for (int i = 0; i < _shopCardViews.Count; i++)
        {
            if (_shopCardViews[i].item == item)
                return _shopCardViews[i];
        }
        return null;
    }

    #endregion


    private void ShowScrollViews(bool isShowCharacter, bool isShowTop, bool isShowGlasses, bool isShowCap,
                                 bool isShowRamka, bool isShowGround)
    {
        _scrollViewCharacter.gameObject.SetActive(isShowCharacter);
        _buttonCharacter.GetComponent<Image>().sprite = isShowCharacter ? _choosedButtonSprite : _standartButtonSprite;
        _scrollViewTop.gameObject.SetActive(isShowTop);
        _buttonTop.GetComponent<Image>().sprite = isShowTop ? _choosedButtonSprite : _standartButtonSprite;
        _scrollViewGlasses.gameObject.SetActive(isShowGlasses);
        _buttonGlasses.GetComponent<Image>().sprite = isShowGlasses ? _choosedButtonSprite : _standartButtonSprite;
        _scrollViewCap.gameObject.SetActive(isShowCap);
        _buttonCap.GetComponent<Image>().sprite = isShowCap ? _choosedButtonSprite : _standartButtonSprite;
        _scrollViewRamka.gameObject.SetActive(isShowRamka);
        _buttonRamka.GetComponent<Image>().sprite = isShowRamka ? _choosedButtonSprite : _standartButtonSprite;
        _scrollViewGround.gameObject.SetActive(isShowGround);
        _buttonGround.GetComponent<Image>().sprite = isShowGround ? _choosedButtonSprite : _standartButtonSprite;

        ClickedButtonShowScrollView?.Invoke();
    }

    private void OnClickedButtonCard(ShopCardView shopCardView) => ChangeCurrentShopCard(shopCardView);
}