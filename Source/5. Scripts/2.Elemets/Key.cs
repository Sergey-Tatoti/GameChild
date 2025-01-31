using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Create/new Key", order = 51)]

public class Key : ScriptableObject
{
    [SerializeField] private Sprite _spriteItem;

    public Sprite SpriteItem => _spriteItem;
}