using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    
    public abstract class ItemSO : ScriptableObject 
    {
        // Information about items that recived
        [field: SerializeField]
        public bool IsStackable { get; set; }
        public int ID => GetInstanceID(); // unity에서 고유 ID 제공.

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }
        [field: SerializeField]
        public Sprite ItemImage { get; set; }

    }

    public interface IDestroyableItem
    {
        // not implement yet
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        //public AudioClip actionSFX { get; }

        bool PerformAction(GameObject character);
    }
    [Serializable]
    public class ModifierData
    {
        public CharacterStateModifierSO stateModifier;
        public float value;
    }
}
