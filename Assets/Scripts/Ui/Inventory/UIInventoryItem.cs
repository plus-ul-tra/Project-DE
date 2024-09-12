using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
        , IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        // Management of slot in Inventory
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text quantityText;
        [SerializeField]
        // select item border
        private Image borderImage;
        private bool empty = true;

        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;
        private void Awake()
        {
            ResetData();
            Deselect();
        }
        public void ResetData()
        {
            this.itemImage.gameObject.SetActive(false);
            this.empty = true;
        }
        public void Deselect()
        {
            this.borderImage.enabled = false;
        }
        public void SetData(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityText.text = quantity + "";
            this.empty = false;
        }
        public void Select()
        { // item that selected
            borderImage.enabled = true;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {

            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                // Right
                OnRightMouseBtnClick?.Invoke(this);
                Debug.Log("Click Action");
            }
            else
            {
                //Left
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty) { return; }
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}