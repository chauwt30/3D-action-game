using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Skill
{
    // Start is called before the first frame update
    void Start()
    {
        CD = 0.5f;
        Dmg = 30;
    }

    public override void Cast(Mob mob)
    {
        RaycastHit[] hit = Physics.SphereCastAll(mob.transform.position + transform.forward * radius, radius, mob.transform.forward, radius);

        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.transform.position = mob.transform.position + transform.forward * radius;
        //sphere.transform.localScale = new Vector3(radius, radius, radius);

        //MyLib.delayCall(() => { Destroy(sphere.gameObject); }, 0.5f);

        foreach (RaycastHit h in hit)
        {
            GameObject obj = h.transform.gameObject;
            if (mob.tag != obj.tag)
                obj.SendMessage("OnHit", this, SendMessageOptions.DontRequireReceiver); 
            //Debug.Log(h.transform.gameObject.name);           
        }

    }
}
