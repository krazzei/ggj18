using UnityEngine;

[RequireComponent(typeof(TwigsGhettoAnimator))]
public class Movement : MonoBehaviour 
{
	public float speed;
	private Vector3 _movement;
	private Rigidbody _body;
	private TwigsGhettoAnimator _animator;

	public Vector3 MovementDir
	{
		get { return _movement; }
	}

	private void Awake()
	{
		_movement = new Vector3();
		_body = GetComponent<Rigidbody>();
		_animator = GetComponent<TwigsGhettoAnimator>();
	}

	/// <summary>
	/// Might want to move this to a fixed update method.
	/// </summary>
	private void FixedUpdate()
	{
		_movement.x = Input.GetAxis("Horizontal");
		_movement.z = Input.GetAxis("Vertical");
		// Normalized because on keyboard you can move at a higher speed going
		// diagonal.
		_movement.Normalize();

		if (Vector3.Distance(_movement, Vector3.zero) < 0.01f)
		{
			_animator.ChangeAnimState(AnimationState.Idle);
		}
		else
		{
			_animator.ChangeAnimState(AnimationState.Walk);
		}

		//_transform.position += _movement * speed * Time.deltaTime;
		_body.MovePosition(_body.position + _movement * speed * Time.deltaTime);
	}
}
