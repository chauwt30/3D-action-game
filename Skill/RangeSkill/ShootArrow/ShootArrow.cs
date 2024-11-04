using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : Skill
{
    public Arrow arrow;
    public float chargeStartTime=0f;

    private void Start()
    {
        CD = 0.5f;
        SkillName = "ChargeArrow";
    }

    public override void Cast(Mob mob)
    {
        chargeStartTime = Time.time;
        //mob.MoveSpeedFactor -= 0.5f;
        //StartCoroutine(MyLib.DelayCallRepeat( GenBullet,0.2f,3, () => { mob.UsingSkill = false; }));
    }

    public override void Release(Mob mob)
    {
        Player player = (Player)mob;
        if (player.currArrow != null)
                arrow = player.currArrow;

        arrow.shoot(user.AimDir, user);


        //mob.MoveSpeedFactor += 0.5f;
        chargeStartTime = 0f;
    }
    
    public override void GiveUp(Mob mob)
    {
        chargeStartTime = 0f;
    }

    public void GenBullet()
    {
        Debug.Log("asds");
        arrow.shoot(user.AimDir, user);
    }
}
