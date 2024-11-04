using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Projectile
{

    // survie how long after hit something
    public float hitDestroyTime;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    public override void Cast(Mob mob)
    {
        if (mob == null)
            return;
        user = mob;
        Rock p = Instantiate(this, user.transform.position, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.tag = user.tag;
        p.rig = p.GetComponent<Rigidbody>();
        p.transform.LookAt(p.transform.position + mob.AimDir);
        p.rig.velocity = mob.AimDir * this.MoveSpeed;
    }

    public void Cast(Vector3 aimDir, Mob mob)
    {
        if (mob == null)
            return;
        user = mob;
        Rock p = Instantiate(this, user.transform.position, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.tag = user.tag;
        p.rig = p.GetComponent<Rigidbody>();
        p.transform.LookAt(p.transform.position);
        p.rig.velocity = aimDir * this.MoveSpeed;
    }

    public override void Hit(Collider col)
    {
        List<GameObject> ObjList = new List<GameObject>();
        ObjList.Add(col.gameObject);
        foreach (GameObject obj in ObjList)
        {
            //Debug.Log(obj.tag + " ; " + user.tag);
            if (obj.tag != tag)
            {
                obj.SendMessage("OnHit", this, SendMessageOptions.DontRequireReceiver);
                //CurrLifeTime = LifeTime - hitDestroyTime;
                Destroy(this.gameObject);
            }
        }
        //Destroy(this.gameObject);
    }

}
