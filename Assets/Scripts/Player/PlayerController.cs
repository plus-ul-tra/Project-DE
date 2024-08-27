using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 dir = Vector2.zero; // 플레이어 벡터
    Vector2 mousePosition;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
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
        spriter =GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        //움직임 Key 입력만 Player에서
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        // 화면상의 마우스 좌표
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition);
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = dir.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        // animator parameter의 speed의 값 변경에 따른 Animation 재생
        anim.SetFloat("Speed", dir.magnitude);

        //mouseposition에 따라 flipX 결정
        if(transform.position.x > mousePosition.x )
        {
            spriter.flipX = true;
        }
        else
        {
            spriter.flipX=false;
        }

    }

    // Key - 동작
    private void OnKeyBoard()
    {
        // 추후 사용할 키 추가 ex) 상호작용, 스킬, 등
        // here

    }

    // 마우스 클릭 동작
    private void OnMouseClicked()
    {
        // 추후 마우스 관련 기능 추가
    }
}
