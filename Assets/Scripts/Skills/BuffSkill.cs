using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuffSkill : Skill
{
    private PlayerInfo playerInfo;
    [field: SerializeField]
    //지속시간
    public float duration;
    // 스킬 후딜, 후딜이 끝나면 다음 스킬 발동 (스킬이펙트가 겹치는 걸 방지하기 위함 임)
    [field: SerializeField]
    //public Sprite[] skillSprites;
    public AnimationClip skillAnimation;
    
    public override void ActivateSkill(GameObject user) 
    {
        Debug.Log($"{SkillName} 발동");
    }

    //private IEnumerator SkillDelay(GameObject user)
    //{
    //    //발동효과 및 특수효과 재생
    //    yield return new WaitForSeconds(afterDelay); 
        
    //}

    // 버프 스킬의 경우, 사용자의 능력치와 특수효과만 부여함.
    // 쿨타임, 지속시간, 올려줄 스텟만 달라서 Sciptable object로 구현이 편리.

}

