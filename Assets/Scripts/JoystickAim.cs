using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAim : MonoBehaviour 
{
	private Transform _transform;
	private Vector3 _aim;

    private void Awake()
    {
		_transform = transform;
		_aim = new Vector3();
    }

    private void Update()
    {
		var x = Input.GetAxis("Mouse X");
		var z = Input.GetAxis("Mouse Y");

		if (x == 0 && z == 0)
		{
			return;
		}

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
		// the driver I'm using inverts the Y axis.
		z *= -1;
#endif

		_aim.x = x;
		_aim.z = z;
		_aim.y = 0;

		_transform.LookAt(_aim + _transform.position);
    }
}
