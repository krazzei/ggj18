using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour 
{
	private Transform _transform;
	private Camera _camera;

    private void Awake()
    {
		_transform = transform;

		enabled = PlayerPrefs.GetInt("Controller", 0) != 1;
    }

	private void Start()
	{
		_camera = Camera.main;
		if (_camera == null)
		{
			Debug.Log("Main camera not found!");
		}
	}

    private void Update()
	{
		// I wanted to make this just a Screen to world point, but due to the
		// 'Isometric' camera the translation followed the viewport.
		var ray = _camera.ScreenPointToRay(Input.mousePosition);

		RaycastHit info;
		if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Wall", "Floor")))
		{
			var lookAt = info.point;
			lookAt.y = _transform.position.y;
			_transform.LookAt(lookAt);
		}
	}
}
