using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialShop : Tutorial
{
    private Button _buttonShop;
    private Button _buttonPanelShop;
    private Button _buttonItem;

    public override void ActivateTutroialReward(Item item)
    {
        base.ActivateTutroialReward(item);

        Debug.Log("WOOOORK");

        _buttonAnimationShop.SetShining(true);
        _buttonShop = _buttonAnimationShop.GetComponent<Button>();
        _buttonShop.interactable = true;
        _buttonPanelShop = GetButtonPanelShop(item);
        _buttonPanelShop.interactable = true;
        _buttonItem = GetButtonItem(item);
        _buttonItem.interactable = true;

        _buttonShop.onClick.AddListener(ShowButtonPanel);
        _buttonPanelShop.onClick.AddListener(ShowButtonItem);
        _buttonItem.onClick.AddListener(DeactivateTutroialReward);
    }

    public override void DeactivateTutroialReward()
    {
        base.DeactivateTutroialReward();

        _buttonItem.GetComponent<ButtonAnimation>().SetShining(false);
    }

    private void ShowButtonPanel()
    {
        _buttonAnimationShop.SetShining(false);
        _buttonPanelShop.GetComponent<ButtonAnimation>().SetShining(true);
    }

    private void ShowButtonItem()
    {
        _buttonPanelShop.GetComponent<ButtonAnimation>().SetShining(false);
        _buttonItem.GetComponent<ButtonAnimation>().SetShining(true);
    }

    private Button GetButtonPanelShop(Item item)
    {
        Button button = null;

        switch (item.TypeItem)
        {
            case ItemInfo.Type.Character:
                button = _buttonAnimationCharacterShop.GetComponent<Button>();
                break;
            case ItemInfo.Type.Hat:
                button = _buttonAnimationCapShop.GetComponent<Button>();
                break;
            case ItemInfo.Type.Glasses:
                button = _buttonAnimationGlassesShop.GetComponent<Button>();
                break;
            case ItemInfo.Type.Top:
                button = _buttonAnimationTopShop.GetComponent<Button>();
                break;
            case ItemInfo.Type.Ramka:
                button = _buttonAnimationRamkaShop.GetComponent<Button>();
                break;
            case ItemInfo.Type.Ground:
                button = _buttonAnimationGroundShop.GetComponent<Button>();
                break;

        }
        return button;
    }

    private Button GetButtonItem(Item item)
    {
        for (int i = 0; i < _shopCardViews.Count; i++)
        {
            if (_shopCardViews[i].item == item)
            {
                return _shopCardViews[i].ButtonCard;
            }
        }
        return null;
    }
}
