using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{   //Player�� component ���
    public float moveSpeed = 10f;
    public float magnetDistance = 15f; // ���� �������� ����
    private Transform Player;

    private void Start()
    {
        Player = transform;
    }
    void Update()
    {
        
    }
}
