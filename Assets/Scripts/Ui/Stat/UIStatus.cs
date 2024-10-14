using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatus : MonoBehaviour
{
    public List<TMP_Text> statTexts;
    public PlayerInfo playerinfo;

    private void Awake()
    {
        playerinfo.OnStatChanged += UpdateStatText; //스텟변경 Delegate
    }

    private void Start()
    {
        UpdateStatText();
    }

    //스텟에 변경사항이 생겼을 경우 UI를 다시 그려야함.
    public void UpdateStatText()
    {
        //statTexts[0].text = "최대체력 : " + playerinfo.stat.MaxHP.ToString();
        //statTexts[1].text = "물리공격력 : " + playerinfo.stat.PhyAttack.ToString();
        //statTexts[2].text = "마법공격력 : " + playerinfo.stat.MagAttack.ToString();
        //statTexts[3].text = "물리방어력 : " + playerinfo.stat.PhyDeffense.ToString();
        //statTexts[4].text = "마법방어력 : " + playerinfo.stat.MagDeffense.ToString();
        //statTexts[5].text = "이동속도 : " + playerinfo.stat.Speed.ToString();
        //statTexts[6].text = "치명타확률 : " + playerinfo.stat.CriticalRate.ToString();
        //statTexts[7].text = "치명타데미지 : " + playerinfo.stat.CriticalDamage.ToString();
        //statTexts[8].text = "스킬범위 : " + playerinfo.stat.SkillRange.ToString();
        //statTexts[9].text = "쿨타임감소 : " + playerinfo.stat.SkillCoolDown.ToString();
        //statTexts[10].text = "회복량 : " + playerinfo.stat.HealRate.ToString();
        //statTexts[11].text = "체력흡수 : " + playerinfo.stat.HpDrainRate.ToString();

        statTexts[0].text = "MaxHp : " + playerinfo.stat.MaxHP.ToString();
        statTexts[1].text = "PhyAttack : " + playerinfo.stat.PhyAttack.ToString();
        statTexts[2].text = "MagiAttack : " + playerinfo.stat.MagAttack.ToString();
        statTexts[3].text = "PhyDeffense : " + playerinfo.stat.PhyDeffense.ToString();
        statTexts[4].text = "MagiDeffense : " + playerinfo.stat.MagDeffense.ToString();
        statTexts[5].text = "Speed : " + playerinfo.stat.Speed.ToString();
        statTexts[6].text = "CriticalRate : " + playerinfo.stat.CriticalRate.ToString();
        statTexts[7].text = "CriticalDamage : " + playerinfo.stat.CriticalDamage.ToString();
        statTexts[8].text = "SkillRange : " + playerinfo.stat.SkillRange.ToString();
        statTexts[9].text = "CoolTimeReduce : " + playerinfo.stat.SkillCoolDown.ToString();
        statTexts[10].text = "HealRate : " + playerinfo.stat.HealRate.ToString();
        statTexts[11].text = "HPDraine : " + playerinfo.stat.HpDrainRate.ToString();

    }
    
    
}
