using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singletone
    public static Inventory instance;  
    private void Awake()
    {
        if(instance != null)  // �ߺ��� �ν��Ͻ��� ������
        {
            Destroy(gameObject);  
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;  // slotCnt�� ���� ��ȯ
        set
        {
            slotCnt = value;  // slotCnt�� value���� ���� (SlotCnt ��)
            onSlotCountChange.Invoke(slotCnt);   // onSlotCountChange �����Ʈ�� ������ �ִ� slotCntŸ���� �Ű������� ���� �Լ� ���� �� slotCnt �� ����
                                                 // ���ŵ� slotCnt���� get�� ���� SlotCnt�� return �ȴ�.
        }
    }

    private void Start()
    {
        SlotCnt = 4;
    }

    public bool Additem(Item _item) // �κ��丮�� ������ �߰�����(bool ��)
    {
        if(items.Count < SlotCnt)  // �κ��丮 �������� ������ �������� ���ٸ�
        {
            items.Add(_item);  // �κ��丮�� ������ �߰�
            if(onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;  // �׷��� �ʴٸ� false�� �Ͽ� �������� ���� ���ϰ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("FieldItem"))
        {
            FieldItem fieldItem = collision.GetComponent<FieldItem>();
            if (Additem(fieldItem.GetItem()))
                fieldItem.DestroyItem();
        }    
    }
}

