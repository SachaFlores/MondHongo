using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    
    public Image image;
    public Color selected, notSelected;
    private void Awake()
    {
        DeSelected();
    }
    public void Selected()
    {
        image.color = selected;
    }
    public void DeSelected()
    {
        image.color = notSelected;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }

    }
}
