using Godot;
using System;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready() {
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>() {
        Node newState = null;				// start from nothing, use the Node type since everything is a node and we don't need to run methods on it
        foreach(Node state in states) {		// for each node in the Array (as defined in the editor inspector for the character)
            if (state is T) {				// If this array member is the same type as the called type (T)
                newState = state;			// set newState as this array member.
            }
		// Node newState = states.Where((state) => state is T).FirstOrDefault();  // can also be done in System.Linq
        }
        if ( newState == null ) return; // character inspector is probably blank or the new state was not addced to the array there.
		currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);	// disable old currentState (send a 5002 godot notification)
        currentState = newState;											// change the currentState to the newState
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);	// enable new currentState (send a 5001 godot notification)
    }
}
