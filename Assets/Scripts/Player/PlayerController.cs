using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 dir = Vector2.zero; // ???????? ????
    Vector2 mousePosition;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    // Player Speed 
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private UIInventoryPage inventoryUI;
    public int inventorySize = 10;


    void Start()
    {
        // ????????
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        //Managers.Input.MouseAction -= OnMouseClicked;
        //Managers.Input.MouseAction += OnMouseClicked;
        // inventory √ ±‚»≠
        inventoryUI.InitializeInventoryUI(inventorySize);

    }
    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
        spriter =GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        //?????? Key ?????? Player????
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        // ???????? ?????? ????
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
        // Inventory toggle
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();

            }
            else
            {
                inventoryUI.Hide();
            }
        }


    }

    // ?????? ???? ????
    private void OnMouseClicked()
    {
        // ???? ?????? ???? ???? ????
    }
}
