using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionArrow : Arrow
{

    public ParticleSystem explosionFx;
    public float explosionRadius;

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
        p.tag = user.tag;
        p.rig = p.GetComponent<Rigidbody>();
        p.transform.LookAt(p.transform.position + dir);
        p.rig.velocity = dir * this.MoveSpeed;
    }

    public override void Hit(Collider col)
    {
        Debug.Log("Explosion!!!!!!!!!");
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, explosionRadius, transform.forward, explosionRadius);

        foreach (RaycastHit h in hit)
        {
            GameObject obj = h.transform.gameObject;
            if (tag != obj.tag )
            {
                explosionFx.Play();
                obj.SendMessage("OnHit", this, SendMessageOptions.DontRequireReceiver);
                rig.velocity = Vector3.zero;
                MyLib.delayCall(() => { Destroy(this.gameObject); }, 0.5f);
                //Destroy(this.gameObject);
                //Debug.Log(h.transform.gameObject.name);           
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}
