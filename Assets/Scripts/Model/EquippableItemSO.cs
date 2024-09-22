using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model 
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if(weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? DefualtParametersList : itemState);
                return true;
            }


            return false;
        }
    }
}
