using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullOrb : Mob
{
    private StateGeneric<Skull> CurrState;

    public override void InitData()
    {
        // Init data
        base.InitData();
        Invisible = false;
        //MobName = "Enemy";
        //MaxHP = 41;
        //MoveSpeed = 4f;

        HP = MaxHP;
        rig = GetComponent<Rigidbody>();
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
        }
        //HPBar.UpdateHealthBar(this);
    }

    public override void Die()
    {
        //if (DieFx != null)
        //    DieFx.Play();
        Invisible = true;
        GlobalMng.ins.player.gold += 50;
        GlobalMng.ins.player.shatterNum++;

        //HitFx.transform.position = transform.position;
        DieFx.Play();

        MyLib.delayCall(() => { Destroy(this.gameObject);},2f);
    }

    public override void ExtraUpdate()
    {
        //if (GlobalMng.ins.worldState != GlobalMng.WorldState.Menu)
        //{
        //    anim.enabled = true;
        //}
        //else
        //    anim.enabled = false;
    }
}
