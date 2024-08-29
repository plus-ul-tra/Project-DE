using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;

    void Awake()
    {
        instance = this;
        
    }

    public List<Item> itemDB = new List<Item>(); //아이템 데이터 리스트

    public GameObject fieldItemPrefab; // 필드아이템 프리팹
    public Vector3[] pos;  // 필드 아이템 위치 


    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0,3)]);
        }
    }
}
