using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class InventoryMenu : MonoBehaviour
{
    // Set from inspector
    [SerializeField] private Transform _buttonContent;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Text _itemDescriptionText;

    // SetFrom here
    private PlayerInput _playerInput;
    private GameManager _gameManager;

    // Set from public methods
    private Inventory _inventory;


    [Inject]
    public void Construct(PlayerInput playerInput, GameManager gameManager) {
        _playerInput = playerInput;
        _gameManager = gameManager;
    }

    public void SetInvenory(Inventory inventory) {
        _inventory = inventory;
    }

    void Start()
    {
        _itemDescriptionText.text = "";
        _playerInput.currentActionMap.actions.Where(x => x.name == "OpenCloseInventory").FirstOrDefault().performed += _gameManager.InventoryButtonPressed;
        gameObject.SetActive(false);
    }

    public void UpdateInventoryMenu() {
        ClearExistingButtons();
        AddNewButtons();
    }

    #region ClearExistingButtons
    private void ClearExistingButtons() {
        Button[] buttons = _buttonContent.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
    }
    #endregion

    #region AddNewButtons
    private void AddNewButtons() {
        foreach (Item item in _inventory) {
            var button = Instantiate(_buttonPrefab).GetComponent<Button>();
            button.gameObject.transform.SetParent(_buttonContent);
            Text label = button.GetComponentInChildren<Text>();
            label.gameObject.transform.SetParent(button.gameObject.transform);
            label.text = item.ItemSO.Name;
            if (item.ItemSO.IsStackable) {
                label.text += " (" + item.Count + ")";
            }

            var itemButton = button.gameObject.AddComponent<ItemButton>();
            itemButton.SetInventory(_inventory);
            itemButton.SetItem(item);
            itemButton.SetDescription(_itemDescriptionText);

            var eventTrigger = button.gameObject.AddComponent<EventTrigger>();
        }
    }
    #endregion

    private void OnDestroy()
    {
        _playerInput.currentActionMap.actions.Where(x => x.name == "OpenCloseInventory").FirstOrDefault().performed -= _gameManager.InventoryButtonPressed;
    }
}
