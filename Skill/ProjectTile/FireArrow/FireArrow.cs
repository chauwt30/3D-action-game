using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : Arrow
{
    public Fire fire;

    private void Start()
    {
        LifeTime = 10f;
        rig = GetComponent<Rigidbody>();
    }

    public override void shoot(Vector3 dir, Mob mob)
    {
        user = mob;
        tag = user.tag;
        Arrow p = Instantiate(this, user.transform.position, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.user = mob;
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
                if (obj.tag != tag)
                {
                    hited = true;
                    fire.shoot(transform.position, user);
                    //CurrLifeTime = LifeTime - hitDestroyTime;
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
