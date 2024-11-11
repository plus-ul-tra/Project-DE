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
    //private List<ItemParameter> itemCurrentState; // 파라미터 장비 스텟

    // 장착된 아이템들
    private Dictionary<EquipmentType, EquippableItemSO> equippedItems = new Dictionary<EquipmentType, EquippableItemSO>();
    private Dictionary<EquipmentType, List<ItemParameter>> itemCurrentStates = new Dictionary<EquipmentType, List<ItemParameter>>();


    public void SetEquipment(EquippableItemSO equipmentItemSO, List<ItemParameter> itemState, EquipmentType itemType)
    {
        // 현재 장착된 아이템이 있는지 확인하고 해제
        if (equippedItems.TryGetValue(itemType, out EquippableItemSO currentEquipment))
        {
            // 현재 장착된 아이템을 인벤토리로 반환
            inventoryData.AddItem(currentEquipment, 1, itemCurrentStates[itemType]);
            UnequipItem(currentEquipment, itemCurrentStates[itemType]); // 장착 해제
        }
        

        // 새 장비 아이템 장착
        //Dictionary에 추가
        equippedItems[itemType] = equipmentItemSO; 
        itemCurrentStates[itemType] = new List<ItemParameter>(itemState); 
        EquipItem(equipmentItemSO, itemState); // 장비 장착
    }


    public void EquipItem(EquippableItemSO equippableItem, List<ItemParameter> itemParameters)
    {
        PlayerInfo playerInfo = GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            playerInfo.ModifyStat(itemParameters, 1); // flag 1, -1
        }
        //장착 list에 in
    }

    // 아이템 장착 해제 시 스탯을 원래대로 돌리는 메서드 (선택 사항)
    public void UnequipItem(EquippableItemSO equippableItem, List<ItemParameter> itemParameters) 
    {
        PlayerInfo playerInfo = GetComponent<PlayerInfo>();
        if (playerInfo != null)
        {
            playerInfo.ModifyStat(itemParameters, -1); // flag -1 장착 해제
        }
    }
    //UI에서 해제 작업 추가

}
