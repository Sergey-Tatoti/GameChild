using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardRewardView : MonoBehaviour
{
    [SerializeField] private Button _buttonCard;
    [SerializeField] private Image _cardCloseImage;
    [SerializeField] private Image _cardOpenImage;
    [SerializeField] private Image _cardRewardImage;

    private Item _item;

    public Item Item => _item;

    public event UnityAction<CardRewardView> ClickedButtonCard;

    private void OnEnable() => _buttonCard.onClick.AddListener(ShowCardOpen);

    private void OnDisable() => _buttonCard.onClick.AddListener(ShowCardOpen);

    public void SetValue(Item itme)
    {
        _item = itme;
        _cardRewardImage.sprite = _item.SpriteItem;
    }

    public void BlockButton(bool isBlock)
    {
        _buttonCard.interactable = !isBlock;
    }

    private void ShowCardOpen()
    {
        _cardCloseImage.gameObject.SetActive(false);
        _cardOpenImage.gameObject.SetActive(true);

        ClickedButtonCard?.Invoke(this);
    }
}