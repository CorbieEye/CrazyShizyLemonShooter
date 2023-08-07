using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlacebleItem : MonoBehaviour
{
    [SerializeField] ItemSO _itemSO;
    [SerializeField] int _count = 1;

    private float _distanceToPick = 3f;

    // Set from here
    private Item _item;
    private PlayerCharacterController _playerCharacterController;
    private Inventory _inventory;
    private HUD _hud;
    private Character _character;

    public void Start()
    {
        Debug.Log("PlacebleItem started!!!");
        _playerCharacterController = (UnityEngine.Object.FindObjectsOfType(typeof(PlayerCharacterController))[0]) as PlayerCharacterController;
        _inventory = _playerCharacterController.gameObject.GetComponent<Inventory>();
        _hud = _playerCharacterController.gameObject.GetComponent<HUD>();
        _character = _playerCharacterController.gameObject.GetComponent<Character>();

        if (_item == null && _itemSO != null) {
            _item = _itemSO.ToItem();
        }
    }

    public Item Item { 
        get { return _item; }
        set { if (_item == null) _item = value; }
    }

    private bool PlayerCanPick() {
        
        bool playerCharacterIsAlive = !_character.IsDead;
        Debug.Log("playerCharacterIsAlive: " + playerCharacterIsAlive); 
        Debug.Log("Vector3.Distance(gameObject.transform.position, _character.transform.position): " + Vector3.Distance(gameObject.transform.position, _character.transform.position));
        bool enoughDistance = Vector3.Distance(gameObject.transform.position, _character.transform.position) <= _distanceToPick;
        return playerCharacterIsAlive && enoughDistance;
    }

    public void TryPick()
    {
        if (PlayerCanPick()) {
            _inventory.Additem(_item);
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        _playerCharacterController.PlacebleItem = this;
    }

    private void OnMouseExit()
    {
        _playerCharacterController.PlacebleItem = null;
    }
}
