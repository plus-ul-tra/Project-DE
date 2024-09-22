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

    // 아이템 장착 시 스탯을 올려주는 메서드
    public void EquipItem(EquippableItemSO equippableItem, List<ItemParameter> itemParameters)
    {
        PlayerInfo playerInfo = GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            foreach (var parameter in itemParameters)
            {
                if (parameter.parameterType == ParameterType.Speed) // 열거형으로 비교
                {
                    playerInfo.stat.Speed += parameter.value; // 스피드 추가
                }
            }


        }
    }

    // 아이템 장착 해제 시 스탯을 원래대로 돌리는 메서드 (선택 사항)
    public void UnequipItem(EquippableItemSO equippableItem, List<ItemParameterSO> itemParameters)
    {
        
    }


}
