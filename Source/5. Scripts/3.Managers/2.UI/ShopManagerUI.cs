using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] private List<Avatar> _avatars;
    [SerializeField] private List<GroundAvatar> _groundsAvatar;
    [SerializeField] private GameObject _markButtonShop;
    [Space]
    [SerializeField] private ScrollViewShop _scrollViewCharacter;
    [SerializeField] private ScrollViewShop _scrollViewTop;
    [SerializeField] private ScrollViewShop _scrollViewGlasses;
    [SerializeField] private ScrollViewShop _scrollViewCap;

    private List<ScrollViewShop> _scrollViewShops;
    private List<ShopCardView> _shopCardViews = new List<ShopCardView>();
    private List<ShopCardView> _currentsCardView = new List<ShopCardView>();
    private List<Tutorial> _tutorials;

    public event UnityAction<Item, Item> ChangedSelectedItem;
    public event UnityAction ClickedButtonShowScrollView;
    public event UnityAction ClickedButtonHideScrollView;
    public event UnityAction<int> ChangedGroundAvatar;

    #region ----- Initialize ------

    private void OnEnable()
    {
        for (int i = 0; i < _groundsAvatar.Count; i++)
        {
            _groundsAvatar[i].ClickedButtonGround += OnClickedButtonGround;
        }

        if (_scrollViewShops != null)
        {
            for (int i = 0; i < _scrollViewShops.Count; i++)
            {
                _scrollViewShops[i].ClickedButtonShop += OnClickedButtonShop;
            }
        }

        if(_shopCardViews != null)
        {
            for (int i = 0; i < _shopCardViews.Count; i++)
            {
                _shopCardViews[i].ClickedButtonCard += OnClickedButtonCard;
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _groundsAvatar.Count; i++)
        {
            _groundsAvatar[i].ClickedButtonGround -= OnClickedButtonGround;
        }

        if (_scrollViewShops != null)
        {
            for (int i = 0; i < _scrollViewShops.Count; i++)
            {
                _scrollViewShops[i].ClickedButtonShop -= OnClickedButtonShop;
            }
        }

        if (_shopCardViews != null)
        {
            for (int i = 0; i < _shopCardViews.Count; i++)
            {
                _shopCardViews[i].ClickedButtonCard -= OnClickedButtonCard;
            }
        }
    }

    public void SetValue(List<Item> items, ShopCardView shopCardView, List<Tutorial> tutorials, int indexGroundAvatar)
    {
        _tutorials = tutorials;
        _scrollViewCharacter.ShowPanel(true);

        FillListShopCards(items, shopCardView);
        FillTutorials();

        _scrollViewShops = new List<ScrollViewShop>() { _scrollViewCap, _scrollViewCharacter, 
                                                        _scrollViewGlasses,  _scrollViewTop };

        for (int i = 0; i < _scrollViewShops.Count; i++)
        {
            _scrollViewShops[i].ClickedButtonShop += OnClickedButtonShop;
        }

        OnClickedButtonGround(_groundsAvatar[indexGroundAvatar].TypeGround);
    }

    private void FillTutorials()
    {
        for (int i = 0; i < _tutorials.Count; i++)
        {
            _tutorials[i].SetButtonsShop(_scrollViewCharacter.ButtonShop, _scrollViewGlasses.ButtonShop,
                                         _scrollViewTop.ButtonShop, _scrollViewCap.ButtonShop, _shopCardViews);
        }
    }

    #endregion

    #region ----- Avatars -----

    public void ChangeSpriteItem(Item item)
    {
        for (int i = 0; i < _avatars.Count; i++) { _avatars[i].ChangeSpriteItem(item); }
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

        _markButtonShop.SetActive(isShowMark);
    }

    #endregion

    #region ----- ActionsShopCards -----


    public void UpdateShopCards(List<Item> items)
    {
        UpdatesShopCardViews();
        UpdateAvatars(items);

        _scrollViewCharacter.UpdateViewShop(items);
        _scrollViewTop.UpdateViewShop(items);
        _scrollViewCap.UpdateViewShop(items);
        _scrollViewGlasses.UpdateViewShop(items);
    }

    private void UpdatesShopCardViews()
    {
        for (int i = 0; i < _shopCardViews.Count; i++)
        {
            _shopCardViews[i].UpdateInfo();
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

    #endregion

    #region ----- Clicked Buttons -----

    private void OnClickedButtonShop(ScrollViewShop scrollViewShop)
    {
        for (int i = 0; i < _scrollViewShops.Count; i++)
        {
            if (_scrollViewShops[i] == scrollViewShop)
                _scrollViewShops[i].ShowPanel(true);
            else
                _scrollViewShops[i].ShowPanel(false);
        }

        ClickedButtonShowScrollView?.Invoke();
    }

    private void OnClickedButtonGround(GroundAvatar.TypeAvatarGround typeGround)
    {
        for (int i = 0; i < _groundsAvatar.Count; i++)
        {
            _groundsAvatar[i].ShowGround(false);

            if (_groundsAvatar[i].TypeGround == typeGround)
            {
                for (int j = 0; j < _avatars.Count; j++)
                {
                    _avatars[j].SetGround(_groundsAvatar[i].SpriteGround, _groundsAvatar[i].SpriteRamka);
                }

                _groundsAvatar[i].ShowGround(true);
                ChangedGroundAvatar?.Invoke(i);
            }
        }
    }

    private void OnClickedButtonCard(ShopCardView shopCardView) => ChangeCurrentShopCard(shopCardView);

    #endregion
}