using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    protected ItemSO _itemSO;
    protected int _count;

    public ItemSO ItemSO { get { return _itemSO; } }
    public int Count { get { return _count; } }

    public Item(ItemSO itemSO, int count) {
        _itemSO = itemSO;
    }
    public abstract void Use(Character user);
    public void IncreaseCount(int value) {
        _count += value;
    }
    public string Description { get { return _itemSO.Name + "\n" + _itemSO.ToString(); } }
}
