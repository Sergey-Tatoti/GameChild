using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemInfo _itemInfo;

    private bool _isOpened;
    private bool _isSelected;
    private bool _isShower;

    public int Id => _itemInfo.Id;
    public Sprite SpriteItem => _itemInfo.SpriteItem;
    public Sprite SpriteHead => _itemInfo.SpriteHead;
    public Sprite SpriteBody => _itemInfo.SpriteBody;
    public Sprite SpriteEyes => _itemInfo.SpriteEyes;
    public Sprite SpriteArm => _itemInfo.SpriteArm;
    public Sprite SpriteLeg => _itemInfo.SpriteLeg;
    public Sprite SpriteTop => _itemInfo.SpriteTop;
    public Sprite SpriteTopArm => _itemInfo.SpriteTopArm;

    public ItemInfo.Type TypeItem => _itemInfo.TypeItem;
    public bool IsOpened => _isOpened;
    public bool IsSelected => _isSelected;
    public bool IsShower => _isShower;

    public void OpenItem(bool isOpened) => _isOpened = isOpened;

    public void SelectItem(bool isSelected) => _isSelected = isSelected;

    public void SetShowerItem() => _isShower = true;
}