using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private UIInventoryDescription itemDescription;
    [SerializeField]
    private MouseFollower mouseFollower; 
    // item list
    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    // Item index that dragging on mouse.
    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;


    private void Awake()
    {
        Hide();
        
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(int inventorysize)
    {
        for( int i = 0;i< inventorysize;i++)
        {
            // PreFabs ����
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero,Quaternion.identity);
            //contentPanel�� �ڽ����� ���� �� listOfUIItems�� �ֱ�
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);

            //delegate ����
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if(listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
       
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {   
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        //listOfUIItems[currentlyDraggedItemIndex].SetData(index == 0 ? image : image2, quantity);
        //listOfUIItems[index].SetData(currentlyDraggedItemIndex == 0 ? image : image2, quantity);
        //mouseFollower.Toggle(false);
        //currentlyDraggedItemIndex = -1;
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        // Item index that dragging on mouse.
        currentlyDraggedItemIndex = -1;  
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if(index == -1)
        {
            return; 
        }
        // Item index that dragging on mouse.
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);  
    }

    public void Show()
    {   
        gameObject.SetActive(true);
        ResetSelection();
    }
    private void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach(UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

}
