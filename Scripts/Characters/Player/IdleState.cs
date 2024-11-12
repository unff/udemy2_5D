using Godot;
using System;

public partial class IdleState : PlayerState
{
    
    public override void _PhysicsProcess( double delta ) {
        if ( characterNode.direction != Vector2.Zero) {
            characterNode.stateMachineNode.SwitchState<MoveState>();
        }
    }
    
	public override void _Input(InputEvent @event) {
		if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
			characterNode.stateMachineNode.SwitchState<DashState>();
		}
	}

	protected override void EnterState() {
		characterNode.animPlayerNode.Play(GameConstants.ANIM_IDLE);
	}
}
