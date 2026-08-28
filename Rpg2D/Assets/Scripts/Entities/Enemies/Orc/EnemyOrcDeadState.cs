using UnityEngine;

public class EnemyOrcDeadState : EnemyOrcState
{
    public EnemyOrcDeadState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (enemyOrc.IsTriggered())
        {
            GameObject.Destroy(enemyOrc.gameObject);
        }
    }
}