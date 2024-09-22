using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> itemCurrentState;



    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        EquipItem(weaponItemSO, itemState);

    }

    // ������ ���� �� ������ �÷��ִ� �޼���
    public void EquipItem(EquippableItemSO equippableItem, List<ItemParameter> itemParameters)
    {
        PlayerInfo playerInfo = GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            foreach (var parameter in itemParameters)
            {
                if (parameter.parameterType == ParameterType.Speed) // ���������� ��
                {
                    playerInfo.stat.Speed += parameter.value; // ���ǵ� �߰�
                }
            }


        }
    }

    // ������ ���� ���� �� ������ ������� ������ �޼��� (���� ����)
    public void UnequipItem(EquippableItemSO equippableItem, List<ItemParameterSO> itemParameters)
    {
        
    }


}
