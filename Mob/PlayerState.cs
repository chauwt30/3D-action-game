using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public class IdelState : StateGeneric<Player>
    {
        public IdelState()
        {
            SetSubState(new Idle());
        }
        public override void OnUpdate(Player mob)
        {
            CurrState.OnUpdate(mob);
        }
    }

    public class Idle : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            GlobalMng.ins.setDebugText("Player state", "Idle");
            mob.anim.Play("idle");
        }

        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
        }

        public override void CheckCondition(Player mob)
        {
            if (mob.MoveInput() == true)
                ChangeState(new Walking(), mob);

            if (Input.GetMouseButton(0))
                ChangeState(new Pulling(), mob);
        }
    }

    public class Walking : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            GlobalMng.ins.setDebugText("Player state", "Walking");
            mob.anim.Play("runForward");
        }
        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
        }
        public override void CheckCondition(Player mob)
        {
            if (mob.MoveInput() == false)
                ChangeState(new Idle(), mob);

            if (Input.GetMouseButton(0))
            {
                    ChangeState(new Pulling(), mob);
            }
            if (Input.GetKeyDown(KeyCode.Space))
                ChangeState(new Dodging(), mob);
        }
    }

    public class Dodging : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            GlobalMng.ins.setDebugText("Player state", "Dodging");
            mob.MoveSpeedFactor += 1f;
            mob.anim.Play("dodge");
        }

        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
            mob.MovePos(mob.WalkDir);
        }

        public override void OnExit(Player mob)
        {
            mob.MoveSpeedFactor -= 1f;
        }
        public override void CheckCondition(Player mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                ChangeState(new Idle(), mob);
            }
        }
    }

    public class Pulling : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            if (mob.currArrow.qty > 0)
            {
                mob.currArrow.qty--;
                mob.SkillList[0].Use(mob);
                GlobalMng.ins.setDebugText("Player state", "Pulling");
                mob.anim.Play("bow_pull");
            }
            else
            {
                GlobalMng.ins.showErrorMsg("Not enough arrow!!");
                ChangeState(new IdelState(), mob);
            }

        }
        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
            //mob.FaceTo(new Vector3(mob.AimPos.x, mob.transform.position.y, mob.AimPos.z));
            mob.MoveInput();
            mob.FaceTo(new Vector3(mob.AimPos.x, mob.transform.position.y, mob.AimPos.z));
        }
        public override void CheckCondition(Player mob)
        {
            ChangeState(new Aiming(), mob);
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    ChangeState(new Shooting(), mob);
                }
                ChangeState(new Aiming(), mob);
            }
        }
    }

    public class Aiming : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            GlobalMng.ins.setDebugText("Player state", "Aiming");
            mob.anim.Play("bow_aim");
            mob.MoveSpeedFactor -= 0.5f;
        }
        public override void OnExit(Player mob)
        {
            mob.MoveSpeedFactor += 0.5f;
        }

        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
            mob.FaceTo(new Vector3(mob.AimPos.x, mob.transform.position.y, mob.AimPos.z));
        }

        public override void CheckCondition(Player mob)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ChangeState(new Shooting(), mob);
                //ChangeState(new Idle(), mob);
            }
            if (mob.MoveInput())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    mob.SkillList[0].GiveUp(mob);
                    ChangeState(new Dodging(), mob);
                }

                mob.anim.Play("bow_aimForward");
                //ChangeState(new AimingWalk(), mob);
            }
            else
            {
                mob.anim.Play("bow_aim");
            }

        }
    }

    public class AimingWalk : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            mob.SkillList[0].Use(mob);
            GlobalMng.ins.setDebugText("Player state", "AimingWalk");
            mob.anim.Play("bow_aimForward");
        }
        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
            mob.FaceTo(new Vector3(mob.AimPos.x, mob.transform.position.y, mob.AimPos.z));
        }

        public override void CheckCondition(Player mob)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ChangeState(new Shooting(), mob);
            }
            if (!mob.MoveInput())
            {
                ChangeState(new Aiming(), mob);
            }
        }
    }

    public class Shooting : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            GlobalMng.ins.setDebugText("Player state", "Shooting");
            mob.anim.Play("bow_shot");
            mob.SkillList[0].Release(mob);
        }
        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
            //mob.MoveInput();
            //mob.FaceTo(new Vector3(mob.AimPos.x, mob.transform.position.y, mob.AimPos.z));
        }

        public override void CheckCondition(Player mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                ChangeState(new Idle(), mob);
            }
            // urgent dogde
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f && mob.MoveInput())
                if (Input.GetKeyDown(KeyCode.Space))
                    ChangeState(new Dodging(), mob);
        }
    }

    public class TakingDamage : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            GlobalMng.ins.setDebugText("Player state", "Taking damage");
            mob.anim.Play("takeDamage");
        }

        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
        }

        public override void CheckCondition(Player mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
                ChangeState(new Idle(), mob);
        }
    }

    public class Dying : StateGeneric<Player>
    {
        public override void OnEnter(Player mob)
        {
            GlobalMng.ins.setDebugText("Player state", "Dying");
            mob.anim.Play("dead");
        }
        public override void OnUpdate(Player mob)
        {
            CheckCondition(mob);
        }

        public override void CheckCondition(Player mob)
        {
            if (mob.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                GlobalMng.ins.deathMenu.showDeathMenu();
                //Destroy(mob.gameObject);
            }
        }
    }

    public class UsingSkill : StateGeneric<Player> { }
    public class Talking : StateGeneric<Player> { }
}
