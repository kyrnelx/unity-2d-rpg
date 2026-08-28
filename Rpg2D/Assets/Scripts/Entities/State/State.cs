using UnityEngine;

public class State
{
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Entity entity;
    protected bool isReturn;
    protected float timer;
    public bool triggered;

    public State(StateMachine stateMachine, string animBoolName, Entity entity)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.entity = entity;
    }

    public virtual void Enter()
    {
        triggered = false;
        entity.animator.SetBool(animBoolName, true);
        timer = 0;
    }

    public virtual void Update()
    {
        timer += Time.deltaTime;
    }

    public virtual void Exit()
    {
        entity.animator.SetBool(animBoolName, false);
    }

    protected bool IsReturn()
    {
        if (isReturn)
        {
            isReturn = false;
            return true;
        }

        return false;
    }
}