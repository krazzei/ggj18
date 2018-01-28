using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState
{
	Idle,
	Walk,
	Attack,
	Dash
}

public class TwigsGhettoAnimator : MonoBehaviour 
{
	[SerializeField]
	private Animator _animator;
	private float _lastTime;
	private float _priorityDuration;

	public void ChangeAnimState(AnimationState state, float priorityDuration = -1)
	{
		if (Time.time < _lastTime + _priorityDuration)
		{
			Debug.Log("priority");
			return;
		}

		_priorityDuration = priorityDuration;
		_lastTime = Time.time;

		_animator.SetBool("Idle", false);
		_animator.SetBool("Walk", false);
		_animator.SetBool("Attack", false);
		_animator.SetBool("Dash", false);

		switch (state)
		{
			case AnimationState.Idle:
				_animator.SetBool("Idle", true);
				break;
			case AnimationState.Walk:
				_animator.SetBool("Walk", true);
				break;
			case AnimationState.Attack:
				_animator.SetBool("Attack", true);
				break;
			case AnimationState.Dash:
				_animator.SetBool("Dash", true);
				break;
		}
	}
}
