using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivator : MonoBehaviour
{
	private AbilityData _data;
	private float _lastTime;

    private void Update()
    {
		if (_data != null && _data.abilityPrefab != null && Input.GetAxis("Fire1") > 0 && Time.time > _lastTime + _data.abilityCooldown)
		{
			_lastTime = Time.time;
			for (var i = 0; i < _data.amount; ++i)
			{
				Instantiate(_data.abilityPrefab, transform.position + transform.forward, transform.rotation);
			}
		}
    }

	public AbilityData Swap(AbilityData newData)
	{
		var oldData = _data;
		_data = newData;
		return oldData;
	}
}
