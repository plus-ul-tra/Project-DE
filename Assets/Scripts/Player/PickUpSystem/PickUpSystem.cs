using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{   // InventorySO�� ����� �������� �ѱ��, ���� �ִ� �������� ���ִ� ��ũ��Ʈ 

    public float moveSpeed = 10f;
    public float magnetDistance = 15f;

    [field: SerializeField]
    private InventorySO inventoryData;

    //�⺻ ������ �Ⱦ�
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
    //�ڼ� ȿ��
    public void Magnet()
    {

    }
}
