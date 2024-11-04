using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : StateGeneric<Enemy>
{
    public class IdelState : StateGeneric<Enemy>
    {
        public IdelState()
        {
            SetSubState(new ScoutState());
        }
        public override void OnUpdate(Enemy mob)
        {
            CurrState.OnUpdate(mob);
        }
    }

    public class AttackState : StateGeneric<Enemy>
    {
        public override void OnUpdate(Enemy mob)
        {
            CheckCondition(mob);
            mob.SkillList[0].Use(mob);
        }
        public override void CheckCondition(Enemy mob)
        {
            if (mob.IsEnemyAround() == false) { ChangeState(new ChaseState(), mob); }
        }
    }

    public class ChaseState : StateGeneric<Enemy>
    {
        public override void OnUpdate(Enemy mob)
        {
            CheckCondition(mob);
            if (mob.HasTarget())
            {
                // Move to target 
                mob.MovePos(mob.MoveSpeed * MyLib.NormalizeDir(mob.transform.position, mob.CurrTarget.transform.position));
            }
        }
        public override void CheckCondition(Enemy mob)
        {
            if (mob.IsEnemyAround())
            {
                ChangeState(new AttackState(), mob);
                return;
            }
            if (mob.HasTarget() == false)
            {
                ChangeState(new ScoutState(), mob);
                return;
            }
        }
    }

    public class ScoutState : StateGeneric<Enemy>
    {
        public override void OnUpdate(Enemy mob)
        {
            CheckCondition(mob);
        }
        public override void CheckCondition(Enemy mob)
        {
            if (mob.HasTarget())
            {
                ChangeState(new ChaseState(), mob);
                return;
            }
        }
    }
}