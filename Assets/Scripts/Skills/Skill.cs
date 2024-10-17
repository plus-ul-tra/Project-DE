using Inventory.Model;
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

    public abstract void ActivateSkill(GameObject user,PlayerInfo playerinfo);
}
public interface IStatHandler {
    public List<ItemParameter> Parameters { get; set; }
    void StatModifyWithSkill(List<ItemParameter> Parameters, PlayerInfo playerinfo,int flag);

} 


public interface IResourceEffect
{
    //아트 연출 구현
}


