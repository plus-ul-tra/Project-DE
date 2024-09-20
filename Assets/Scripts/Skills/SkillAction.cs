using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private void Start()
    {
        // 스킬마다 쿨타임 초기화
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = 0f; // 처음엔 쿨타임이 0 (즉시 사용 가능)
        }
    }
    private void Update()
    {
        // 쿨타임 감소
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

            // 스킬의 쿨타임이 끝났는지 확인
            if (skillCooldowns[skill] <= 0)
            {
                // 스킬 발동
                skill.ActivateSkill(gameObject);
                Debug.Log("Fury 발동");
                // 스킬 쿨타임 설정
                skillCooldowns[skill] = skill.coolTime;
            }
            
        }
    }
}
