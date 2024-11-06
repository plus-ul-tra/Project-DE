using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{   //Player에 component 등록
    public float moveSpeed = 10f;
    public float magnetDistance = 15f; // 추후 동적으로 수정
    private Transform Player;

    private void Start()
    {
        Player = transform;
    }
    void Update()
    {
        
    }
}
