
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model; // namespace Á¤ÀÇ
using Inventory.UI; // namespace Á¤ÀÇ

// Check user action, Update Model
namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUI;
        [SerializeField] private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();   

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            Managers.Input.KeyAction -= OnKeyBoardForInventory;
            Managers.Input.KeyAction += OnKeyBoardForInventory;
        }

        private void PrepareUI()
        {
            //View
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest; //ÀåÂø ºÎ
        }
        private void PrepareInventoryData()
        {
            //Model
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach(InventoryItem item in initialItems) 
            {
                if (item.IsEmpty)
                {
                    continue;
                }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }

        }

        private void HandleItemActionRequest(int itemIndex) //ÀåÂø µ¿ÀÛ
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if(destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if(itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState); //ÀåÂø?
            }

        }

        private void HandleDragging(int itemIndex)
        {
            // Drag Action
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if(inventoryItem.IsEmpty)
            {
                return;
            }
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        private void OnKeyBoardForInventory() // Inventory UI Ã¢
        {
            // Inventory toggle
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())  // return Dictionary
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity); // view update
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }


        }
    }
}