using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopManagerUI _shopManagerUI;
    [SerializeField] private ShopCardView _shopCardViewPrefab;

    private List<int> _idSelectedItems = new List<int>();
    private List<Item> _items;

    public event UnityAction<Item> ChangedCharacter;
    public event UnityAction<List<int>, Item> ChangedIdSelectedItems;
    public event UnityAction ClickedButton;
    public event UnityAction<int> ChangedGroundAvatar;

    private void OnEnable()
    {
        _shopManagerUI.ChangedSelectedItem += OnChangedSelectedItem;
        _shopManagerUI.ClickedButtonShowScrollView += OnClickedButtonShowScrollView;
        _shopManagerUI.ChangedGroundAvatar += OnChangedGroundAvatar;
    }

    private void OnDisable()
    {
        _shopManagerUI.ChangedSelectedItem -= OnChangedSelectedItem;
        _shopManagerUI.ClickedButtonShowScrollView -= OnClickedButtonShowScrollView;
        _shopManagerUI.ChangedGroundAvatar -= OnChangedGroundAvatar;
    }

    public void SetValue(List<Item> items, List<Tutorial> tutorials, int indexGround)
    {
        _items = items;

        _shopManagerUI.SetValue(items, _shopCardViewPrefab, tutorials, indexGround);

        SetSelectedItems(items);
        _shopManagerUI.UpdateShopCards(_items);
    }

    public void TakedItem(Item lastItem, Item currentItem)
    {
        currentItem.OpenItem(true);

        _shopManagerUI.UpdateShopCards(_items);
    }

    private void OnClickedButtonShowScrollView() => ClickedButton?.Invoke();

    private void OnChangedGroundAvatar(int indexGround) => ChangedGroundAvatar?.Invoke(indexGround);

    #region ----- SelectedItems -----

    private void OnChangedSelectedItem(Item lastItem, Item currentItem)
    {
        if (lastItem != null)
            ChangeCurrentItem(lastItem, currentItem);
        else
            _idSelectedItems.Add(currentItem.Id);

        currentItem.SelectItem(true);

        TryChangeCharacter(currentItem);

        ChangedIdSelectedItems?.Invoke(_idSelectedItems, currentItem);

        _shopManagerUI.UpdateShopCards(_items);
    }

    private void ChangeCurrentItem(Item lastItem, Item currentItem)
    {
        for (int i = 0; i < _idSelectedItems.Count; i++)
        {
            if (_idSelectedItems[i] == lastItem.Id)
            {
                _idSelectedItems[i] = currentItem.Id;

                lastItem.SelectItem(false);
                break;
            }
        }
    }

    private void TryChangeCharacter(Item currentItem)
    {
        switch (currentItem.TypeItem)
        {
            case ItemInfo.Type.Character:
            case ItemInfo.Type.Hat:
            case ItemInfo.Type.Glasses:
            case ItemInfo.Type.Top:
                ChangedCharacter?.Invoke(currentItem);
                break;
        }
    }

    private void SetSelectedItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].IsSelected)
                _idSelectedItems.Add(items[i].Id);
        }
    }

    #endregion
}