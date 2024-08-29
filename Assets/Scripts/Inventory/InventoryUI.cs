using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryUI : MonoBehaviour
{
    Inventory inven;

    public GameObject InventoryPanel;
    public GameObject ShopPanel;
    static bool activeInventory = false;
    public Button closeShop;

    public Slot[] slots;
    public Transform slotHolder;

    void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;   //델리게이트를 통해 SlotChange함수를 호출
        inven.onChangeItem += RedrawSlotUI;
        InventoryPanel.SetActive(activeInventory);
        closeShop.onClick.AddListener(DeactivateShop);

    }

    void SlotChange(int val)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }
    void Update()
    {
            // 먼저 어떤 키가 눌렸는지 확인
            if (Input.anyKeyDown) // 어떤 키든 눌렸다면 true
            {
                // 현재 눌린 키를 가져옵니다.
                KeyCode keyCode = GetCurrentKey();

                // switch 문으로 각 키에 대한 동작을 정의.
                switch (keyCode)
                {
                    case KeyCode.Escape:
                        InventoryPanel.SetActive(false);
                        activeInventory = false;
                        ShopPanel.SetActive(false);
                        break;

                    case KeyCode.I:
                         activeInventory = !activeInventory;
                        InventoryPanel.SetActive(activeInventory);
                        break;
                }
            }

        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼
        {
            RayShop();
        }
    }

    public void AddSlot()  // 슬롯 추가(oneClick 이벤트)
    {
        inven.SlotCnt++;
    }

    // 어떤 키가 눌렸는지 확인하는 함수
    KeyCode GetCurrentKey()
    {
        // 모든 키코드를 순회하면서 눌린 키를 찾습니다.
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode; // 눌린 키를 반환합니다.
            }
        }
        return KeyCode.None; // 눌린 키가 없다면 KeyCode.None을 반환합니다.
    }

    void RayShop()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;
        RaycastHit2D hit2D = Physics2D.Raycast(mousePos, transform.forward, 30); // 레이캐스트 쏘기

        if(hit2D.collider != null) // 레이케스트와 충돌한 충돌체가 있을시 (널이 아닐시)
        {
            if(hit2D.collider.CompareTag("Store")) // 충동체 태그가 Store이면 상점활성화
            {
                ActivateShop(true);
            }
        }
    }
    public void ActivateShop(bool isOpen)  // 상점 활성화
    {
        ShopPanel.SetActive(isOpen);
        InventoryPanel.SetActive(true);
        activeInventory = true;
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    public void DeactivateShop()  // 상점 비활성화
    {
        ShopPanel.SetActive(false);
    }


}
