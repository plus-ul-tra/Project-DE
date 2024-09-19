using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{   // InventorySO에 드랍된 아이템을 넘기고, 씬에 있는 아이템은 없애는 스크립트 

    [field: SerializeField]
    private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if(item != null)
        {   
            // reminder's role? 남은 수량 확인
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if(reminder == 0 )
            {
                item.DestoryItem();
            } 
            else
            {
                item.Quantity = reminder;   
            }
        }
    }
}
