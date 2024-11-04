using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Skill
{
    public Rigidbody rig;
    public float MoveSpeed=5f;
    public float LifeTime = 2f;
    [HideInInspector]
    public float CurrLifeTime=0;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        Dmg = 20;
    }

    private void Update()
    {
        CurrLifeTime += Time.deltaTime;
        if( CurrLifeTime >= LifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void shoot(Vector3 dir, Mob mob)
    {
        user = mob;
        Projectile p = Instantiate(this, user.transform.position, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.tag = user.tag;
        p.Start();
        //p.rig = p.GetComponent<Rigidbody>();
        p.rig.velocity = dir * MoveSpeed;
    }

    public void Shoot(Vector3 Dir,Mob mob)
    {
        user = mob;
        rig.velocity = Dir*MoveSpeed;
    }

    public override void Hit(Collider col)
    {
        List<GameObject> ObjList = new List<GameObject>();
        ObjList.Add(col.gameObject);
        foreach (GameObject obj in ObjList)
        {
            if (obj.tag != user.tag)
            {
                obj.SendMessage("OnHit", this, SendMessageOptions.DontRequireReceiver);
                Destroy(this.gameObject);
            }
        }
        //Destroy(this.gameObject);
    }
}
