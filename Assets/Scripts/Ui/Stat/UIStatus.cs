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
        playerinfo.OnStatChanged += UpdateStatText; //���ݺ��� Delegate
    }

    private void Start()
    {
        UpdateStatText();
    }

    //���ݿ� ��������� ������ ��� UI�� �ٽ� �׷�����.
    public void UpdateStatText()
    {
        //statTexts[0].text = "�ִ�ü�� : " + playerinfo.stat.MaxHP.ToString();
        //statTexts[1].text = "�������ݷ� : " + playerinfo.stat.PhyAttack.ToString();
        //statTexts[2].text = "�������ݷ� : " + playerinfo.stat.MagAttack.ToString();
        //statTexts[3].text = "�������� : " + playerinfo.stat.PhyDeffense.ToString();
        //statTexts[4].text = "�������� : " + playerinfo.stat.MagDeffense.ToString();
        //statTexts[5].text = "�̵��ӵ� : " + playerinfo.stat.Speed.ToString();
        //statTexts[6].text = "ġ��ŸȮ�� : " + playerinfo.stat.CriticalRate.ToString();
        //statTexts[7].text = "ġ��Ÿ������ : " + playerinfo.stat.CriticalDamage.ToString();
        //statTexts[8].text = "��ų���� : " + playerinfo.stat.SkillRange.ToString();
        //statTexts[9].text = "��Ÿ�Ӱ��� : " + playerinfo.stat.SkillCoolDown.ToString();
        //statTexts[10].text = "ȸ���� : " + playerinfo.stat.HealRate.ToString();
        //statTexts[11].text = "ü����� : " + playerinfo.stat.HpDrainRate.ToString();

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
