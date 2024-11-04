using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkullEliteState : MonoBehaviour
{
    public class IdelState : StateGeneric<SkullElite>
    {
        public IdelState()
        {
            SetSubState(new birth());
        }
        public override void OnUpdate(SkullElite mob)
        {
            CurrState.OnUpdate(mob);
        }
    }

    public class birth : StateGeneric<SkullElite>
    {
        public override void OnEnter(SkullElite mob)
        {
            mob.anim.Play("switch");
        }

        public override void OnUpdate(SkullElite mob)
        {
            CheckCondition(mob);
            mob.anim.Play("switch");
        }

        public override void CheckCondition(SkullElite mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeState(new Idle(), mob);
        }
    }


    public class Idle : StateGeneric<SkullElite>
    {
        public override void OnEnter(SkullElite mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Idle");
        }

        public override void OnUpdate(SkullElite mob)
        {
            CheckCondition(mob);
            mob.anim.Play("idle");
        }

        public override void CheckCondition(SkullElite mob)
        {
            if (mob.IsEnemyAround(20f))
                ChangeState(new Chasing(), mob);

            if(mob.HP/mob.MaxHP <= 0.7f && mob.spawn1!=true)
                ChangeState(new Spawning(), mob);

            if (mob.HP / mob.MaxHP <= 0.3f && mob.spawn2 != true)
                ChangeState(new Spawning(), mob);
        }
    }

    public class Chasing : StateGeneric<SkullElite>
    {
        public override void OnEnter(SkullElite mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Chasing");
            mob.anim.Play("walk");
        }
        public override void OnExit(SkullElite mob)
        {

        }

        public override void OnUpdate(SkullElite mob)
        {
            CheckCondition(mob);
            mob.anim.Play("walk");
            mob.UpdateAim();
            mob.MovePos(mob.AimDir);
            mob.FaceTo(mob.AimPos);
        }

        public override void CheckCondition(SkullElite mob)
        {
            if (mob.IsEnemyAround(4f))
                ChangeState(new Attacking(), mob);
            if (!mob.IsEnemyAround(20f))
                ChangeState(new Idle(), mob);
        }
    }

    public class Attacking : StateGeneric<SkullElite>
    {
        bool attackLock = false;
        int rand;
        public override void OnEnter(SkullElite mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Attacking");

            rand = (int)Random.Range(0, 2f);
            attackLock = false;

            if (mob.SkillList[1].IsUsable() && Time.time - mob.birthTime>3f)
            {
                rand = 2;
                mob.anim.Play("attack2");
            }
            else
            {
                if (rand == 0)
                    mob.anim.Play("attack1");
                if (rand == 1)
                    mob.anim.Play("attack3");
            }
        }

        public override void OnUpdate(SkullElite mob)
        {
            CheckCondition(mob);

            if (rand == 0)
                mob.anim.Play("attack1");
            if (rand == 1)
                mob.anim.Play("attack3");
            if (rand == 2)
                mob.anim.Play("attack2");
        }
        
        public override void CheckCondition(SkullElite mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && !attackLock && rand!=2)
            {
                mob.SkillList[0].Use(mob);
                attackLock = true;
            }
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && !attackLock && rand == 2)
            {
                mob.SkillList[1].Use(mob);
                attackLock = true;
            }

            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeState(new Idle(), mob);
        }
    }

    public class Spawning: StateGeneric<SkullElite>
    {
        public override void OnEnter(SkullElite mob)
        {
            GlobalMng.ins.setDebugText("Boss state", "Spawning");
            mob.anim.Play("switch");

            if (mob.spawn1 == false)
                mob.spawn1 = true;
            else
                mob.spawn2 = true;

        }
        public override void OnExit(SkullElite mob)
        {
            mob.skullSpawner.Spawn();
            mob.skullSpawner.Spawn();
            mob.skullRangerSpawner.Spawn();
            mob.skullRangerSpawner.Spawn();
        }

        public override void OnUpdate(SkullElite mob)
        {
            CheckCondition(mob);
            mob.anim.Play("switch");
        }

        public override void CheckCondition(SkullElite mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeState(new Idle(), mob);
        }
    }


    public class TakingDamage : StateGeneric<SkullElite>
    {
        public override void OnEnter(SkullElite mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "TakingDamage");
            mob.anim.Play("takeDamage");
        }

        public override void OnUpdate(SkullElite mob)
        {
            CheckCondition(mob);
        }

        public override void CheckCondition(SkullElite mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeState(new Idle(), mob);
        }
    }

    public class Dying : StateGeneric<SkullElite>
    {
        float dieTime = 0f;
        public override void OnEnter(SkullElite mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Dying");
            mob.anim.Play("dead");
            dieTime = Time.time;
        }

        public override void OnUpdate(SkullElite mob)
        {
            //mob.anim.Play("dead");
            CheckCondition(mob);
        }
        public override void CheckCondition(SkullElite mob)
        {
            if (Time.time - dieTime > 0.5f)
                if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    SceneManager.LoadScene("ending");
                    //Destroy(mob.gameObject);                 
                }                 
        }
    }

}
