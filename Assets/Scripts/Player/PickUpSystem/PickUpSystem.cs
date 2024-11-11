using Fusion;
using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{   // InventorySO�� ����� �������� �ѱ��, ���� �ִ� �������� ���ִ� ��ũ��Ʈ

    public float magnetRange = 3.0f;
    [field: SerializeField]
    private InventorySO inventoryData;

    //�⺻ ������ �Ⱦ�
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Item item = collision.GetComponent<Item>();
        if(item != null && collision.CompareTag("Item"))
        {   
           PickUpItem(item);
        }

    }

    public void PickUpItem(Item item)
    {
        // reminder's role? ���� ���� Ȯ��
        int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity); //�κ��丮�� ���� ���
        if (reminder == 0)
        {
            item.DestoryItem();
        }
        else
        {
            item.Quantity = reminder;
        }
    }
   
}
