using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRanger : Skull
{
    public StateGeneric<Skull> CurrState;

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

        CurrState = new SkullRangerState.IdelState();
    }

    public override void OnHit(Skill skill)
    {
        if (Invisible == false)
        {
            HP -= skill.Dmg;

            if (HitFx != null)
                HitFx.Play();

            if (HP <= 0)
                Die();
            else
            {
                CurrState.CurrState.ChangeState(new SkullRangerState.TakingDamage(), this);
            }
        }
        //HPBar.UpdateHealthBar(this);
    }

    public override void Die()
    {
        if (DieFx != null)
            DieFx.Play();
        Invisible = true;
        GlobalMng.ins.mobNum--;
        GlobalMng.ins.player.gold += 5;

        if (spawner != null)
            spawner.mobNum--;

        CurrState.CurrState.ChangeState(new SkullRangerState.Dying(), this);
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
