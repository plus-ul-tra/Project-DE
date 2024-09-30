using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public PlayerJob job { get; } // ���� only get
    public string name { get; set;} 
    public float MaxHP {get;set;} // �ִ�ü��
    public float PhyAttack { get;set;} // ���� ����
    public float MagAttack { get;set;} //���� ����
    public float PhyDeffense { get;set;} //���� ���
    public float MagDeffense { get;set;} //���� ���
    public float Speed { get;set;} // �̼�
    public float CriticalRate { get;set;} // ũ��Ƽ�� Ȯ��
    public float CriticalDamage { get;set;} //ũ��Ƽ�� ������ ����
    public float SkillRange { get;set;} //��ų ����(ũ��)
    public float SkillCoolDown { get;set;} //��ų ��
    public float HealRate { get;set;} // ȸ����
    public  float HpDrainRate { get;set;} // ü�����

    
    public Stat(PlayerJob job, string name, float maxHP, float phyAttack, float magAttack, float phyDeffense, float magDeffense, float speed, float criticalRate, float criticalDamage, float skillRange, float skillCoolDown, float healRate, float hpDrainRate)
    {
        this.job = job;
        this.name = name;
        this.MaxHP = maxHP;
        this.PhyAttack = phyAttack;
        this.MagAttack = magAttack;
        this.PhyDeffense = phyDeffense;
        this.MagDeffense = magDeffense;
        this.Speed = speed;
        this.CriticalRate = criticalRate;
        this.CriticalDamage = criticalDamage;
        this.SkillRange = skillRange;
        this.SkillCoolDown = skillCoolDown;
        this.HealRate = healRate;
        this.HpDrainRate = hpDrainRate;
    }
    public Stat()
    {

    }
    public Stat StatInitialize(PlayerJob jobCode)
    {// Job�� ���� Stat �ʱ�ȭ
        Stat stat = null;
        //Stat(PlayerJob job, string name, float maxHP, float phyAttack, float magAttack, float phyDeffense, float magDeffense, float speed, float criticalRate,
        //float criticalDamage, float skillRange, float skillCoolDown, float healRate, float hpDrainRate)
        switch (jobCode)
        {
            case PlayerJob.Knight:
                stat = new Stat(PlayerJob.Knight, "Knight", 400f, 20f, 20f, 50f, 50f, 5.0f, 5f, 5f, 1f, 1f, 1f, 0f);
                break;
            case PlayerJob.Warrior:
                stat = new Stat(PlayerJob.Warrior,"Warrior", 300f, 30f, 5f, 20f, 20f, 6.0f, 10f, 10f, 3f, 1f, 1f, 1f);
                break;
            case PlayerJob.Archer:
                stat = new Stat(PlayerJob.Archer, "Archer", 200f, 50f, 10f, 50f, 50f, 8.0f, 5f, 5f, 1f, 1f, 1f, 0f);
                break;
            case PlayerJob.Magician:
                stat = new Stat(PlayerJob.Magician, "Magician", 200f, 20f, 5f, 50f, 50f, 7.0f, 5f, 5f, 1f, 1f, 1f, 0f);
                break;
            case PlayerJob.Supporter:
                stat = new Stat(PlayerJob.Supporter, "Supporter", 300f, 20f, 5f, 50f, 50f, 6.0f, 5f, 5f, 1f, 1f, 1f, 0f);
                break;

        }
        return stat;
    }
}
