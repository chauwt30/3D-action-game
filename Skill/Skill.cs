using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Skill : MonoBehaviour
{
    [Header("Attribute")]
    public string SkillName;
    public int Dmg;
    public float CD;
    public float startCD;
    public float radius;

    [Header("Animation")]
    public PlayableDirector anim;

    [HideInInspector]
    public float CDLeft;
    [HideInInspector]
    public float lastUsedTime=0f;
    [HideInInspector]
    public List<string> Label;
    [HideInInspector]
    public Mob user;
    

    virtual public void Use(Mob mob)
    {
        user = mob;
        if (lastUsedTime == 0 | Time.time - lastUsedTime >= CD)
        {
            lastUsedTime = Time.time;
            Cast(mob);
        }
    }

    virtual public void Cast(Mob mob)
    {

    }
    virtual public void Release(Mob mob)
    {

    }
    virtual public void GiveUp(Mob mob)
    {

    }

    virtual public bool IsUsable()
    {
        return (lastUsedTime == 0 | Time.time - lastUsedTime >= CD);
    }

    virtual public void Hit(Collider2D col)
    {
        List<GameObject> ObjList = new List<GameObject>();
        ObjList.Add(col.gameObject);
        foreach (GameObject obj in ObjList)
        {
            if (obj.tag != user.tag)
            {
                obj.SendMessage("OnHit", this, SendMessageOptions.DontRequireReceiver);
            }
        }
    }


    virtual public void Hit(Collider col)
    {
    }

    //private void Update()
    //{
    //    CustomUpdate();
    //    if (CDLeft >= 0)
    //    {
    //        CDLeft -= Time.deltaTime;
    //    }
    //}

    //public virtual void CustomUpdate() { }
}


public class ProjectileController : Skill
{
    public Projectile bullet;
    virtual public void GenBullet()
    {

    }
}