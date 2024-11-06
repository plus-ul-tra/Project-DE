using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{   // InventorySO에 드랍된 아이템을 넘기고, 씬에 있는 아이템은 없애는 스크립트 

    public float moveSpeed = 10f;
    public float magnetDistance = 15f;

    [field: SerializeField]
    private InventorySO inventoryData;

    //기본 아이템 픽업
    private void OnTriggerEnter2D(Collider2D collision) 
    {   
        Item item = collision.GetComponent<Item>();
        if(item != null)
        {   
           PickUpItem(item);
        }
    }

    private void PickUpItem(Item item)
    {
        // reminder's role? 남은 수량 확인
        int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity); //인벤토리에 들어가는 기능
        if (reminder == 0)
        {
            item.DestoryItem();
        }
        else
        {
            item.Quantity = reminder;
        }
    }
    //자석 효과
    public void Magnet()
    {

    }
}
