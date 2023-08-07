using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour, IEnumerable
{
    private List<Item> _items = new List<Item>();
    [SerializeField]private Character _character;

    private InventoryMenu _inventoryMenu;

    [Inject]
    public void Construct(InventoryMenu inventoryMenu) {
        _character = GetComponent<Character>();
        _inventoryMenu = inventoryMenu;
        _inventoryMenu.SetInvenory(this);
    }

    public void Additem(Item item, int count = 1) {
        if (item.ItemSO.IsStackable)
        {
            var existingItem = _items.Where(x => x.ItemSO.Equals(item.ItemSO)).FirstOrDefault();
            if (existingItem != null)
            {
                existingItem.IncreaseCount(count);
            }
            else
            {
                item.IncreaseCount(count);
                _items.Add(item);
            }
        }
        else {
            for (int i = 0; i < count; i++) {
                _items.Add(item);
            }
        }
    }

    public void UseItem(Item item) {
        if (_items.Contains(item)) {
            item.Use(_character);
        }
    }

    public void RemoveItem(Item item) {
        if (_items.Contains(item))
        {
            if (item.ItemSO.IsStackable)
            {
                item.IncreaseCount(-1);
                if (item.Count <= 0)
                {
                    _items.Remove(item);
                }
            }
            else
            {
                if (item is Weapon) {
                    var weapon = (Weapon)item;
                    if (_character.CurrentWeapon == weapon) {
                        _character.EquipWeapon(weapon);
                    }
                }
                _items.Remove(item);
            }
        }
        _inventoryMenu.UpdateInventoryMenu();
    }

    public void DropItem(Item item) {
        InstantiatePlacebleItem(item);
        RemoveItem(item);
    }

    private void InstantiatePlacebleItem(Item item) {
        var pointToInstantiate = _character.transform.position + _character.transform.forward + Vector3.up;
        var go = Instantiate(item.ItemSO.Model, pointToInstantiate, Quaternion.identity, null);
        go.AddComponent<Rigidbody>();
        var placebleItem = go.AddComponent<PlacebleItem>();
        placebleItem.Item = item;
    }

    public IEnumerator GetEnumerator()
    {
        return new InventoryEnumerator(_items.ToArray());
    }
}
