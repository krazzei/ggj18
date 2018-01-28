using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(Dash), typeof(Movement))]
[RequireComponent(typeof(JoystickAim), typeof(MouseAim))]
public class Player : MonoBehaviour 
{
	private Health _health;
	private Dash _dash;
	private Movement _movement;
	private JoystickAim _joyaim;
	private MouseAim _mouseaim;

	private void Awake()
	{
		_health = GetComponent<Health>();
		_dash = GetComponent<Dash>();
		_movement = GetComponent<Movement>();
		_joyaim = GetComponent<JoystickAim>();
		_mouseaim = GetComponent<MouseAim>();
	}

	private void Start () 
	{
		_health.OnDeath += OnDeath;
	}

	private void OnDeath()
	{
		_dash.enabled = false;
		_movement.enabled = false;
		_joyaim.enabled = false;
		_mouseaim.enabled = false;
		Hud.instance.Dead();
	}
}
