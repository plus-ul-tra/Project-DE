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


// 현재 스탯 -> 아이템장착으로 인한 스탯 변화,
// -> 버프로 인한 스탯 변화.
// 아이템 여러개만 만들고 스탯 변화주기 -> 아이템 scriptable object로 구현.
// 아이템 장착 구현 UI 우선순위 낮음
// 몬스터 피격