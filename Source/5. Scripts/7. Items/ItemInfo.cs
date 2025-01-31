using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Create/new Item", order = 51)]

public class ItemInfo : ScriptableObject
{
    public enum Type { Character, Hat, Glasses, Top, Ramka, Ground }

    [SerializeField] private int _id;
    [SerializeField] private Type _type;
    [Header("Если предмет:")]
    [SerializeField] private Sprite _spriteItem;
    [Header("Если одежда:")]
    [SerializeField] private Sprite _spriteTop;
    [SerializeField] private Sprite _spriteTopArm;
    [Header("Если персонаж:")]
    [SerializeField] private Sprite _spriteHead;
    [SerializeField] private Sprite _spriteBody;
    [SerializeField] private Sprite _spriteEyes;
    [SerializeField] private Sprite _spriteArm;
    [SerializeField] private Sprite _spriteLeg;

    public int Id => _id;
    public Sprite SpriteItem => _spriteItem;
    public Sprite SpriteHead => _spriteHead;
    public Sprite SpriteBody => _spriteBody;
    public Sprite SpriteEyes => _spriteEyes;
    public Sprite SpriteArm => _spriteArm;
    public Sprite SpriteLeg => _spriteLeg;
    public Sprite SpriteTop => _spriteTop;
    public Sprite SpriteTopArm => _spriteTopArm;
    public Type TypeItem => _type;

}