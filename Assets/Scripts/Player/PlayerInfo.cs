using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo : MonoBehaviour
{
    public PlayerJob job;
    public Stat stat;

   

    private void Awake()
    {
        stat = new Stat();
        stat = stat.StatInitialize(job);
    }



}


// ���� ���� -> �������������� ���� ���� ��ȭ,
// -> ������ ���� ���� ��ȭ.
// ������ �������� ����� ���� ��ȭ�ֱ� -> ������ scriptable object�� ����.
// ������ ���� ���� UI �켱���� ����
// ���� �ǰ�