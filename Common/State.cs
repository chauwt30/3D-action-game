using UnityEngine;

// When the mob have just born , deside what state it
// should enter 
public class StateGeneric<T>
{
    public StateGeneric<T> CurrState; // Tail 
    public StateGeneric<T> UpperNode; // Connected Head Node 

    // Write all Tail node here 
    virtual public void CheckCondition(T mob) { }

    // Behavior 
    virtual public void OnEnter(T mob) { }
    virtual public void OnUpdate(T mob) { }
    virtual public void OnExit(T mob) { }

    public void ChangeState(StateGeneric<T> nextstate, T mob)
    {
        UpperNode.CurrState.OnExit(mob);
        UpperNode.SetSubState(nextstate);
        UpperNode.CurrState.OnEnter(mob);
    }
    public void SetSubState(StateGeneric<T> substate)
    {
        CurrState = substate;
        CurrState.UpperNode = this;
    }
}


public class MobState
{
    public MobState CurrState; // Tail 
    public MobState UpperNode; // Connected Head Node 
    public Mob mob;

    // Write all Tail node condition here 
    virtual public void CheckCondition() { }

    virtual public void OnEnter() { }
    virtual public void OnUpdate()
    {
        CheckCondition();
        if (CurrState != null) { CurrState.OnUpdate(); }
    }
    virtual public void OnExit() { }

    // Change to the state that on the Same Layer
    public void ChangeState(MobState nextstate)
    {
        UpperNode.CurrState.OnExit();
        UpperNode.SetSubState(nextstate);
        UpperNode.CurrState.OnEnter();
    }
    // Change self state 
    public void SetSubState(MobState substate)
    {
        if (CurrState != null) CurrState.OnExit();
        CurrState = substate;
        CurrState.UpperNode = this;
        CurrState.mob = mob;
        CurrState.OnEnter();
    }
}
public class Mobbehavior<T> : MonoBehaviour where T : Mob
{
    [HideInInspector]
    public T mob;
    [HideInInspector]
    public MobState CurrState;

    private void Awake()
    {
        mob = GetComponent<T>();
        if (mob == null) { Debug.Log("Mob not found, AI will not be execute properly."); }
        mob = mob as T;
    }
    public void SetCurrState(MobState state)
    {
        CurrState = state;
        state.mob = mob;
        state.OnEnter();
    }
    private void Update()
    {
        if (CurrState != null) { CurrState.OnUpdate(); }
    }
}