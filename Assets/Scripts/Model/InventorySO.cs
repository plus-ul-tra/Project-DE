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
        private List<InventoryItem> inventoryItems;
        [field: SerializeField]
        public int Size { get; private set; } = 10;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; 

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                // Empty Struct로 초기화
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            { // nonstackable
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false) //quantity가 0이상 and Full이 아닌 경우
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1); // quantity의 parameter가 1이면 하나씩 넣는다는 뜻

                    }
                    //bug
                    //quantity = AddStackableItem(item, quantity);
                    InformAboutChange(); // nonStackable == only have 1
                    return quantity;
                    
                }
            }
            quantity = AddStackableItem(item,quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity) 
        { // 새로운 slot에 item 구조체를 저장

            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
            };
            for(int i = 0; i< inventoryItems.Count; i++) //inventoryItems - slot
            {
                if (inventoryItems[i].IsEmpty) // i 번째 slot이 Empty인 경우
                {
                    inventoryItems[i] = newItem; // 아이템이 있는 구조체를 그곳에 삽입
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull() // 빈 슬롯이 하나도 없는 경우 true
        => inventoryItems.Where(item => item.IsEmpty).Any() == false; 

        private int AddStackableItem(ItemSO item, int quantity) //quantity -> 추가하려는 수량
        {
            for(int i = 0; i < inventoryItems.Count; i++) //inventoryItems.Count slot 개수
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                
                if (inventoryItems[i].item.ID == item.ID) // 동일한 ID의 아이템이 이미 inventory에 존재하는 경우
                {
                    // left quantity can stack
                    int amoutPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if(quantity > amoutPossibleToTake)
                    {
                       
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amoutPossibleToTake; // 쌓아야 할 수량 업데이트 후 while 문으로
                    }
                    else
                    {   // 남은 공간에 전부 쌓을 수 있는 경우 쌓고 return 0으로 함수 종료
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }// 해당 for 문의 처음과 두번째 if 는 ID를 통해서 동일한 item이 있는지 찾는것 

            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity); 
            }
            return quantity; // quantity 는 대체로 0 이지만, inventoryFull 인 경우만 quantity != 0
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
    {   //실제 인벤토리에 저장되는 구조체.
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null; // item null이면 ture 반환

        // To modify Struct's Value
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
            };
    }
}
