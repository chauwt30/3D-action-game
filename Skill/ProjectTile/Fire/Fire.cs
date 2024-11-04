using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Projectile
{
    // survie how long after hit something
    public float hitDestroyTime;
    public float burnGapTime;
    float lastBurnTime;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Time.time - lastBurnTime > burnGapTime)
        {
            burn();
            lastBurnTime = Time.time;
        }

        CurrLifeTime += Time.deltaTime;
        if (CurrLifeTime >= LifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    public override void shoot(Vector3 dir, Mob mob)
    {
        user = mob;
        tag = user.tag;
        Fire p = Instantiate(this, dir, Quaternion.identity);
        p.gameObject.SetActive(true);
        p.tag = user.tag;
        p.rig = p.GetComponent<Rigidbody>();
    }
    
    public void burn()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, radius, transform.forward, radius);
        foreach (RaycastHit h in hit)
        {
            GameObject obj = h.transform.gameObject;
            if (tag != obj.tag)
            {
                obj.SendMessage("OnHit", this, SendMessageOptions.DontRequireReceiver);     
            }
        }
    }

    public override void Hit(Collider col)
    {
        //Destroy(this.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }


}