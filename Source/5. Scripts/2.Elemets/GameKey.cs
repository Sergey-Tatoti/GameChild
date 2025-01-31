using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKey : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Key _key;

    public Key Key => _key;

    public void SetValue()
    {
        _spriteRenderer.sprite = _key.SpriteItem;
    }
}