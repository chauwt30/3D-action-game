//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StateGeneri
//{
//    public StateGeneric CurrState; // Tail 
//    public StateGeneric UpperNode; // Connected Head Node 
//                               // List<State> TailList;
//                               //  List<State> Sibling;
//                               // Head 
//    bool IsChangeState()
//    {
//        return true;
//    }
//    virtual public void CheckCondition(Mob mob)
//    {
//        // Change Upper Node Curr State 
//    }
//    virtual public void OnEnter<T>(T mob)
//    {

//    }
//    virtual public void OnUpdate(Mob mob)
//    {
//    }
//    virtual public void OnExit(Mob mob)
//    {
//    }
//    public void ChangeState(StateGeneric UpperNode, StateGeneric NextState, Mob mob)
//    {
//        UpperNode.CurrState.OnExit(mob);
//        UpperNode.CurrState = NextState;
//        UpperNode.CurrState.UpperNode = UpperNode;
//        UpperNode.OnEnter(mob);
//    }
//    public void ChangeState(StateGeneric nextstate, Mob mob)
//    {
//        UpperNode.CurrState.OnExit(mob);
//        UpperNode.CurrState = nextstate;
//        UpperNode.CurrState.UpperNode = UpperNode;
//        UpperNode.CurrState.OnEnter(mob);
//    }
//    public void SetSubState(StateGeneric substate)
//    {
//        CurrState = substate;
//        substate.UpperNode = this;
//    }
//}

