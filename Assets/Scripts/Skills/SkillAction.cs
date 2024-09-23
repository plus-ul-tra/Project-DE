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
            skillCooldowns[skill] = skill.coolTime; // Dictionary ��Ÿ�� �ʱ�ȭ 
        }
    }
    private void Start()
    {
        
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

        AvailableSkills(); //������Ʈ ���� ��� skill check
    }

    private void AvailableSkills()
    {
        foreach (var skill in skillList)
        {
            // ��Ÿ���� 0 ������ ��ų�� �ߵ�
            if (skillCooldowns[skill] <= 0)
            {
                skill.ActivateSkill(player);

                // ��ų �ߵ� �� �ٽ� ��Ÿ�� �缳��
                skillCooldowns[skill] = skill.coolTime;
            }
        }
    }
   
}
