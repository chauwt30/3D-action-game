using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDB : Singleton<SkillDB>
{
    public Projectile Bullet;
    public void ShootBullet(Vector3 Dir,Mob user)
    {
        Projectile p;
        p = Instantiate(Bullet, user.transform.position, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.tag = user.tag;
        p.Shoot(Dir, user);
    }
}
