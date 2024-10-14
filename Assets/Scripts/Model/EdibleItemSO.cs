using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction //
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData> ();
        public string ActionName => "Consume";
        //public AudioClip actionSFX => {get; private set;}

        // Affect to Player
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach(ModifierData data in modifiersData)
            {
                data.stateModifier.AffectCharacter(character, data.value);
            }
            return true;
        }

        [SerializeField]
        public class ModifierData
        {
            public CharacterStateModifierSO stateModifier;
            public float value;
        }
    }

}
