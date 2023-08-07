using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //
    private Item _item;
    private Inventory _inventory;
    private Text _itemDescriptionText;

    public void SetItem(Item item) { _item = item; }
    public void SetInventory(Inventory inventory) { _inventory = inventory; }
    public void SetDescription(Text text) { _itemDescriptionText = text; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) {
            _inventory.UseItem(_item);
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            _inventory.DropItem(_item);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemDescriptionText.text = _item.Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemDescriptionText.text = "";
    }
}
