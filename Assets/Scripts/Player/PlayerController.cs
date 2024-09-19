using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInfo playerInfo;
    Vector2 dir = Vector2.zero; 
    Vector2 mousePosition;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    // Player Speed 
    //private float speed = 10.0f;
    
    void Start()
    {
        // ????????
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        //Managers.Input.MouseAction -= OnMouseClicked;
        //Managers.Input.MouseAction += OnMouseClicked;
        // inventory √ ±‚»≠

    }
    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();    
        rigid = GetComponent<Rigidbody2D>();
        spriter =GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePosition);
    }
    private void FixedUpdate()
    {
        Vector2 nextVec = dir.normalized * playerInfo.stat.Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        // animator parameter?? speed?? ?? ?????? ???? Animation ????
        anim.SetFloat("Speed", dir.magnitude);

        //mouseposition?? ???? flipX ????
        if(transform.position.x > mousePosition.x )
        {
            spriter.flipX = true;
        }
        else
        {
            spriter.flipX=false;
        }

    }

    // Key - ????
    private void OnKeyBoard()
    {


    }

    // ?????? ???? ????
    private void OnMouseClicked()
    {
        // ???? ?????? ???? ???? ????
    }
}
