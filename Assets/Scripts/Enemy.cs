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
		Attacking,
		Dead
	}

	public float aggroRange;
	public float attackRange;
	public float attackCooldown;

	private NavMeshAgent _agent;
	public GameObject _player;
	private EnemyState _state;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_state = EnemyState.Idle;
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
						_state = EnemyState.Attacking;
					}
				}
				break;
			case EnemyState.Attacking:
				// pew pew
				Debug.Log("Pew Pew");
				_agent.isStopped = true;
				if (Vector3.Distance(transform.position, _player.transform.position) > attackRange)
				{
					_state = EnemyState.Pathing;
				}
				break;
			case EnemyState.Dead:
				//bleh
				break;
		}
	}
}
