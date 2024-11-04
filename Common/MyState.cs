using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyState 
{
    public MyState CurrState; // Tail 
    public MyState UpperNode; // Connected Head Node 

    virtual public void CheckCondition<T>(T mob)
    {
        // Change Upper Node Curr State 
    }
    virtual public void OnEnter<T>(T mob)
    {
    }
    virtual public void OnUpdate<T>(T mob)
    {
    }
    virtual public void OnExit<T>(T mob)
    {
    }
    


    public void ChangeState<T>(MyState UpperNode, MyState NextState, T mob)
    {
        UpperNode.CurrState.OnExit(mob);
        UpperNode.CurrState = NextState;
        UpperNode.CurrState.UpperNode = UpperNode;
        UpperNode.OnEnter(mob);
    }
    public void ChangeState<T>(MyState nextstate, T mob)
    {
        UpperNode.CurrState.OnExit(mob);
        UpperNode.SetSubState(nextstate);
        UpperNode.CurrState.OnEnter(mob);
    }
    public void SetSubState(MyState substate)
    {
        CurrState = substate;
        CurrState.UpperNode = this;
    }



}
