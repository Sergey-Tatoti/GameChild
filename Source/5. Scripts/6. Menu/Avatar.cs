using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    [SerializeField] private TMP_Text _textName;
    [SerializeField] private Image _imageMarkNewItem;
    [Header("Ёлементы персонажа")]
    [SerializeField] private Image _imageCharacterHead;
    [SerializeField] private Image _imageCharacterBody;
    [SerializeField] private Image _imageCharacterLeftArm;
    [SerializeField] private Image _imageCharacterRightArm;
    [SerializeField] private Image _imageCharacterLeftLeg;
    [SerializeField] private Image _imageCharacterRightLeg;
    [Header("Ёлементы одежды")]
    [SerializeField] private Image _imageTop;
    [SerializeField] private Image _imageTopLeftArm;
    [SerializeField] private Image _imageTopRightArm;
    [SerializeField] private Image _imageGlasses;
    [SerializeField] private Image _imageHat;
    [Header("Ёлементы ‘она")]
    [SerializeField] private Image _imageRamka;
    [SerializeField] private Image _imageGround;

    public void ChangeSpriteItem(Item item)
    {
        switch (item.TypeItem)
        {
            case ItemInfo.Type.Character:
                _imageCharacterHead.sprite = item.SpriteHead;
                _imageCharacterBody.sprite = item.SpriteBody;
                _imageCharacterLeftArm.sprite = item.SpriteArm;
                _imageCharacterRightArm.sprite = item.SpriteArm;
                _imageCharacterLeftLeg.sprite = item.SpriteLeg;
                _imageCharacterRightLeg.sprite = item.SpriteLeg;
                break;
            case ItemInfo.Type.Top:
                if (item.SpriteTop != null)
                {
                    _imageTop.sprite = item.SpriteTop;
                    _imageTopLeftArm.sprite = item.SpriteTopArm;
                    _imageTopRightArm.sprite = item.SpriteTopArm;
                }

                _imageTop.gameObject.SetActive(item.SpriteTop != null);
                _imageTopLeftArm.gameObject.SetActive(item.SpriteTopArm != null);
                _imageTopRightArm.gameObject.SetActive(item.SpriteTopArm != null);
                break;
            case ItemInfo.Type.Glasses:
                if (item.SpriteItem != null)
                    _imageGlasses.sprite = item.SpriteItem;

                _imageGlasses.gameObject.SetActive(item.SpriteItem != null);
                break;
            case ItemInfo.Type.Hat:
                if (item.SpriteItem != null)
                    _imageHat.sprite = item.SpriteItem;

                _imageHat.gameObject.SetActive(item.SpriteItem != null);
                break;
            case ItemInfo.Type.Ramka:
                _imageRamka.sprite = item.SpriteItem;
                break;
            case ItemInfo.Type.Ground:
                _imageGround.sprite = item.SpriteItem;
                break;
        }
    }

    public void ChangeName(string name) => _textName.text = name;

    public void ShowMarkNewitem(bool isShow)
    {
        _imageMarkNewItem.gameObject.SetActive(isShow);
        _imageMarkNewItem.GetComponent<ButtonAnimation>().SetShining(isShow);
    }
}