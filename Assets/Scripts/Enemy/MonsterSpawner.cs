using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 여러 몬스터 프리팹을 지정하는 리스트
    public List<GameObject> monsterPrefabs;

    // 몬스터 생성 주기를 설정하는 변수
    public float spawnInterval = 3.0f;

    // 각 몬스터 프리팹에 대한 풀 크기를 설정하는 변수
    public int poolSize;

    // 스포너가 활성화될 때 실행되는 메서드
    void Start()
    {
        // 각 몬스터 프리팹에 대해 풀을 초기화
        foreach (GameObject prefab in monsterPrefabs)
        {
            Managers.Pool.InitializePool(prefab, poolSize);
        }

        // 일정 시간 간격으로 몬스터를 한 번씩 스폰
        InvokeRepeating("SpawnMonster", spawnInterval, spawnInterval);
    }

    // 몬스터를 생성하는 메서드
    void SpawnMonster()
    {
        // 랜덤하게 몬스터 프리팹을 선택
        GameObject selectedPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];

        // 오브젝트 풀에서 몬스터를 가져옴
        GameObject monster = Managers.Pool.GetObjectFromPool(selectedPrefab);

        // 풀에 남아 있는 몬스터가 없는 경우 스폰하지 않음
        if (monster != null)
        {
            // 가져온 몬스터의 위치를 설정
            monster.transform.position = transform.position;

            // 몬스터를 활성화
            monster.SetActive(true);
        }
    }
}