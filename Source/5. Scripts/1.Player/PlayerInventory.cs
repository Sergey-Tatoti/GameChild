using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private string _name;
    private SpriteRenderer _spriteCharacterHead;
    private SpriteRenderer _spriteCharacterBody;
    private SpriteRenderer _spriteCharacterEyes;
    private SpriteRenderer _spriteCharacterArmLeft;
    private SpriteRenderer _spriteCharacterArmRight;
    private SpriteRenderer _spriteCharacterLegLeft;
    private SpriteRenderer _spriteCharacterLegRight;
    private SpriteRenderer _spriteTop;
    private SpriteRenderer _spriteTopArmLeft;
    private SpriteRenderer _spriteTopArmRight;
    private SpriteRenderer _spriteGlasses;
    private SpriteRenderer _spriteHat;
    private SpriteRenderer _spriteKey;
    private GameKey _gameKey;

    public GameKey GameKey => _gameKey;
    public string Name => _name;

    public void SetValue(string name, List<Item> items, SpriteRenderer spriteCharacterBody, SpriteRenderer spriteCharacterHead,
                         SpriteRenderer spriteCharacterEyes, SpriteRenderer spriteCharacterArmLeft,
                         SpriteRenderer spriteCharacterArmRight, SpriteRenderer spriteCharacterLegLeft,
                         SpriteRenderer spriteCharacterLegRight, SpriteRenderer spriteTop, SpriteRenderer spriteTopArmLeft,
                         SpriteRenderer spriteTopArmRight, SpriteRenderer spriteGlasses, SpriteRenderer spriteHat, SpriteRenderer spriteKey)
    {
        _name = name;
        _spriteCharacterHead = spriteCharacterHead;
        _spriteCharacterBody = spriteCharacterBody;
        _spriteCharacterEyes = spriteCharacterEyes;
        _spriteCharacterArmLeft = spriteCharacterArmLeft;
        _spriteCharacterArmRight = spriteCharacterArmRight;
        _spriteCharacterLegLeft = spriteCharacterLegLeft;
        _spriteCharacterLegRight = spriteCharacterLegRight;
        _spriteTop = spriteTop;
        _spriteTopArmLeft = spriteTopArmLeft;
        _spriteTopArmRight = spriteTopArmRight;
        _spriteGlasses = spriteGlasses;
        _spriteHat = spriteHat;
        _spriteKey = spriteKey;

        for (int i = 0; i < items.Count; i++) { ChangeCharacterItem(items[i]); }
    }

    public void ChangeCharacterItem(Item item)
    {
        switch (item.TypeItem)
        {
            case ItemInfo.Type.Character:
                _spriteCharacterHead.sprite = item.SpriteHead;
                _spriteCharacterBody.sprite = item.SpriteBody;
                _spriteCharacterEyes.sprite = item.SpriteEyes;
                _spriteCharacterArmLeft.sprite = item.SpriteArm;
                _spriteCharacterArmRight.sprite = item.SpriteArm;
                _spriteCharacterLegLeft.sprite = item.SpriteLeg;
                _spriteCharacterLegRight.sprite = item.SpriteLeg;
                break;
            case ItemInfo.Type.Top:
                if (item.SpriteItem != null)
                {
                    _spriteTop.sprite = item.SpriteTop;
                    _spriteTopArmLeft.sprite = item.SpriteTopArm;
                    _spriteTopArmRight.sprite = item.SpriteTopArm;
                }

                _spriteTop.gameObject.SetActive(item.SpriteItem != null);
                _spriteTopArmLeft.gameObject.SetActive(item.SpriteItem != null);
                _spriteTopArmRight.gameObject.SetActive(item.SpriteItem != null);
                break;
            case ItemInfo.Type.Glasses:
                if (item.SpriteItem != null)
                    _spriteGlasses.sprite = item.SpriteItem;

                _spriteGlasses.gameObject.SetActive(item.SpriteItem != null);
                break;
            case ItemInfo.Type.Hat:
                if (item.SpriteItem != null)
                    _spriteHat.sprite = item.SpriteItem;

                _spriteHat.gameObject.SetActive(item.SpriteItem != null);
                break;
        }
    }

    public void SetKey(GameKey gameKey)
    {
        _gameKey = gameKey;
        _spriteKey.sprite = gameKey.Key.SpriteItem;
        _spriteKey.gameObject.SetActive(true);
    }

    public bool CheckLock(Lock locked)
    {
        Debug.Log(locked.Key + " " + _gameKey.Key);
        if (locked.Key == _gameKey.Key)
        {
            
            ResetKey();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetKey()
    {
        _gameKey = null;
        _spriteKey.gameObject.SetActive(false);

    }

    public void ChangeName(string name) => _name = name;
}