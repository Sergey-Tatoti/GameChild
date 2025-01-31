using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopCardView : MonoBehaviour
{
    [SerializeField] private Image _imageBackCard;
    [SerializeField] private Image _imageItemCard;
    [SerializeField] private Image _imageOutLine;
    [SerializeField] private Image _imageMarkNewItem;
    [SerializeField] private Button _buttonCard;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _openSprite;

    private Item _item;
    private Sprite _closeSprite;
    private bool _isSelect;

    public Item item => _item;

    public event UnityAction<ShopCardView> ClickedButtonCard;

    private void OnEnable() => _buttonCard.onClick.AddListener(OnClickedButtonCard);

    private void OnDisable() => _buttonCard.onClick.RemoveListener(OnClickedButtonCard);

    public void SetValue(Item item, Sprite spriteCloseCard)
    {
        _item = item;
        _closeSprite = spriteCloseCard;
        _imageItemCard.sprite = _item.SpriteItem;

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        OpenCard(_item.IsOpened);
        ChangeSelect(_item.IsSelected);
        TrySetDefoultItem();
    }

    public void OpenCard(bool isOpen)
    {
        _imageMarkNewItem.gameObject.SetActive(!_item.IsShower && isOpen);
        _imageItemCard.gameObject.SetActive(isOpen);
        _imageBackCard.sprite = isOpen ? _openSprite : _closeSprite;
        _buttonCard.interactable = isOpen;
    }

    public void ChangeSelect(bool isSelect)
    {
        _isSelect = isSelect;
        _imageOutLine.gameObject.SetActive(isSelect);
    }

    private void TrySetDefoultItem()
    {
        if (_item.SpriteItem == null)
        {
            _imageItemCard.gameObject.SetActive(false);
            _imageBackCard.sprite = _defaultSprite;
        }
    }

    private void OnClickedButtonCard()
    {
        if (!_isSelect)
        {
            _imageMarkNewItem.gameObject.SetActive(false);

            ChangeSelect(!_isSelect);

            ClickedButtonCard?.Invoke(this);
        }
    }
}