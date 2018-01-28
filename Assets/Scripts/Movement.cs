using UnityEngine;

public class Movement : MonoBehaviour 
{
	public float speed;
	private Vector3 _movement;
	private Rigidbody _body;

	public Vector3 MovementDir
	{
		get { return _movement; }
	}

	private void Awake()
	{
		_movement = new Vector3();
		_body = GetComponent<Rigidbody>();
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

		//_transform.position += _movement * speed * Time.deltaTime;
		_body.MovePosition(_body.position + _movement * speed * Time.deltaTime);
	}
}
