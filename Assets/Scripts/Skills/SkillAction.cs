using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    // buff�� active ��ų�� ���� �������� ����, ���� �ϴ°� ���� �� ����. ���� script���� ���� buff skill��
    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private Queue<Skill> readyQueue = new Queue<Skill>();
    private GameObject player;
    private bool skillActive = false; //���� ��ų �ߵ� �� = true
    private Dictionary<BuffSkill, Coroutine> activeBuffCoroutines = new Dictionary<BuffSkill, Coroutine>(); //�ߵ��ǰ� ���� coroutine�� ����ǰ� �ִ� Buff Dictionary
    public float skillStartDelay = 1.0f; // ��ó�� ��ų ������ ������

    public PlayerInfo playerInfo;
    private void Awake()
    {
        player = gameObject;
        skillActive= true;
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = skill.coolTime; // Dictionary ��Ÿ�� �ʱ�ȭ 
        }
        playerInfo = player.GetComponent<PlayerInfo>();
    }
    private void Start()
    {
        StartCoroutine(FirstSkillActivate());
    }

    private void Update()
    {
        foreach (var skill in skillList) // ��Ÿ�� ���Ҹ� ���� for��
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
                readyQueue.Enqueue(skill); // ��Ÿ���� ���� ��ų�� ť�� �߰�
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

            readyQueue.Enqueue(skill); // ó���� ��� �ߵ������ϴ� ť�� �߰�
        }
        StartCoroutine(SkillSequence());
    }

    private IEnumerator SkillSequence()
    {
        while (readyQueue.Count > 0)
        {
            var skill = readyQueue.Dequeue();
            skillActive = true;

            skill.ActivateSkill(player, playerInfo); //���� ��ų �ߵ� // ApplyBuff()�� ��ü

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
