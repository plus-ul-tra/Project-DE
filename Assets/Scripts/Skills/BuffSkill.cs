using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffSkill : Skill,IStatHandler
{
    [field: SerializeField]
    private PlayerInfo playerInfo;
    [field: SerializeField]
    //���ӽð�
    public float duration; //���� ���ӽð�
    // ��ų �ĵ�, �ĵ��� ������ ���� ��ų �ߵ� (��ų����Ʈ�� ��ġ�� �� �����ϱ� ���� ��)
    [field: SerializeField]
    //public Sprite[] skillSprites;
    public AnimationClip skillAnimation;
    
    [field: SerializeField]
    public List<ItemParameter> Parameters { get; set;} //Parameter ���
    
    public override void ActivateSkill(GameObject user, PlayerInfo playerInfo) 
    {
        Debug.Log($"{SkillName} �ߵ�");
        StatModifyWithSkill(Parameters, playerInfo, 1); // flag 1 ���� ����, flag  -1 ��������
        // ��ų ����Ʈ
        // ���ݺ���
        // RPC
    }
    public void RemoveSkill(PlayerInfo playerInfo) //GameObject user �Ķ���ʹ� ���� �ʿ��ϸ� ���
    {
        Debug.Log($"{SkillName} ȿ�� ����");
        StatModifyWithSkill(Parameters, playerInfo, -1);

    }

    public void StatModifyWithSkill(List<ItemParameter> Parameters, PlayerInfo playerinfo, int flag)
    {
        if (flag == 1) { 
        playerinfo.ModifyStat(Parameters, flag);
        }
        if(flag == -1)
        {
            playerInfo.ModifyStat(Parameters, flag);
        }
    }

    //private IEnumerator SkillDelay(GameObject user)
    //{
    //    //�ߵ�ȿ�� �� Ư��ȿ�� ���
    //    yield return new WaitForSeconds(afterDelay); 

    //}

    // ���� ��ų�� ���, ������� �ɷ�ġ�� Ư��ȿ���� �ο���.
    // ��Ÿ��, ���ӽð�, �÷��� ���ݸ� �޶� Sciptable object�� ������ ��.

}

