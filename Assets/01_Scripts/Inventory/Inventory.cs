using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventary : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public List<Item> ItemsList;
    public GameObject inventoryItemPreFab;

    public GameObject inv;
    public bool isActive;

    int selectedSlot = -1;
    private void Start()
    {
        changeSelectedSlot(1);
    }
    void Update()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 8)
            {
                changeSelectedSlot(number - 1);
            }
        }
        
        if (scrollWheelInput > 0f)
        {
            changeSelectedSlot(selectedSlot - 1);
        }
        else if (scrollWheelInput < 0f)
        {
            changeSelectedSlot(selectedSlot + 1);
        }

        if (isActive)
        {
            inv.SetActive(true);

        }
        else
        {
            inv.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isActive = !isActive;
        }
    }

    void changeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].DeSelected();
        }
        if(newValue > 6 )
        {
            newValue = 0;
        }
        else if(newValue < 0 )
        {
            newValue = 6;
        }

        inventorySlots[newValue].Selected();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot != null && itemSlot.count < 64 && itemSlot.item == item && itemSlot.item.stackable)
            {
                itemSlot.count++;
                itemSlot.RefreshCount();
                return true;
            }
        }

        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }
    void SpawnNewItem(Item item , InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPreFab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Item"))
        {
            Item it = collision.collider.GetComponent<PreFabItem>().item;
            AddItem(it);
            collision.collider.GetComponent<PreFabItem>().Added();
            //Destroy(collision.collider.gameObject);
        }
    }
}
