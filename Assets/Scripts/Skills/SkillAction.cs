using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{

    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private GameObject player;

    private void Awake()
    {
        player = gameObject;
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = skill.coolTime; // Dictionary 쿨타임 초기화 
        }
    }
    private void Start()
    {
        
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

        AvailableSkills(); //업데이트 동안 계속 skill check
    }

    private void AvailableSkills()
    {
        foreach (var skill in skillList)
        {
            // 쿨타임이 0 이하인 스킬만 발동
            if (skillCooldowns[skill] <= 0)
            {
                skill.ActivateSkill(player);

                // 스킬 발동 후 다시 쿨타임 재설정
                skillCooldowns[skill] = skill.coolTime;
            }
        }
    }
   
}
