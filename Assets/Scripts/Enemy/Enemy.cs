using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;  // 적의 이동 속도
    public float health; // 적의 현재 체력
    public float maxHealth; // 적의 최대 체력
    public GameObject expPrefab; // 적이 사망 시 드롭할 경험치 프리팹

    public bool isPlayerAlive = true; // 플레이어가 살아있는지 여부

    private bool isLive; // 적이 살아있는지 여부를 나타내는 플래그
    private Transform player; // 플레이어의 Transform을 저장할 변수

    // 적의 물리적 및 시각적 요소를 관리하기 위한 컴포넌트
    private Rigidbody2D rigid; // 적의 Rigidbody2D 컴포넌트
    private Collider2D coll; // 적의 Collider2D 컴포넌트

    void Awake()
    {
        // 필요한 컴포넌트들을 초기화
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Start()
    {
        // "Player" 태그를 가진 오브젝트의 Transform을 찾음
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // 플레이어의 Transform을 저장
        }
    }

    void FixedUpdate()
    {
        // 플레이어가 살아있고 적이 살아있으면 추적
        if (player != null && isLive)
        {
            // 플레이어와 적 사이의 방향 벡터 계산
            Vector2 dirVec = player.position - transform.position;
            // 방향 벡터를 정규화하고 속도를 곱해 이동할 다음 위치 계산
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            // 적의 현재 위치를 계산된 위치로 이동
            rigid.MovePosition((Vector2)transform.position + nextVec);
        }
    }

    void LateUpdate()
    {
        // 플레이어가 죽었다면 적의 행동 중지
        if (!isPlayerAlive)
        {
            return;
        }

        // 적이 살아있지 않으면 동작하지 않음
        if (!isLive)
        {
            return;
        }
    }

    // 적이 활성화될 때 호출되는 함수
    void OnEnable()
    {
        // 적의 초기 상태를 설정
        isLive = true; // 적이 살아있음을 설정
        coll.enabled = true; // 적의 충돌 처리를 활성화
        rigid.simulated = true; // 물리 연산을 활성화
        health = maxHealth; // 적의 체력을 최대 체력으로 설정
    }

    // 플레이어와 충돌했을 때 적이 사망하는 메서드
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 "Player" 태그를 가지고 있는지 확인
        if (collision.CompareTag("Player"))
        {
            // 적을 사망 처리
            Dead();
        }
    }
    */
    // 적이 사망 상태로 전환될 때 호출되는 함수
    void Dead()
    {
        // 적을 비활성화하여 게임에서 제거
        gameObject.SetActive(false);

        // 사망 시 경험치 프리팹 드롭
        Instantiate(expPrefab, transform.position, Quaternion.identity);
    }
}