using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private void Start()
    {
        // ��ų���� ��Ÿ�� �ʱ�ȭ
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = 0f; // ó���� ��Ÿ���� 0 (��� ��� ����)
        }
    }
    private void Update()
    {
        // ��Ÿ�� ����
        List<Skill> keys = new List<Skill>(skillCooldowns.Keys);
        foreach (var skill in keys)
        {
            if (skillCooldowns[skill] > 0)
            {
                skillCooldowns[skill] -= Time.deltaTime;
            }
        }
    }
    public void UseSkill(int index)
    {
        if (index >= 0 && index < skillList.Count)
        {
            Skill skill = skillList[index];

            // ��ų�� ��Ÿ���� �������� Ȯ��
            if (skillCooldowns[skill] <= 0)
            {
                // ��ų �ߵ�
                skill.ActivateSkill(gameObject);
                Debug.Log("Fury �ߵ�");
                // ��ų ��Ÿ�� ����
                skillCooldowns[skill] = skill.coolTime;
            }
            
        }
    }
}
