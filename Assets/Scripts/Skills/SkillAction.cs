using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{

    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private Queue<Skill> readyQueue = new Queue<Skill>();
    private GameObject player;
    private bool skillActive = false; //현재 스킬 발동 중 = true
    public float skillStartDelay = 1.0f; // 맨처음 스킬 시작전 딜레이
    
    private void Awake()
    {
        player = gameObject;
        skillActive= true;
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
           
            readyQueue.Enqueue(skill); // 처음엔 모두 발동가능하니 큐에 추가
        }
        StartCoroutine(SkillSequence());
    }
    private IEnumerator SkillSequence()
    {
        while ( readyQueue.Count > 0)
        {
            var skill = readyQueue.Dequeue();
            skillActive = true;
            skill.ActivateSkill(player);
            skillCooldowns[skill] = skill.coolTime;

            yield return new WaitForSeconds(skill.afterDelay);
            skillActive= false;
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
        foreach (var skill in skillList)
        {
            if (skillCooldowns[skill] <= 0 && !readyQueue.Contains(skill))
            {
                readyQueue.Enqueue(skill); // 쿨타임이 끝난 스킬을 큐에 추가
            }
        }
        if(!skillActive && readyQueue.Count > 0 )
        {
            StartCoroutine(SkillSequence());
        }
         
    }

   
   
}
