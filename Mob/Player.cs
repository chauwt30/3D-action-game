using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob
{
    public StateGeneric<Player> CurrState;

    public List<Arrow> arrows;
    public ParticleSystem healFx;

    [HideInInspector]
    public Arrow currArrow;
    public int gold = 0;
    public int potionNum = 0;

    public int shatterNum = 0;

    // Start is called before the first frame update
    public override void InitData()
    {
        //MobName = "Player76";
        //MaxHP = 2000;
        //MoveSpeed = 4f;

        HP = MaxHP;
        rig = GetComponent<Rigidbody>();
        anim = mobModel.GetComponent<Animator>();
        CurrState = new PlayerState.IdelState();
        currArrow = arrows[0];

        arrows[0].qty = 9999;
        arrows[1].qty = 0;
        arrows[2].qty = 0;
        arrows[3].qty = 0;

        Debug.Log(arrows[0].qty);
    }

    // Update is called once per frame
    public override void DataUpdate()
    {
        base.DataUpdate();
        UpdateAim();
    }

    public override void ExtraUpdate()
    {
        //Change arrow
        changeArrow();
        if (GlobalMng.ins.worldState != GlobalMng.WorldState.Menu)
        {
            anim.enabled = true;
            CurrState.OnUpdate(this);
        }
        else
            anim.enabled = false;
        //PlayerInput();
    }

    public void PlayerInput()
    {
        MoveInput();
        SkillInput();
    }

    // Arrow select 
    public void changeArrow()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            currArrow = arrows[0];
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            currArrow = arrows[1];
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            currArrow = arrows[2];
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            currArrow = arrows[3];
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(potionNum>0)
            {
                if (HP + 20 > MaxHP)
                    HP = MaxHP;
                 else
                   HP += 20;
                potionNum--;
                healFx.Play();
            }
            else
            {
                GlobalMng.ins.showErrorMsg("You don't have any bandage");
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            MaxHP = 10000f;
            HP = 10000f;
            arrows[0].qty = 9999;
            arrows[1].qty = 99;
            arrows[2].qty = 99;
            arrows[3].qty = 99;
            shatterNum = 4;
            Invisible = true;
        }
    }

    // Event Handle
    public override void OnHit(Skill skill)
    {
        if (Invisible == false)
        {
            HP -= skill.Dmg;
            HitFx.Play();
            //MyLib.playParticle(HitFx, transform.position - new Vector3(0,-1f,0));
            CurrState.CurrState.ChangeState(new PlayerState.TakingDamage(), this);
            //HPBar.UpdateHealthBar(this);
            if (HP <= 0)
                Die();
        }
    }

    public override void Die()
    {
        Invisible = true;
        CurrState.CurrState.ChangeState(new PlayerState.Dying(),this);
    }

    // Input Handle
    public bool MoveInput()
    {
        WalkDir = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            WalkDir += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            WalkDir += (new Vector3(1, 0, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            WalkDir += (new Vector3(0, 1, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            WalkDir += (new Vector3(0, -1, 0));
        }

        MovePos(WalkDir);

        if (WalkDir * MoveSpeedFactor == Vector3.zero)
            return false;
        else
            return true;
    }

    public override void MovePos(Vector3 Dir)
    {
   
        //transform.LookAt(transform.position + new Vector3(Dir.x, 0, Dir.y));
        //transform.LookAt(Vector3.Lerp(transform.position, transform.position + new Vector3(Dir.x, 0, Dir.y), Time.deltaTime));

        // Rotate character 
        if (Dir != Vector3.zero)
        {
            if (GlobalMng.ins.currCam.type == CameraController.CameraType.TopView)
            {
                //Quaternion toRotation = Quaternion.LookRotation(new Vector3(Dir.x, 0, Dir.y));
                //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 30 * Time.deltaTime);
                FaceTo(Dir,10f);
                rig.MovePosition(rig.position + new Vector3(Dir.x, 0, Dir.y) * MoveSpeed * MoveSpeedFactor * Time.deltaTime);
            }

            if (GlobalMng.ins.currCam.type == CameraController.CameraType.ThridPerson)
            {
                Vector3 y = GlobalMng.ins.currCam.transform.forward * Dir.y;
                Vector3 x = GlobalMng.ins.currCam.transform.right * Dir.x;

                Dir = y + x;
                Dir.y = 0;
                Dir.Normalize();
                GlobalMng.ins.setDebugText("Cam face", "" + y);
                rig.MovePosition(rig.position + Dir * MoveSpeed * MoveSpeedFactor * Time.deltaTime);
                FaceTo(transform.position + Dir);


                //GlobalMng.ins.setDebugText("Real vector3", "" + v);
                //Quaternion toRotation = Quaternion.LookRotation(new Vector3(v.x, 0,v.z));
                //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 30 * Time.deltaTime);


                //rig.MovePosition(rig.position + new Vector3(Dir.x + forward.x, 0, Dir.y + forward.y) * MoveSpeed * MoveSpeedFactor * Time.deltaTime);
                //Quaternion toRotation = Quaternion.LookRotation(new Vector3(Dir.x+ forward.x, 0, Dir.y+ forward.y));
                //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 30 * Time.deltaTime);
            }

        }
    }

    public void SkillInput()
    {
        if (Input.GetMouseButton(0) && SkillList.Capacity>=1)
        {
            SkillList[0].Use(this);
           // anim.Play("bow_pull");
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)&& SkillList.Capacity >= 2)
        {
            SkillList[1].Use(this);
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(AimPos, Vector2.zero);
            if( hit.transform!=null)
            {
                Mob m = hit.transform.gameObject.GetComponent<Mob>();
                if( m!=null)
                {
                    GlobalMng.ins.SelectMob(m);
                }
            }
        }
    }

    // Action
    
    public override void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从相机向鼠标点击点发射条射线
        RaycastHit hit;//记录碰撞信息
        bool isCollider = Physics.Raycast(ray, out hit);//判断是否发生碰撞

        // Aim for top view 
        if (GlobalMng.ins.currCam.type == CameraController.CameraType.TopView)
        {
            AimPos = hit.point;
            AimPos.y = 0;    
        }

        // Aim for third person camera 
        if (GlobalMng.ins.currCam.type == CameraController.CameraType.ThridPerson)
        {
            if (isCollider)
            {
                GlobalMng.ins.setDebugText("Aiming", "Hit");
                AimPos = hit.point;
            }
            else
            {
                GlobalMng.ins.setDebugText("Aiming", "Miss");
                AimPos = Camera.main.transform.position + 20*Camera.main.transform.forward;
            }
        }
        
        //if (isCollider && hit.collider.tag == "Ground")
        //{
        //    AimPos = hit.point;
        //}
        //AimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + (new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z)));
        AimDir =  AimPos - transform.position;
        AimDir.Normalize();

        GlobalMng.ins.setDebugText("Aim Pos", "" + AimPos);
        //AimDir.y = 0f;
        AimAngle = MyLib.Arctan(AimDir)-90;

    }
}
