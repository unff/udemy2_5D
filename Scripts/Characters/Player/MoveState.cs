using Godot;
using System;
using static Godot.TextServer;

public partial class MoveState : PlayerState
{
    
    public override void _PhysicsProcess( double delta ) {
        if ( characterNode.direction == Vector2.Zero ) {
            characterNode.stateMachineNode.SwitchState<IdleState>();
			return;
        }

		characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
		characterNode.Velocity *= 5;
		characterNode.MoveAndSlide();
		characterNode.Flip();
	}
  
	public override void _Input(InputEvent @event) {
		if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
			characterNode.stateMachineNode.SwitchState<DashState>();
		}
	}

	protected override void EnterState() {
		characterNode.animPlayerNode.Play(GameConstants.ANIM_MOVE);
	}
}
