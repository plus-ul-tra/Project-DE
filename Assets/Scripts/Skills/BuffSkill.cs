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
    [field: SerializeField]
    //public Sprite[] skillSprites;
    public AnimationClip skillAnimation;
    
    public override void ActivateSkill(GameObject user) 
    {
        Animator animator = user.GetComponent<Animator>();
        if (animator != null && skillAnimation != null)
        {
            animator.Play(skillAnimation.name);
            
        }
    }

}

