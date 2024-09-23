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
        // 스킬마다 쿨타임 초기화
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = skill.coolTime; // Dictionary 쿨타임 초기화 (즉시 사용 가능)
        }
    }
    private void Update()
    {
        // 쿨타임 감소
        //List<Skill> keys = new List<Skill>(skillCooldowns.Keys);

        foreach (var skill in skillList) // 쿨타임 감소를 위한 for문
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
            // 쿨타임이 0 이하인 스킬만 발동
            if (skillCooldowns[skill] <= 0)
            {
                skill.ActivateSkill(player);
                Debug.Log($"{skill.SkillName} 발동");

                // 스킬 발동 후 다시 쿨타임 재설정
                skillCooldowns[skill] = skill.coolTime;
            }
        }
    }
    //public void UseSkill(int index)
    //{
    //    if (index >= 0 && index < skillList.Count)
    //    {
    //        Skill skill = skillList[index];

    //        // 스킬의 쿨타임이 끝났는지 확인
    //        if (skillCooldowns[skill] <= 0)
    //        {
    //            // 스킬 발동
    //            skill.ActivateSkill(player);
    //            Debug.Log("Fury 발동");
    //            // 스킬 쿨타임 설정
    //            skillCooldowns[skill] = skill.coolTime;
    //        }
            
    //    }
    //}
}
