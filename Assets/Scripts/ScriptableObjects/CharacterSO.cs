using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Crazy shizy lemon shooter/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private string[] _characterTags;
    [SerializeField]
    private CharacterStatsSO _stats;
    [SerializeField]
    private Vector2 _inventorySize;

    public string Name { get { return _name; } }
    public Sprite Icon { get { return _icon; } }
    public GameObject Model { get { return _model; } }
    public string[] CharacterTags { get { return _characterTags; } }
    public CharacterStatsSO Stats { get { return _stats; } }
    public Vector2 InventorySize { get { return _inventorySize; } }
}
