using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkullElite : Enemy
{
    private StateGeneric<SkullElite> CurrState;

    [HideInInspector]
    public bool spawn1 = false;
    [HideInInspector]
    public bool spawn2 = false;

    [Header("Spawning")]
    public ParticleSystem spawnFx;
    public MobSpawner skullSpawner;
    public MobSpawner skullRangerSpawner;

    [HideInInspector]
    public float birthTime;

    public override void InitData()
    {
        // Init data
        base.InitData();

        //MobName = "Enemy";
        //MaxHP = 41;
        //MoveSpeed = 4f;

        HP = MaxHP;
        CurrTarget = player;
        rig = GetComponent<Rigidbody>();
        anim = mobModel.GetComponent<Animator>();
        CurrState = new SkullEliteState.IdelState();
        birthTime = Time.time;
        SkillList[0].radius = 2.5f;
    }

    public override void OnHit(Skill skill)
    {
        if (Invisible == false)
        {
            HP -= skill.Dmg;

            if (HitFx != null)
            {
                HitFx.transform.position = skill.transform.position;
                HitFx.Play();
            }
            if (HP <= 0)
                Die();
            else
            {
                //CurrState.CurrState.ChangeState(new SkullEliteState.TakingDamage(), this);
            }
        }
    }

    public override void Die()
    {
        if (DieFx != null)
            DieFx.Play();
        Invisible = true;
        GlobalMng.ins.mobNum--;
        GlobalMng.ins.player.gold += 20;
        CurrState.CurrState.ChangeState(new SkullEliteState.Dying(), this);
    }

    public override void ExtraUpdate()
    {

        if (GlobalMng.ins.worldState != GlobalMng.WorldState.Menu)
        {
            anim.enabled = true;
            CurrState.OnUpdate(this);
        }

        else
            anim.enabled = false;
    }
}
