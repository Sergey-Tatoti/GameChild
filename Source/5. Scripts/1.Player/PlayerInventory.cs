using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private string _name;
    private int _multiplyOrderInLayer;
    private int _currentOrderInLayer = 1;

    private SpriteRenderer _spriteCharacterHead;
    private SpriteRenderer _spriteCharacterBody;
    private SpriteRenderer _spriteCharacterEyes;
    private SpriteRenderer _spriteCharacterArmLeft;
    private SpriteRenderer _spriteCharacterArmRight;
    private SpriteRenderer _spriteCharacterLegLeft;
    private SpriteRenderer _spriteCharacterLegRight;
    private SpriteRenderer _spriteShadow;
    private SpriteRenderer _spriteTop;
    private SpriteRenderer _spriteTopArmLeft;
    private SpriteRenderer _spriteTopArmRight;
    private SpriteRenderer _spriteGlasses;
    private SpriteRenderer _spriteHat;
    private SpriteRenderer _spriteKey;

    private GameKey _gameKey;
    private List<SpriteRenderer> _allSpriteRenderers;

    public GameKey GameKey => _gameKey;
    public string Name => _name;

    public void SetValue(string name, int multiplyOrderInLayer, List<Item> items, SpriteRenderer spriteCharacterBody, SpriteRenderer spriteCharacterHead,
                         SpriteRenderer spriteCharacterEyes, SpriteRenderer spriteCharacterArmLeft,
                         SpriteRenderer spriteCharacterArmRight, SpriteRenderer spriteCharacterLegLeft, SpriteRenderer spriteCharacterLegRight,
                         SpriteRenderer spriteShadow, SpriteRenderer spriteTop, SpriteRenderer spriteTopArmLeft,
                         SpriteRenderer spriteTopArmRight, SpriteRenderer spriteGlasses, SpriteRenderer spriteHat, SpriteRenderer spriteKey)
    {
        _name = name;
        _multiplyOrderInLayer = multiplyOrderInLayer;
        _spriteCharacterHead = spriteCharacterHead;
        _spriteCharacterBody = spriteCharacterBody;
        _spriteCharacterEyes = spriteCharacterEyes;
        _spriteCharacterArmLeft = spriteCharacterArmLeft;
        _spriteCharacterArmRight = spriteCharacterArmRight;
        _spriteCharacterLegLeft = spriteCharacterLegLeft;
        _spriteCharacterLegRight = spriteCharacterLegRight;
        _spriteShadow = spriteShadow;
        _spriteTop = spriteTop;
        _spriteTopArmLeft = spriteTopArmLeft;
        _spriteTopArmRight = spriteTopArmRight;
        _spriteGlasses = spriteGlasses;
        _spriteHat = spriteHat;
        _spriteKey = spriteKey;

        _allSpriteRenderers = new List<SpriteRenderer>()
        {
           _spriteCharacterHead, _spriteCharacterBody, _spriteCharacterEyes, _spriteCharacterArmLeft,
           _spriteCharacterArmRight, _spriteCharacterLegLeft, _spriteCharacterLegRight, _spriteShadow,
           _spriteTop, _spriteTopArmLeft, _spriteTopArmRight, _spriteGlasses, _spriteHat, _spriteKey
        };

        for (int i = 0; i < items.Count; i++) { ChangeCharacterItem(items[i]); }
    }

    #region ----- Action Sprites -----

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

    public void ResetOrderInLayer()
    {
        for (int i = 0; i < _allSpriteRenderers.Count; i++)
        {
            _allSpriteRenderers[i].sortingOrder -= _currentOrderInLayer * _multiplyOrderInLayer;
        }
    }

    public void OnChangedLine(int orderInLayer)
    {
        if (_allSpriteRenderers[0].sortingOrder >= _multiplyOrderInLayer)
            ResetOrderInLayer();

        _currentOrderInLayer = orderInLayer;

        for (int i = 0; i < _allSpriteRenderers.Count; i++)
        {
            _allSpriteRenderers[i].sortingOrder += _currentOrderInLayer * _multiplyOrderInLayer;
        }
    }

    #endregion

    #region ----- Keys Action -----

    public void SetKey(GameKey gameKey)
    {
        _gameKey = gameKey;
        _spriteKey.sprite = gameKey.Key.SpriteItem;
        _spriteKey.gameObject.SetActive(true);
    }

    public bool CheckLock(Lock locked)
    {
        if (_gameKey != null && locked.Key == _gameKey.Key)
            return true;
        else
            return false;
    }

    public void ResetKey()
    {
        _gameKey = null;
        _spriteKey.gameObject.SetActive(false);
    }

    #endregion

    public void ChangeName(string name) => _name = name;
}