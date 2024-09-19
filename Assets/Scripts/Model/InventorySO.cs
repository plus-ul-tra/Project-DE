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
                // Empty Struct�� �ʱ�ȭ
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            { // nonstackable
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false) //quantity�� 0�̻� and Full�� �ƴ� ���
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1); // quantity�� parameter�� 1�̸� �ϳ��� �ִ´ٴ� ��

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
        { // ���ο� slot�� item ����ü�� ����

            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
            };
            for(int i = 0; i< inventoryItems.Count; i++) //inventoryItems - slot
            {
                if (inventoryItems[i].IsEmpty) // i ��° slot�� Empty�� ���
                {
                    inventoryItems[i] = newItem; // �������� �ִ� ����ü�� �װ��� ����
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull() // �� ������ �ϳ��� ���� ��� true
        => inventoryItems.Where(item => item.IsEmpty).Any() == false; 

        private int AddStackableItem(ItemSO item, int quantity) //quantity -> �߰��Ϸ��� ����
        {
            for(int i = 0; i < inventoryItems.Count; i++) //inventoryItems.Count slot ����
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                
                if (inventoryItems[i].item.ID == item.ID) // ������ ID�� �������� �̹� inventory�� �����ϴ� ���
                {
                    // left quantity can stack
                    int amoutPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if(quantity > amoutPossibleToTake)
                    {
                       
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amoutPossibleToTake; // �׾ƾ� �� ���� ������Ʈ �� while ������
                    }
                    else
                    {   // ���� ������ ���� ���� �� �ִ� ��� �װ� return 0���� �Լ� ����
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }// �ش� for ���� ó���� �ι�° if �� ID�� ���ؼ� ������ item�� �ִ��� ã�°� 

            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity); 
            }
            return quantity; // quantity �� ��ü�� 0 ������, inventoryFull �� ��츸 quantity != 0
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
    {   //���� �κ��丮�� ����Ǵ� ����ü.
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null; // item null�̸� ture ��ȯ

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
