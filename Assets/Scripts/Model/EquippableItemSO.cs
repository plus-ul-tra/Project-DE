using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model 
{
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        public bool PerformAction(GameObject character)
        {
            throw new System.NotImplementedException();
        }
    }
}
