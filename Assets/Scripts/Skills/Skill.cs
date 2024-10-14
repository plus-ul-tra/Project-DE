using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{

    [field: SerializeField]
    public string SkillName { get; set; }
    [field: SerializeField]
    public bool hasCooltime { get; set; } //쿨타임 여부
    [field: SerializeField]
    public float coolTime { get; set; }
    [field: SerializeField]
    public float afterDelay;

    public abstract void ActivateSkill(GameObject user);
}
