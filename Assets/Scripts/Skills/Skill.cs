using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{

    [field: SerializeField]
    public string SkillName { get; set; }
    [field: SerializeField]
    public bool hasCooltime { get; set; } // ��Ÿ�� ����
    [field: SerializeField]
    public float coolTime { get; set; }

    public abstract bool ActivateSkill(GameObject user);
}
