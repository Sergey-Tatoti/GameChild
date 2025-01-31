using UnityEngine;

public class CharacterItem : Item
{
    private Item _capItem;

    public Item CapItem => _capItem;

    public void ChangeCap(Item item) => _capItem = item;
}