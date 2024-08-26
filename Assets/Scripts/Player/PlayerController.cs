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
        // 구독신청
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
        //움직임 Key 입력만 Player에서
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = dir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    // Key - 동작
    private void OnKeyBoard()
    {
        // 추후 사용할 키 추가
        // here

    }

    // 마우스 동작
    private void OnMouseClicked()
    {
        // 추후 마우스 관련 기능 추가
    }
}
