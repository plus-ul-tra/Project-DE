using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float moveSpeed = 10f;  // 아이템 속도
    public float magnetDistance = 15f;   // 자석 작용 거리

    private Transform Player; // 플레이어 위치

    void Start()
    {
        // 플레이어 오브젝트 찾아서 저장
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 아이템과 플레이어 사이의 거리 계산
        float distance = Vector3.Distance(transform.position, Player.position);

        // 거리가 magnetDistance 이내일 경우 아이템을 플레이어 쪽으로 이동
        if (distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);
        }
    }
}