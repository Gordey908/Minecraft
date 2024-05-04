using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private string parentName;
    public ItemData itemData;

    private void Start()
    {
        parentName = transform.parent.name;
    }

    private void AddToListOnDrag(List<GameObject> slots, List<ItemData> items, Transform parent, ItemData itemData)
    {
        if (itemData == null) return;

        bool foundExistingItem = false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetComponent<Slot>().itemData.id == itemData.id)
            {
                items[i].count += itemData.count;
                slots[i].transform.Find("ItemCountText").GetComponent<Text>().text = items[i].count.ToString();
                Destroy(gameObject);
                foundExistingItem = true;
                break;
            }
        }

        if (!foundExistingItem)
        {
            slots.Add(gameObject);
            items.Add(itemData);
            transform.SetParent(parent);
            parentName = transform.parent.name;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.instance.GetDescriptionPanel().SetActive(false);
        transform.SetParent(InventoryManager.instance.GetTempParentForSlots());
        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var pController = PlayerController.instance;
        var inventoryManager = InventoryManager.instance;
        float slotInventoryContentDistance = Vector3.Distance(transform.position, inventoryManager.GetInventoryContent().transform.position);
        float slotChestContentDistance = Vector3.Distance(transform.position, inventoryManager.GetChestContent().transform.position);

        if (slotInventoryContentDistance < slotChestContentDistance)
        {
            if (parentName == "InventoryContent")
            {
                transform.SetParent(inventoryManager.GetInventoryContent().transform);
            }
            else
            {
                inventoryManager.currentChestSlots.Remove(gameObject);
                pController.currentChestItems.Remove(itemData);
                AddToListOnDrag(inventoryManager.inventorySlots, pController.inventoryItems, inventoryManager.GetInventoryContent().transform, itemData);
            }
        }
        else
        {
            if (parentName == "ChestContent")
            {
                transform.SetParent(inventoryManager.GetChestContent().transform);
            }
            else
            {
                inventoryManager.inventorySlots.Remove(gameObject);
                pController.inventoryItems.Remove(itemData);
                AddToListOnDrag(inventoryManager.currentChestSlots, pController.inventoryItems, inventoryManager.GetChestContent().transform, itemData);
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        var iManager = InventoryManager.instance;
        iManager.GetDescriptionPanel().SetActive(true);
        if(itemData != null)
        {
            iManager.text = itemData.description;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.instance.GetDescriptionPanel().SetActive(false);
    }
}
