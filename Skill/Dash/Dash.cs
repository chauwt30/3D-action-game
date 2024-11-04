using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    public AnimationCurve analogIntensityCurve;
    public ParticleSystem DashEffect;

    // Skill Param
    public float Duration = 0.2f;
    public float Dististance = 2f;
    float speed;
    private void Start()
    {
    }

    public override void Use(Mob mob)
    {
        if (mob.WalkDir== Vector3.zero) { return; }
        DashEffect.Play();
        speed = Dististance / Duration;
        mob.GetComponent<Rigidbody2D>().velocity =speed*mob.WalkDir;
        user.MoveLock = true;
        StartCoroutine( MyLib.DelayCall(() =>
        {
            mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            mob.MoveLock = false;
        }, Duration));
    }
}
