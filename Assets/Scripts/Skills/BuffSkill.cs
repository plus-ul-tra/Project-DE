using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffSkill : Skill
{
    private PlayerInfo playerInfo;
    [field: SerializeField]
    //���ӽð�
    public float duration;
    // ��ų �ĵ�, �ĵ��� ������ ���� ��ų �ߵ� (��ų����Ʈ�� ��ġ�� �� �����ϱ� ���� ��)
    [field: SerializeField]
    //public Sprite[] skillSprites;
    public AnimationClip skillAnimation;
    
    public override void ActivateSkill(GameObject user) 
    {
        Debug.Log($"{SkillName} �ߵ�");
    }

    //private IEnumerator SkillDelay(GameObject user)
    //{
    //    //�ߵ�ȿ�� �� Ư��ȿ�� ���
    //    yield return new WaitForSeconds(afterDelay); 
        
    //}

    // ���� ��ų�� ���, ������� �ɷ�ġ�� Ư��ȿ���� �ο���.
    // ��Ÿ��, ���ӽð�, �÷��� ���ݸ� �޶� Sciptable object�� ������ ��.

}

