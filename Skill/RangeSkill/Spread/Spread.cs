using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : Skill
{
    public Projectile projectTile;
    private void Start()
    {
        Dmg = 20;
        CD = 0.5f;
        SkillName = "Spread";
    }
    public override void Cast(Mob mob)
    {
        projectTile.shoot(user.AimDir, user);
        //StartCoroutine(MyLib.DelayCallRepeat( GenBullet,0.2f,3, () => { mob.UsingSkill = false; }));
    }
    public void GenBullet()
    {
        Debug.Log("asds");
        projectTile.shoot(user.AimDir, user);
    }
}
