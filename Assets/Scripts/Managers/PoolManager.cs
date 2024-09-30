using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 풀링할 오브젝트를 저장할 딕셔너리 (각 프리팹마다 별도의 풀을 관리)
    private Dictionary<GameObject, Queue<GameObject>> objectPools;

    // 변수 초기화 메서드
    private void Awake()
    {
        // 오브젝트 풀 초기화
        objectPools = new Dictionary<GameObject, Queue<GameObject>>();
    }

    // 오브젝트 풀 초기화 메서드
    public void InitializePool(GameObject prefab, int poolSize)
    {
        if (!objectPools.ContainsKey(prefab))
        {
            // 해당 프리팹에 대한 새로운 큐 생성
            objectPools[prefab] = new Queue<GameObject>();
        }

        // 프리팹에 해당하는 풀에 오브젝트 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPools[prefab].Enqueue(obj);
        }
    }

    // 풀에서 오브젝트를 가져오는 메서드
    public GameObject GetObjectFromPool(GameObject prefab)
    {
        // 해당 프리팹에 대한 풀에 오브젝트가 있는지 확인
        if (objectPools.ContainsKey(prefab) && objectPools[prefab].Count > 0)
        {
            GameObject obj = objectPools[prefab].Dequeue();
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
            else
            {
                objectPools[prefab].Enqueue(obj);
            }
        }

        // 풀에 오브젝트가 없으면 null 반환
        return null;
    }

    // 오브젝트를 다시 풀에 반환하는 메서드
    public void ReturnObjectToPool(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        if (objectPools.ContainsKey(prefab))
        {
            objectPools[prefab].Enqueue(obj);
        }
    }
}