using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public int spawnCont; // 스폰할 아이템 개수
    public GameObject item; // 스폰할 아이템 (골드 프리팹)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면
        {
            for (int i = 0; i < spawnCont; i++) // spawnCont 개수만큼 스폰
            {
                // 아이템을 스포너의 위치에서 생성
                Instantiate(item, transform.position, Quaternion.identity);
                // transform.position은 스포너(이 스크립트가 붙은 오브젝트)의 위치
                // Quaternion.identity는 기본 회전값 (회전 없음)
            }
        }
    }
}