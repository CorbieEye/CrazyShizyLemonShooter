using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConsumableItem : Item
{
    public ConsumableItem(ConsumableItemSO consumableItemSO, int count) : base(consumableItemSO, count) { }

    public override void Use(Character user)
    {
        var consumable = (ConsumableItemSO)_itemSO;
        foreach (var effect in consumable.Effects) {
            effect.Consume(user);
        }
        user.Inventory.RemoveItem(this);
    }
}
