using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 dir = Vector2.zero;

    // Player Speed 
    [SerializeField] private float speed = 10.0f;

    
    void Start()
    {
        // ������û
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard; 
        //Managers.Input.MouseAction -= OnMouseClicked;
        //Managers.Input.MouseAction += OnMouseClicked;

    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        //������ Key �Է¸� Player����
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = dir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    // Key - ����
    private void OnKeyBoard()
    {
        // ���� ����� Ű �߰�
        // here

    }

    // ���콺 ����
    private void OnMouseClicked()
    {
        // ���� ���콺 ���� ��� �߰�
    }
}
