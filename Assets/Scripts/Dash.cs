﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(MouseAim), typeof(JoystickAim))]
[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(AbilityActivator))]
[RequireComponent(typeof(ModifierQueue), typeof(TwigsGhettoAnimator))]
public class Dash : MonoBehaviour
{
	public float cooldown;
	public float distance;
	public float duration;

	private float _lastDashTime;
	private Vector3 _endPoint;
	private bool _isDashing;
	private MouseAim _mouseAim;
	private JoystickAim _joyAim;
	private Movement _movement;
	private Collider _collider;
	private bool _didSwap;
	private AbilityActivator _abilityActivator;
	private Vector3 _capsuleCenterOffset;
	private Rigidbody _body;
	private ModifierQueue _modQueue;
	private TwigsGhettoAnimator _animator;

    private void Awake()
    {
		_movement = GetComponent<Movement>();
		_mouseAim = GetComponent<MouseAim>();
		_joyAim = GetComponent<JoystickAim>();
		_collider = GetComponent<Collider>();
		_body = GetComponent<Rigidbody>();
		_abilityActivator = GetComponent<AbilityActivator>();
		_modQueue = GetComponent<ModifierQueue>();
		_animator = GetComponent<TwigsGhettoAnimator>();

		_lastDashTime = Time.time - cooldown;
    }

    private void Update()
    {
        if (Input.GetAxis("Fire2") > 0 && Time.time > _lastDashTime + cooldown && !_isDashing)
		{
			_didSwap = false;
			_isDashing = true;
			// TODO: invincability.
			_endPoint = _movement.MovementDir * distance + transform.position;
			RaycastHit info;
			Debug.DrawLine(transform.position, _endPoint, Color.red, 1, true);
			if (Physics.Raycast(transform.position, _movement.MovementDir, out info, distance, LayerMask.GetMask("Wall", "Floor")))
			{
				_endPoint = info.point - _movement.MovementDir * 1.1f;
			}
			Debug.DrawLine(transform.position, _endPoint, Color.blue, 1, true);
			//Hud.instance.MakeCooldownBar(transform, cooldown);
			StartCoroutine(DashRoutine());
		}
		Hud.instance.UpdateDashCooldown(Time.time - _lastDashTime, cooldown);
    }

	private IEnumerator DashRoutine()
	{
		_collider.isTrigger = true;
		var mouseAim = _mouseAim.enabled;
		var joyAim = _joyAim.enabled;
		_mouseAim.enabled = false;
		_joyAim.enabled = false;
		_movement.enabled = false;

		_animator.ChangeAnimState(AnimationState.Dash, duration);

		var time = 0f;
		var startPos = transform.position;
		while (time < duration)
		{
			time += Time.deltaTime;
			_body.MovePosition(Vector3.Lerp(startPos, _endPoint, time / duration));
			yield return null;
		}
		_lastDashTime = Time.time;
		_isDashing = false;

		_animator.ChangeAnimState(AnimationState.Idle);

		_mouseAim.enabled = mouseAim;
		_joyAim.enabled = joyAim;
		_movement.enabled = true;
		_collider.isTrigger = false;
		_body.velocity = Vector3.zero;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (_didSwap)
		{
			return;
		}

		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			_didSwap = true;
			var enemy = other.GetComponent<Enemy>();
			if (enemy != null)
			{
				var enemyAbility = enemy.abilityData;
				enemy.abilityData = _abilityActivator.Swap(enemyAbility);
			}

			var mod = other.GetComponent<Modifier>();
			if (mod != null)
			{
				_modQueue.AddModifier(mod.ModData);
			}
		}
    }
}
