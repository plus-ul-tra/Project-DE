using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{

    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private GameObject player;
    private bool nextSkillAvailable;
    public float skillStartDelay = 1.0f; // 맨처음 스킬 시작전 딜레이
    
    private void Awake()
    {
        player = gameObject;
        nextSkillAvailable= true;
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = skill.coolTime; // Dictionary 쿨타임 초기화 
        }
    }
    private void Start()
    {
        StartCoroutine(FirstSkillActivate());
    }
    private IEnumerator FirstSkillActivate()
    {
        yield return new WaitForSeconds(skillStartDelay);
        foreach(var skill in skillList)
        {
            if(nextSkillAvailable == true)
            {
                nextSkillAvailable = false;
                nextSkillAvailable = skill.ActivateSkill(player);
                skillCooldowns[skill] = skill.coolTime;
            }
            
        }
        
    }
    private void Update()
    {
        foreach (var skill in skillList) // 쿨타임 감소를 위한 for문
        {
            if (skillCooldowns[skill] > 0)
            {
                skillCooldowns[skill] -= Time.deltaTime;
            }
        }

        AvailableBuffSkills(); //업데이트 동안 계속 skill check , update 자체에서 코루틴을 호출한다?
    }

    private void AvailableBuffSkills()
    {
        foreach (var skill in skillList)
        {
            // 쿨타임이 0 이하인 스킬만 발동
            if (skillCooldowns[skill] <= 0 && nextSkillAvailable == true)
            {
                nextSkillAvailable = false;
                nextSkillAvailable = skill.ActivateSkill(player);
                
                // 스킬 발동 후 다시 쿨타임 재설정
                skillCooldowns[skill] = skill.coolTime;
            }
        }
    }
   
}
