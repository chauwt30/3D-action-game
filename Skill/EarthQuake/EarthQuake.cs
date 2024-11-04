using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : Skill
{
    public Rock hitBox;
    public ParticleSystem fx;
    public override void Cast(Mob mob)
    {
        //fx.Play();
        EarthQuake p = Instantiate(this, user.transform.position, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.tag = user.tag;
        p.transform.LookAt(p.transform.position + new Vector3(mob.AimDir.x,0, mob.AimDir.z));
        p.fx.Play();
        p.hitBox.Cast(mob);     
    }
}
