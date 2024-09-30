using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEquipment : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> itemCurrentState; // �Ķ���� ��� ����
    
    public void SetEquipment(EquippableItemSO equipmentItemSO, List<ItemParameter> itemState)
    {
        if(weapon != null) //�ٸ��� �����Ǿ� �ִ� ���
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState); //�����Ǿ� �ִ°� �κ��丮��
            UnequipItem(equipmentItemSO, itemCurrentState);
        }
        this.weapon = equipmentItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        EquipItem(equipmentItemSO, itemState);

    } // weapon�̶�� ���� ���� �����Ǿ� �ִ� ������

    // ������ ���� �� ������ �÷��ִ� �޼���
    // ��������κ�/ ���⼭ ��������, playerInfo���� ��������
    public void EquipItem(EquippableItemSO equippableItem, List<ItemParameter> itemParameters)
    {
        PlayerInfo playerInfo = GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            playerInfo.ModifyStat(itemParameters, 1); // flag 1, -1
        }
        //���� list�� in
    }

    // ������ ���� ���� �� ������ ������� ������ �޼��� (���� ����)
    public void UnequipItem(EquippableItemSO equippableItem, List<ItemParameter> itemParameters) 
    {
        PlayerInfo playerInfo = GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            playerInfo.ModifyStat(itemParameters, -1); // flag -1 ���� ����
        }
    }
    //UI���� ���� �۾� �߰�

}
