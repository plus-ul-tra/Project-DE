using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model 
{   
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        [field: SerializeField]
        public EquipmentType equipmentType;

        // 착용가능 직업 추가

        // 세트효과 or class 지정을 위한 태그 추가

        public bool PerformAction(GameObject player, List<ItemParameter> itemState = null)
        {
            AgentEquipment equipmentSystem = player.GetComponent<AgentEquipment>();
            if(equipmentSystem != null)
            {
                equipmentSystem.SetEquipment(this, itemState == null ? DefualtParametersList : itemState, equipmentType); //
                return true;
            }
            return false;
        }
    }
}
public enum EquipmentType
{
    Weapon,
    Hat,
    Armor,
    Phants,
    Cape,
    Glove,
    Shose,
    Accessory,

}
