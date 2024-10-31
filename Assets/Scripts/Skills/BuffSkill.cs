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
    //지속시간
    public float duration; //버프 지속시간
    // 스킬 후딜, 후딜이 끝나면 다음 스킬 발동 (스킬이펙트가 겹치는 걸 방지하기 위함 임)
    [field: SerializeField]
    //public Sprite[] skillSprites;
    public AnimationClip skillAnimation;
    
    [field: SerializeField]
    public List<ItemParameter> Parameters { get; set;} //Parameter 사용
    
    public override void ActivateSkill(GameObject user, PlayerInfo playerInfo) 
    {
        Debug.Log($"{SkillName} 발동");
        StatModifyWithSkill(Parameters, playerInfo, 1); // flag 1 버프 적용, flag  -1 버프해제
        // 스킬 이펙트
        // 스텟변조
        // RPC
    }
    public void RemoveSkill(PlayerInfo playerInfo) //GameObject user 파라미터는 추후 필요하면 고려
    {
        Debug.Log($"{SkillName} 효과 해제");
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
    //    //발동효과 및 특수효과 재생
    //    yield return new WaitForSeconds(afterDelay); 

    //}

    // 버프 스킬의 경우, 사용자의 능력치와 특수효과만 부여함.
    // 쿨타임, 지속시간, 올려줄 스텟만 달라서 Sciptable object로 구현이 편리.

}

