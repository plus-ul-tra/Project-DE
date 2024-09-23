using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{

    public List<BuffSkill> skillList = new List<BuffSkill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private GameObject player;

    private void Awake()
    {
        player = gameObject;
    }
    private void Start()
    {
        // ��ų���� ��Ÿ�� �ʱ�ȭ
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = skill.coolTime; // Dictionary ��Ÿ�� �ʱ�ȭ (��� ��� ����)
        }
    }
    private void Update()
    {
        // ��Ÿ�� ����
        //List<Skill> keys = new List<Skill>(skillCooldowns.Keys);

        foreach (var skill in skillList) // ��Ÿ�� ���Ҹ� ���� for��
        {
            if (skillCooldowns[skill] > 0)
            {
                skillCooldowns[skill] -= Time.deltaTime;
            }
        }

        ActivateAvailableSkills();
    }

    private void ActivateAvailableSkills()
    {
        foreach (var skill in skillList)
        {
            // ��Ÿ���� 0 ������ ��ų�� �ߵ�
            if (skillCooldowns[skill] <= 0)
            {
                skill.ActivateSkill(player);
                Debug.Log($"{skill.SkillName} �ߵ�");

                // ��ų �ߵ� �� �ٽ� ��Ÿ�� �缳��
                skillCooldowns[skill] = skill.coolTime;
            }
        }
    }
    //public void UseSkill(int index)
    //{
    //    if (index >= 0 && index < skillList.Count)
    //    {
    //        Skill skill = skillList[index];

    //        // ��ų�� ��Ÿ���� �������� Ȯ��
    //        if (skillCooldowns[skill] <= 0)
    //        {
    //            // ��ų �ߵ�
    //            skill.ActivateSkill(player);
    //            Debug.Log("Fury �ߵ�");
    //            // ��ų ��Ÿ�� ����
    //            skillCooldowns[skill] = skill.coolTime;
    //        }
            
    //    }
    //}
}
