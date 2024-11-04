using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mob : MonoBehaviour
{
    // Component
    public Rigidbody rig;

    // Mob model 
    [HideInInspector]
    public Animator anim;

    // Attribute
    [Header("Mob attribute")]
    public string MobName;
    public float MaxHP;
    public float MoveSpeed;

    [Space]
    [Header("3D Model for Mob")]
    public Model mobModel;

    [Space]
    [Header("Effect")]
    public ParticleSystem HitFx;
    public ParticleSystem DieFx;

    // Skill List
    [Space]
    [Header("Skills ")]
    public List<Skill> SkillList;
    //public List<Item> ItemList;

    // UI 
    [Space]
    [Header("UI")]
    public HealthBar HPBar;


    [HideInInspector]
    public float HP;  // Current HP
    [HideInInspector]
    public float MoveSpeedFactor=1f; 

   
    // Input 
    [HideInInspector]
    public Vector3 AimDir;
    [HideInInspector]
    public float AimAngle;
    [HideInInspector]
    public Vector3 AimPos;
    [HideInInspector]
    public Vector3 WalkDir;

    public virtual void UpdateAim() { }

    // Action Lock
    [HideInInspector]
    public bool MoveLock;
    [HideInInspector]
    public bool SkillLock;
    // Mob State 
    [HideInInspector] public bool Walking;
    [HideInInspector] public bool UsingSkill;
    [HideInInspector] public bool Talking;
    [HideInInspector] public bool HitStuning;
    [HideInInspector] public bool Invisible = false;

    // Event Handler
    virtual public void OnHit(Skill mob) {  }

    // For AI informations
    public Mob CurrTarget;
    virtual public bool IsEnemyAround() { return true; }
    virtual public bool HasTarget() { return true; }


    // Start and loop
    public virtual void Awake()
    {
        for (int i = 0; i <= SkillList.Count - 1; i++)
        {
            SkillList[i] = Instantiate(SkillList[i], transform.position, Quaternion.identity, transform);
        }
        InitData();
    }

    public virtual void Update()
    {
        DataUpdate();
        ExtraUpdate();
    }

    
    public virtual void InitData()  { }
    public virtual void DataUpdate() { }
    public virtual void ExtraUpdate() { }


    // Behavior ( Move , Use Skill, die)
    public void PrintName()
    {
        Debug.Log("My name is : " + MobName );
    }
    public virtual void UseSkill(int index)
    {
        SkillList[index].Use(this);
    }
    public virtual void Die()
    {

    }


    // Movement ( move position , rotate ) 
    public virtual void MovePos(Vector3 Dir) {
        rig.MovePosition(rig.position + new Vector3(Dir.x, 0, Dir.z) * MoveSpeed * MoveSpeedFactor * Time.deltaTime);
        //transform.LookAt(transform.position + new Vector3(Dir.x, 0, Dir.y));
        //transform.LookAt(Vector3.Lerp(transform.position, transform.position + new Vector3(Dir.x, 0, Dir.y), Time.deltaTime));

        // Rotate character 
        if (Dir != Vector3.zero)
        {
            //FaceTo(Dir, 30);
            //FaceTo(Dir);
        }
    }

    public virtual void FaceTo(Vector3 Pos)
    {
        //Vector3 Dir = Pos;
        //Dir.Normalize();
        ////transform.LookAt(transform.position + new Vector3(Dir.x, 0, Dir.y));
        //Quaternion toRotation = Quaternion.LookRotation(new Vector3(Dir.x, 0, Dir.y));
        //transform.rotation = toRotation;
        transform.LookAt(new Vector3(Pos.x, transform.position.y, Pos.z));
    }

    public virtual void FaceTo(Vector3 Pos, float rotationSpeed)
    {
        Vector3 Dir = Pos;
        Dir.Normalize();
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(Dir.x, 0, Dir.y));
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        GlobalMng.ins.setDebugText("Look Rotation", ""+toRotation);
    }
}
