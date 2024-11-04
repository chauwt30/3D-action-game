using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPCMob
{
    public Mob player;

    // if any 
    public MobSpawner spawner; 
    
    public bool disableUpdateAim = false;
    //private StateGeneric<Enemy> CurrState;
    public override void InitData()
    {
        // Init data
        base.InitData();
            MobName = "Enemy";
            HP = MaxHP;
            player = GlobalMng.ins.player;
            CurrTarget = player;
            //CurrState = new EnemyState.IdelState();
    }

    public override void DataUpdate()
    {
        base.DataUpdate();
        if(!disableUpdateAim)
            UpdateAim();
    }

    // Event Handle
    public override void OnHit(Skill skill)
    {
        Debug.Log("I am hit");
        HP -= skill.Dmg;
        if (HP <= 0)
            Destroy(this.gameObject);
        //HPBar.UpdateHealthBar(this);
    }

    // Update Data
    public override void UpdateAim()
    {
        AimPos = player.transform.position;
        AimDir = MyLib.NormalizeDir(transform.position,player.transform.position);
        AimDir = (player.transform.position - transform.position);        
        AimDir.Normalize();
        GlobalMng.ins.setDebugText("Enemy Aim" , "" + AimDir);
        AimAngle = MyLib.Arctan(AimDir)-90;
    }

    public void UpdateAim(Vector3 AimPos)
    {
        AimDir = MyLib.NormalizeDir(transform.position, player.transform.position);
        AimDir = (player.transform.position - transform.position);
        AimDir.Normalize();
        GlobalMng.ins.setDebugText("Enemy Aim", "" + AimDir);
        AimAngle = MyLib.Arctan(AimDir) - 90;
    }

    // for AI recognition
    public override bool IsEnemyAround()
    {
        return (Vector3.Distance(player.transform.position, transform.position) <= 5f);
    }

    public  bool IsEnemyAround(float distance)
    {
        return (Vector3.Distance(player.transform.position, transform.position) <= distance);
    }

    public override bool HasTarget()
    {
        return true;
    }
}
