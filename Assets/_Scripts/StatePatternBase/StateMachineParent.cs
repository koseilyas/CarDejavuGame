using UnityEngine;

public abstract class StateMachineParent : MonoBehaviour
{
	public IState CurrentState { get; private set; }

	public void ChangeState(IState newState)
	{
		if (CurrentState == newState)
			return;

		ChangeStateRoutine(newState);
	}

	void ChangeStateRoutine(IState newState)
	{
		if (CurrentState != null)
			CurrentState.Exit();

		CurrentState = newState;
		
		if (CurrentState != null)
			CurrentState.Enter();
		
	}
	
	public void Update()
	{
		if (CurrentState != null)
			CurrentState.UpdateState();
	}

    public void FixedUpdate()
    {
	    if (CurrentState != null)
			CurrentState.FixedUpdateState();
    }
}
