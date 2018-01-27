using UnityEngine;

public class Movement : MonoBehaviour 
{
	public float speed;
	private Vector3 _movement;
	private Transform _transform;

	private void Awake()
	{
		_movement = new Vector3();
		_transform = transform;
	}

	/// <summary>
	/// Might want to move this to a fixed update method.
	/// </summary>
	private void Update()
	{
		var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");
		_movement.x = x;
		_movement.z = y;
		// Normalized because on keyboard you can move at a higher speed going
		// diagonal.
		_movement.Normalize();

		_transform.position += _movement * speed * Time.deltaTime;
	}
}
