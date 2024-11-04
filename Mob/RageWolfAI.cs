using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mob))]
public class RageWolfAI : Mobbehavior<Enemy>
{
    private void Start()
    {
        SetCurrState(new Idle());
    }
    class Idle : MobState
    {
        public override void OnEnter()
        {
            SetSubState(new Chase());
        }
    }
    class Chase : MobState
    {
        public override void OnUpdate()
        {
            base.OnUpdate();
            mob.MovePos(MyLib.NormalizeDir(mob.transform.position,mob.CurrTarget.transform.position));
        }
        public override void CheckCondition()
        {
            if(Vector3.Distance(mob.transform.position, mob.CurrTarget.transform.position) <=2)
            {
                ChangeState(new Attack());
            }
        }
    }
    class Attack : MobState
    {
        float lasttime;
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Time.time - lasttime > 1)
            {
                lasttime = Time.time;
                Debug.Log("go die! player");
                // Skill DB
                SkillDB.ins.ShootBullet(mob.AimDir,mob);
            }
        }
        public override void CheckCondition()
        {
            base.CheckCondition();
            if (Vector3.Distance(mob.transform.position, mob.CurrTarget.transform.position) > 2)
            {
                ChangeState(new Chase());
            }
        }
    }

}





