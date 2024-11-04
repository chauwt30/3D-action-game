using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterArrow : Arrow
{
    public Arrow arrow;
    private void Start()
    {
        LifeTime = 2f;
    }
    public override void shoot(Vector3 dir, Mob mob)
    {
        arrow.shoot(dir,mob);
        arrow.shoot(Quaternion.AngleAxis(10, Vector3.up) * dir, mob);
        arrow.shoot(Quaternion.AngleAxis(20, Vector3.up) * dir, mob);
        arrow.shoot(Quaternion.AngleAxis(-10, Vector3.up) * dir, mob);
        arrow.shoot(Quaternion.AngleAxis(-20, Vector3.up) * dir, mob);
    }

}
