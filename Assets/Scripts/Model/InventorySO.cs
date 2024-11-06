using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;  // �κ��丮 �����۸���Ʈ
        [field: SerializeField]
        public int Size { get; private set; } = 10;   // ���� ũ��
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;    //�κ��丮 ������Ʈ �Լ�

        public void Initialize()  // �κ��丮 �ʱ�ȭ �Լ�
        { 
            inventoryItems = new List<InventoryItem>();  //�κ��丮 �����۸���Ʈ�� ������
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());  // ������ ũ�⸸ŭ �󽽷��� �߰�
            }
        }

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)   // ������ �߰�
        {
            if (item.IsStackable == false)   // ������ �������� �ƴѰ�� ( �Һ��� ������??)
            { // isnt stackable item
                for (int i = 0; i < inventoryItems.Count; i++)  
                {
                    while(quantity > 0 && IsInventoryFull() == false)  // ������ �ϳ� �̻��̸鼭 �κ��丮�� ���������� �ʴٸ�
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1); // �󽽷� �������� -1 �ϰ� �� ù��° ���� ������ �߰�
                        
                    }
                    //bug
                    //quantity = AddStackableItem(item, quantity);
                    InformAboutChange(); // nonStackable == only have 1  // ����� ���� �κ��丮 ���� ����
                    return quantity; // ���� �󽽷� ���� ��ȯ
                    
                }
            }
            quantity = AddStackableItem(item,quantity); // �󽽷� ���� = 0 
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
            };
            for(int i = 0; i< inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull() // Check inventory to figure out empty or not
        => inventoryItems.Where(item => item.IsEmpty).Any() == false; 

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for(int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                
                if (inventoryItems[i].item.ID == item.ID)
                {
                    // left quantity can stack
                    int amoutPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if(quantity > amoutPossibleToTake)
                    {
                        //Possible to stack
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amoutPossibleToTake;
                    }
                    else
                    {
                        // Disable to stack
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity); 
            }
            return quantity;
        }


        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);

                InformAboutChange();
            }
        }
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);  
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }

            return returnValue; // return is Deictionary
        }

        internal InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            // implement of Swap Action.
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();    
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItem 
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        // To modify Struct's Value
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
                itemState = new List<ItemParameter>()
            };
    }
}
