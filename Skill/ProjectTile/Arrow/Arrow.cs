using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    [HideInInspector]
    // see if the arrow already hit something
    public bool hited=false;

    [HideInInspector]
    public int qty = 3;

    // survie how long after hit something
    public float hitDestroyTime;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    public override void Cast(Mob mob)
    {
        user = mob;
        shoot(mob.AimDir,mob);
    }

    public override void shoot(Vector3 dir, Mob mob)
    {

        user = mob;
        if (mob == null)
            return;
        else
            tag = user.tag;
        
        Arrow p = Instantiate(this, user.transform.position, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.tag = user.tag;
        p.rig = p.GetComponent<Rigidbody>();
        p.transform.LookAt(p.transform.position + dir);
        p.rig.velocity = dir * this.MoveSpeed;
    }

    public override void Hit(Collider col)
    {
        if (hited == false)
        {
            List<GameObject> ObjList = new List<GameObject>();
            ObjList.Add(col.gameObject);
            foreach (GameObject obj in ObjList)
            {
                //Debug.Log(obj.tag + " ; " + user.tag);
                if (obj.tag!=tag)
                {
                    obj.SendMessage("OnHit", this, SendMessageOptions.DontRequireReceiver);
                    hited = true;
                    rig.velocity = Vector3.zero;
                    //CurrLifeTime = LifeTime - hitDestroyTime;
                    Destroy(this.gameObject);
                }
            }
        }
        //Destroy(this.gameObject);
    }


}
