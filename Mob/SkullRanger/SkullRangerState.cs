using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRangerState : MonoBehaviour
{
    public class IdelState : StateGeneric<Skull>
    {
        public IdelState()
        {
            SetSubState(new Idle());
        }
        public override void OnUpdate(Skull mob)
        {
            CurrState.OnUpdate(mob);
        }
    }

    public class Idle : StateGeneric<Skull>
    {
        public override void OnEnter(Skull mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Idle");
            mob.anim.Play("idle");
        }

        public override void OnUpdate(Skull mob)
        {
            CheckCondition(mob);
            mob.anim.Play("idle");
        }
        public override void CheckCondition(Skull mob)
        {
            if (mob.IsEnemyAround(20f))
                ChangeState(new Chasing(), mob);
        }
    }

    public class Chasing : StateGeneric<Skull>
    {
        public override void OnEnter(Skull mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Chasing");
            mob.anim.Play("walk");
            //mob.anim.Play("charge");
        }
        public override void OnExit(Skull mob)
        {

        }
        public override void OnUpdate(Skull mob)
        {
            CheckCondition(mob);
            mob.UpdateAim();
            mob.MovePos(mob.AimDir);
            mob.FaceTo(mob.AimPos);
        }

        public override void CheckCondition(Skull mob)
        {
            if (mob.IsEnemyAround(10f))
                ChangeState(new Attacking(), mob);
            if (!mob.IsEnemyAround(20f))
                ChangeState(new Idle(), mob);
        }
    }

    public class Attacking : StateGeneric<Skull>
    {
        bool attackLock = false;
        public override void OnEnter(Skull mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Attacking");
            mob.anim.Play("attack1");
            //mob.disableUpdateAim = true;
        }

        public override void OnExit(Skull mob)
        {
            //mob.disableUpdateAim = false;
        }
        public override void OnUpdate(Skull mob)
        {
            CheckCondition(mob);
        }

        public override void CheckCondition(Skull mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.35f && !attackLock)
            {
                mob.SkillList[0].Use(mob);
                attackLock = true;
            }
            
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeState(new Idle(), mob);
        }
    }

    public class TakingDamage : StateGeneric<Skull>
    {
        public override void OnEnter(Skull mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "TakingDamage");
            mob.anim.Play("takeDamage");
        }

        public override void OnUpdate(Skull mob)
        {
            CheckCondition(mob);
        }

        public override void CheckCondition(Skull mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeState(new Idle(), mob);
        }
    }

    public class Dying : StateGeneric<Skull>
    {
        float dieTime = 0f;
        public override void OnEnter(Skull mob)
        {
            GlobalMng.ins.setDebugText("Enemy state", "Dying");
            mob.anim.Play("dead");
            dieTime = Time.time;
        }

        public override void OnUpdate(Skull mob)
        {
            //mob.anim.Play("dead");
            CheckCondition(mob);
        }

        public override void CheckCondition(Skull mob)
        {
            if (Time.time - dieTime > 0.5f)
                if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                    Destroy(mob.gameObject);
        }
    }
}
