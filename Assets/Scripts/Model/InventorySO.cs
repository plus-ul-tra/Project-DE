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
        private List<InventoryItem> inventoryItems;  // 인벤토리 아이템리스트
        [field: SerializeField]
        public int Size { get; private set; } = 10;   // 슬룻 크기
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;    //인벤토리 업데이트 함수

        public void Initialize()  // 인벤토리 초기화 함수
        { 
            inventoryItems = new List<InventoryItem>();  //인벤토리 아이템리스트를 가져옴
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());  // 사이즈 크기만큼 빈슬룻을 추가
            }
        }

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)   // 아이템 추가
        {
            if (item.IsStackable == false)   // 스택형 아이템이 아닌경우 ( 소비형 아이템??)
            { // isnt stackable item
                for (int i = 0; i < inventoryItems.Count; i++)  
                {
                    while(quantity > 0 && IsInventoryFull() == false)  // 수량이 하나 이상이면서 인벤토리가 가득차있지 않다면
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1); // 빈슬룻 수량에서 -1 하고 빈 첫번째 슬룻에 아이템 추가
                        
                    }
                    //bug
                    //quantity = AddStackableItem(item, quantity);
                    InformAboutChange(); // nonStackable == only have 1  // 변경된 현재 인벤토리 상태 적용
                    return quantity; // 남은 빈슬룻 수량 반환
                    
                }
            }
            quantity = AddStackableItem(item,quantity); // 빈슬룻 수량 = 0 
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
