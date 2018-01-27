using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
	Sword,
	/// <summary>
	/// Nice to have.
	/// </summary>
	Shield,
	Gun,
	Shotgun,
	Bombs,
	Spear,
	Scythe,
	/// <summary>
	/// Nice to have.
	/// </summary>
	Heal
}

public struct AbilityData
{
	public Ability abilityPrefab;
	public int amount;
	public AbilityType type;
	public float abilityCooldown;
}

public class AbilityActivator : MonoBehaviour
{
	private AbilityData _data;
	private float _lastTime;

    private void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && Time.time > _lastTime + _data.abilityCooldown)
		{
			_lastTime = Time.time;
			for (var i = 0; i < _data.amount; ++i)
			{
				Instantiate(_data.abilityPrefab, transform.position + transform.forward, transform.rotation);
			}
		}
    }
}
