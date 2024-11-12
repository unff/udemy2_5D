using Godot;
using System;

public partial class DashState : Node {
	[Export] private Timer dashTimerNode;
	[Export] private float speed = 10;

	private Player characterNode;
	public override void _Ready() {
		characterNode = GetOwner<Player>();
		dashTimerNode.Timeout += OnDashTimeout;
		//SetPhysicsProcess(false);
	}


	public override void _PhysicsProcess(double delta) {
		characterNode.MoveAndSlide();
		characterNode.Flip();
	}
	public override void _Notification(int what) {
		base._Notification(what);
		if (what == 5001) {
			characterNode.animPlayerNode.Play(GameConstants.ANIM_DASH);
			characterNode.Velocity = new(characterNode.direction.X,0,characterNode.direction.Y);
			if (characterNode.Velocity == Vector3.Zero) {
				characterNode.Velocity = characterNode.spriteNode.FlipH ? Vector3.Left : Vector3.Right;
			}
			characterNode.Velocity *= speed;
			dashTimerNode.Start();
			//SetPhysicsProcess(true);
		}
		else if (what == 5002) {
			//SetPhysicsProcess(false);
		}
	}
	private void OnDashTimeout() {
		characterNode.Velocity = Vector3.Zero;
		characterNode.stateMachineNode.SwitchState<IdleState>();
	}
}
