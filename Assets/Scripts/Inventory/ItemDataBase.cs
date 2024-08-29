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

    public List<Item> itemDB = new List<Item>(); //������ ������ ����Ʈ

    public GameObject fieldItemPrefab; // �ʵ������ ������
    public Vector3[] pos;  // �ʵ� ������ ��ġ 


    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0,3)]);
        }
    }
}
