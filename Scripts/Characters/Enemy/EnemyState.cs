using Godot;
using System;

public abstract partial class EnemyState : CharacterState {
	protected Vector3 destination;

	protected Vector3 GetPointGlobalPosition(int idx) {
		Vector3 localPos = characterNode.PathNode.Curve.GetPointPosition(idx);
		Vector3 globalPos = characterNode.PathNode.GlobalPosition;
		return localPos + globalPos;
	}
	protected void Move() {
		characterNode.AgentNode.GetNextPathPosition();
		characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination);
		characterNode.MoveAndSlide();
		characterNode.Flip();
	}

	protected void HandleChaseAreaBodyEntered(Node3D body) {
		characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
	}
}
