using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo : MonoBehaviour
{
    public PlayerJob job;
    public Stat stat;
    public event Action OnStatChanged;
    private void Awake()
    {
        stat = new Stat(); // Stat 인스터스화
        stat = stat.StatInitialize(job);
    }

    public void ModifyStat(List<ItemParameter> itemParameters, int flag) // flag 1 = 장착, -1 해제
    {   //값은 여기서 변환
        foreach(var parameter in itemParameters)
        {
            switch (parameter.parameterType)
            {
                case ParameterType.MaxHP:
                    stat.MaxHP = Mathf.Max(stat.MaxHP + parameter.value * flag, 1); // MaxHP는 1 이하로 떨어지지 않도록 처리
                    break;
                case ParameterType.PhyAttack:
                    stat.PhyAttack += parameter.value * flag;
                    break;
                case ParameterType.MagAttack:
                    stat.MagAttack += parameter.value * flag;
                    break;
                case ParameterType.PhyDeffense:
                    stat.PhyDeffense += parameter.value * flag;
                    break;
                case ParameterType.MagDeffense:
                    stat.MagDeffense += parameter.value * flag;
                    break;
                case ParameterType.Speed:
                    stat.Speed += parameter.value * flag;
                    break;
                case ParameterType.CriticalRate:
                    stat.CriticalRate += parameter.value * flag;
                    break;
                case ParameterType.CriticalDamage:
                    stat.CriticalDamage += parameter.value * flag;
                    break;
                case ParameterType.SkillRange:
                    stat.SkillRange += parameter.value * flag;
                    break;
                case ParameterType.SkillCoolDown:
                    stat.SkillCoolDown += parameter.value * flag;
                    break;
                case ParameterType.HealRate:
                    stat.HealRate += parameter.value * flag;
                    break;
                case ParameterType.HpDrainRate:
                    stat.HpDrainRate += parameter.value * flag;
                    break;
            }

        }
        //delegate로 선언
        OnStatChanged?.Invoke();
    }

    //public void ModifyStat(List<BuffSkillParameter> skillParameters, int flag)
    //{

    //}
}

// 현재 스탯 -> 아이템장착으로 인한 스탯 변화,
// -> 버프로 인한 스탯 변화.
// 아이템 여러개만 만들고 스탯 변화주기 -> 아이템 scriptable object로 구현.
// 아이템 장착 구현 UI 우선순위 낮음
// 몬스터 피격