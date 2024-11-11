using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentEquipment : MonoBehaviour
{
    //[SerializeField]
    //private EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    //[SerializeField]
    //private List<ItemParameter> itemCurrentState; // �Ķ���� ��� ����

    // ������ �����۵�
    private Dictionary<EquipmentType, EquippableItemSO> equippedItems = new Dictionary<EquipmentType, EquippableItemSO>();
    private Dictionary<EquipmentType, List<ItemParameter>> itemCurrentStates = new Dictionary<EquipmentType, List<ItemParameter>>();


    public void SetEquipment(EquippableItemSO equipmentItemSO, List<ItemParameter> itemState, EquipmentType itemType)
    {
        // ���� ������ �������� �ִ��� Ȯ���ϰ� ����
        if (equippedItems.TryGetValue(itemType, out EquippableItemSO currentEquipment))
        {
            // ���� ������ �������� �κ��丮�� ��ȯ
            inventoryData.AddItem(currentEquipment, 1, itemCurrentStates[itemType]);
            UnequipItem(currentEquipment, itemCurrentStates[itemType]); // ���� ����
        }
        

        // �� ��� ������ ����
        //Dictionary�� �߰�
        equippedItems[itemType] = equipmentItemSO; 
        itemCurrentStates[itemType] = new List<ItemParameter>(itemState); 
        EquipItem(equipmentItemSO, itemState); // ��� ����
    }


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
