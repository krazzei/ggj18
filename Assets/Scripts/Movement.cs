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
		_movement.x = Input.GetAxis("Horizontal");
		_movement.z = Input.GetAxis("Vertical");
		// Normalized because on keyboard you can move at a higher speed going
		// diagonal.
		_movement.Normalize();

		_transform.position += _movement * speed * Time.deltaTime;
	}
}
