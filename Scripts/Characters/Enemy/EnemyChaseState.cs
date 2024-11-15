using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState {
	[Export] private Timer chaseTimerNode;
	[Export(PropertyHint.Range, "0,20,0.1")] private float updateFrequency;
	private CharacterBody3D target;
	protected override void EnterState() {
		characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
		target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D; // yikes.  will never work with multiplayer.
		chaseTimerNode.Timeout += HandleChaseTimerNodeTimeout;
		characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
		characterNode.AttackAreaNode.BodyExited += HandleChaseAreaBodyExited;
	}

	

	protected override void ExitState() {
		chaseTimerNode.Timeout -= HandleChaseTimerNodeTimeout;
		characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
		characterNode.AttackAreaNode.BodyExited -= HandleChaseAreaBodyExited;
	}
	public override void _PhysicsProcess(double delta) {
		
		Move();
	}
	private void HandleChaseTimerNodeTimeout() {
		destination = target.GlobalPosition;
		characterNode.AgentNode.TargetPosition = destination;
	}
	private void HandleAttackAreaBodyEntered(Node3D body) {
		characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
	}
	private void HandleChaseAreaBodyExited(Node3D body) {
		characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
	}
}


