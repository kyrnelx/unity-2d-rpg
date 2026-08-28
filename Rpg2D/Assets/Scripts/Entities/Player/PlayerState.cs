public class PlayerState : State
{
    protected Player player;

    public PlayerState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine, animBoolName,
        entity)
    {
        player = entity as Player;
    }
}