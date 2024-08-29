using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singletone
    public static Inventory instance;  
    private void Awake()
    {
        if(instance != null)  // 중복된 인스턴스가 있을시
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
        get => slotCnt;  // slotCnt의 값을 반환
        set
        {
            slotCnt = value;  // slotCnt에 value값을 저장 (SlotCnt 값)
            onSlotCountChange.Invoke(slotCnt);   // onSlotCountChange 댈리게이트가 가지고 있는 slotCnt타입을 매개변수로 갖는 함수 실행 후 slotCnt 값 갱신
                                                 // 갱신된 slotCnt값은 get을 통해 SlotCnt로 return 된다.
        }
    }

    private void Start()
    {
        SlotCnt = 4;
    }

    public bool Additem(Item _item) // 인벤토리에 아이템 추가할지(bool 값)
    {
        if(items.Count < SlotCnt)  // 인벤토리 슬룻갯수가 아이템 갯수보다 많다면
        {
            items.Add(_item);  // 인벤토리에 아이템 추가
            if(onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;  // 그렇지 않다면 false로 하여 아이템을 줍지 못하게
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

