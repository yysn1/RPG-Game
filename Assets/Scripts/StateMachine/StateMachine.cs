public class StateMachine
{
    public EntityState currentState { get; private set; }
    public bool canChangeNewState;

    public void Initialize(EntityState startingState)
    {
        canChangeNewState = true;
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        if (!canChangeNewState)
            return;

        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdataActiveState()
    {
        currentState.Update();
    }

    public void SwitchOffStateMachine() => canChangeNewState = false;
}
