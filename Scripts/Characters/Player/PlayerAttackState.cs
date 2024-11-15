using Godot;
using Godot.Collections;
using System;

public partial class PlayerAttackState : PlayerState
{
	[Export] private Timer comboTimerNode;
	private int comboCounter = 1;
	private int maxComboCounts = 2;

	public override void _Ready() {
		base._Ready();
		comboTimerNode.Timeout += () => comboCounter = 1;
	}
	protected override void EnterState() {
		//GD.Print(GameConstants.ANIM_ATTACK + comboCounter);
		characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK + comboCounter,-1,1.5f);
		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
	}
	protected override void ExitState() {
		characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
		comboTimerNode.Start();
	}

	private void HandleAnimationFinished(StringName animName) {
		//GD.Print(comboCounter);
		comboCounter++;
		comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCounts + 1);
		characterNode.StateMachineNode.SwitchState<IdleState>();
	}
}
