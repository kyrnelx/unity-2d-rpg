using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
    }

    public override void Update()
    {
        base.Update();
        if (triggered)
        {
            GameObject.Destroy(player.gameObject);
            EventSubscriber.OnPlayerDead?.Invoke();
        }
    }
}