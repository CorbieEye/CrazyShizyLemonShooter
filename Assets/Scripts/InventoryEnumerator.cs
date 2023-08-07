using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEnumerator : IEnumerator
{
    private Item[] _items;
    private int _position = -1;

    public InventoryEnumerator(Item[] items) {
        _items = items;
    }

    public object Current {
        get
        {
            if (_position == -1 || _position >= _items.Length)
                throw new ArgumentException();
            return _items[_position];
        }
    }

    public bool MoveNext()
    {
        if (_position < _items.Length - 1)
        {
            _position++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        _position = -1;
    }
}
