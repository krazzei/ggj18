using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public enum EnemyState
	{
		Idle,
		Pathing,
		AttackBuildUp,
		Attacking,
		Dead
	}

	public float aggroRange;
	public float attackRange;
	public float attackWindup;
	public AbilityData abilityData;

	private NavMeshAgent _agent;
	private GameObject _player;
	private EnemyState _state;
	private float _attackBegin;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_state = EnemyState.Idle;
		// lulz
		_player = FindObjectOfType<Dash>().gameObject;
	}

	private void Update()
	{
		switch (_state)
		{
			case EnemyState.Idle:
				// search for player
				if (Vector3.Distance(transform.position, _player.transform.position) < aggroRange)
				{
					_state = EnemyState.Pathing;
				}
				break;
			case EnemyState.Pathing:
				if (Time.frameCount % 15 == 0 || _agent.isStopped)
				{
					_agent.isStopped = false;
					_agent.SetDestination(_player.transform.position);
					if (Vector3.Distance(transform.position, _player.transform.position) < attackRange)
					{
						StartAttack();
					}
				}
				break;
			case EnemyState.AttackBuildUp:
				_agent.isStopped = true;
				if (Time.time > _attackBegin + attackWindup)
				{
					_state = EnemyState.Attacking;
				}

				if (Vector3.Distance(transform.position, _player.transform.position) > attackRange)
				{
					_state = EnemyState.Pathing;
				}
				break;
			case EnemyState.Attacking:
				if (abilityData != null && abilityData.abilityPrefab != null)
				{
					for (var i = 0; i < abilityData.amount; ++i)
					{
						Instantiate(abilityData.abilityPrefab, transform.position + transform.forward, transform.rotation);
					}
				}
				_state = EnemyState.Idle;
				break;
			case EnemyState.Dead:
				Destroy(gameObject);
				break;
		}
	}

	private void StartAttack()
	{
		Debug.Log("Start Attack");
		_state = EnemyState.AttackBuildUp;
		_attackBegin = Time.time;
		//Hud.instance.MakeCooldownBar(transform, attackWindup);
	}
}
