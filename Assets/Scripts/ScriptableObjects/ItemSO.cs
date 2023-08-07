using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private GameObject _model;

    public string Name { get { return _name; } }
    public Sprite Icon { get { return _icon; } }
    public GameObject Model { get { return _model; } }
    public abstract bool IsStackable { get; }

    public abstract Item ToItem();
}
