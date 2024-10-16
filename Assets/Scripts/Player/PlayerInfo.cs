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
        stat = new Stat(); // Stat �ν��ͽ�ȭ
        stat = stat.StatInitialize(job);
    }

    public void ModifyStat(List<ItemParameter> itemParameters, int flag) // flag 1 = ����, -1 ����
    {   //���� ���⼭ ��ȯ
        foreach(var parameter in itemParameters)
        {
            switch (parameter.parameterType)
            {
                case ParameterType.MaxHP:
                    stat.MaxHP = Mathf.Max(stat.MaxHP + parameter.value * flag, 1); // MaxHP�� 1 ���Ϸ� �������� �ʵ��� ó��
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
        //delegate�� ����
        OnStatChanged?.Invoke();
    }

    //public void ModifyStat(List<BuffSkillParameter> skillParameters, int flag)
    //{

    //}
}

// ���� ���� -> �������������� ���� ���� ��ȭ,
// -> ������ ���� ���� ��ȭ.
// ������ �������� ����� ���� ��ȭ�ֱ� -> ������ scriptable object�� ����.
// ������ ���� ���� UI �켱���� ����
// ���� �ǰ�