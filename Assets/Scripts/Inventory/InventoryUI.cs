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
        inven.onSlotCountChange += SlotChange;   //��������Ʈ�� ���� SlotChange�Լ��� ȣ��
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
            // ���� � Ű�� ���ȴ��� Ȯ��
            if (Input.anyKeyDown) // � Ű�� ���ȴٸ� true
            {
                // ���� ���� Ű�� �����ɴϴ�.
                KeyCode keyCode = GetCurrentKey();

                // switch ������ �� Ű�� ���� ������ ����.
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

        if (Input.GetMouseButton(0)) // ���콺 ���� ��ư
        {
            RayShop();
        }
    }

    public void AddSlot()  // ���� �߰�(oneClick �̺�Ʈ)
    {
        inven.SlotCnt++;
    }

    // � Ű�� ���ȴ��� Ȯ���ϴ� �Լ�
    KeyCode GetCurrentKey()
    {
        // ��� Ű�ڵ带 ��ȸ�ϸ鼭 ���� Ű�� ã���ϴ�.
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode; // ���� Ű�� ��ȯ�մϴ�.
            }
        }
        return KeyCode.None; // ���� Ű�� ���ٸ� KeyCode.None�� ��ȯ�մϴ�.
    }

    void RayShop()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;
        RaycastHit2D hit2D = Physics2D.Raycast(mousePos, transform.forward, 30); // ����ĳ��Ʈ ���

        if(hit2D.collider != null) // �����ɽ�Ʈ�� �浹�� �浹ü�� ������ (���� �ƴҽ�)
        {
            if(hit2D.collider.CompareTag("Store")) // �浿ü �±װ� Store�̸� ����Ȱ��ȭ
            {
                ActivateShop(true);
            }
        }
    }
    public void ActivateShop(bool isOpen)  // ���� Ȱ��ȭ
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

    public void DeactivateShop()  // ���� ��Ȱ��ȭ
    {
        ShopPanel.SetActive(false);
    }


}
