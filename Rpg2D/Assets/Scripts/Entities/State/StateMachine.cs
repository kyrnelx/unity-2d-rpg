public class StateMachine
{
    public State currentState { get; private set; }

    public void Initialise(State state)
    {
        currentState = state;
        state.Enter();
    }

    public void ChangeState(State state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}