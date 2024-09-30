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
    private List<ItemParameter> itemCurrentState; // 파라미터 장비 스텟
    
    public void SetEquipment(EquippableItemSO equipmentItemSO, List<ItemParameter> itemState)
    {
        if(weapon != null) //다른게 장착되어 있는 경우
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState); //장착되어 있는거 인벤토리로
            UnequipItem(equipmentItemSO, itemCurrentState);
        }
        this.weapon = equipmentItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        EquipItem(equipmentItemSO, itemState);

    } // weapon이라는 것은 현재 장착되어 있는 아이템

    // 아이템 장착 시 스탯을 올려주는 메서드
    // 스텟적용부분/ 여기서 관리할지, playerInfo에서 관리할지
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
