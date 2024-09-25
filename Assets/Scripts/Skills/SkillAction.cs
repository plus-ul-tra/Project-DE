using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{

    public List<Skill> skillList = new List<Skill>();
    private Dictionary<Skill, float> skillCooldowns = new Dictionary<Skill, float>();
    private GameObject player;
    private bool nextSkillAvailable;
    public float skillStartDelay = 1.0f; // ��ó�� ��ų ������ ������
    
    private void Awake()
    {
        player = gameObject;
        nextSkillAvailable= true;
        foreach (var skill in skillList)
        {
            skillCooldowns[skill] = skill.coolTime; // Dictionary ��Ÿ�� �ʱ�ȭ 
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
        foreach (var skill in skillList) // ��Ÿ�� ���Ҹ� ���� for��
        {
            if (skillCooldowns[skill] > 0)
            {
                skillCooldowns[skill] -= Time.deltaTime;
            }
        }

        AvailableBuffSkills(); //������Ʈ ���� ��� skill check , update ��ü���� �ڷ�ƾ�� ȣ���Ѵ�?
    }

    private void AvailableBuffSkills()
    {
        foreach (var skill in skillList)
        {
            // ��Ÿ���� 0 ������ ��ų�� �ߵ�
            if (skillCooldowns[skill] <= 0 && nextSkillAvailable == true)
            {
                nextSkillAvailable = false;
                nextSkillAvailable = skill.ActivateSkill(player);
                
                // ��ų �ߵ� �� �ٽ� ��Ÿ�� �缳��
                skillCooldowns[skill] = skill.coolTime;
            }
        }
    }
   
}
