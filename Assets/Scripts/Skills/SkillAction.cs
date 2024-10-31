using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    // buff와 active 스킬을 같이 관리하지 말고, 따로 하는게 좋을 것 같음. 현재 script에는 전부 buff skill만
    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private Queue<Skill> readyQueue = new Queue<Skill>();
    private GameObject player;
    private bool skillActive = false; //현재 스킬 발동 중 = true
    private Dictionary<BuffSkill, Coroutine> activeBuffCoroutines = new Dictionary<BuffSkill, Coroutine>(); //발동되고 종료 coroutine이 실행되고 있는 Buff Dictionary
    public float skillStartDelay = 1.0f; // 맨처음 스킬 시작전 딜레이

    public PlayerInfo playerInfo;
    private void Awake()
    {
        player = gameObject;
        skillActive= true;
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = skill.coolTime; // Dictionary 쿨타임 초기화 
        }
        playerInfo = player.GetComponent<PlayerInfo>();
    }
    private void Start()
    {
        StartCoroutine(FirstSkillActivate());
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
    public void ApplyBuff(BuffSkill buff)
    {
        if (!activeBuffCoroutines.ContainsKey(buff))
        {
            buff.ActivateSkill(player, playerInfo);
            Coroutine buffCoroutine = StartCoroutine(RemoveBuffAfterDuration(buff, buff.duration));
            activeBuffCoroutines[buff] = buffCoroutine;
        }
    }

    private IEnumerator FirstSkillActivate()
    {
        yield return new WaitForSeconds(skillStartDelay);

        foreach (var skill in skillList)
        {

            readyQueue.Enqueue(skill); // 처음엔 모두 발동가능하니 큐에 추가
        }
        StartCoroutine(SkillSequence());
    }

    private IEnumerator SkillSequence()
    {
        while (readyQueue.Count > 0)
        {
            var skill = readyQueue.Dequeue();
            skillActive = true;

            skill.ActivateSkill(player, playerInfo); //실제 스킬 발동 // ApplyBuff()로 교체

            skillCooldowns[skill] = skill.coolTime;

            yield return new WaitForSeconds(skill.afterDelay);
            skillActive = false;
        }
    }

    private IEnumerator RemoveBuffAfterDuration(BuffSkill buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        buff.RemoveSkill(playerInfo);
        activeBuffCoroutines.Remove(buff);
    }
}
