using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
	Sword, 
	Shield, 
	Gun, 
	Shotgun, 
	Bombs, 
	Spear, 
	Scythe, 
	Heal
}

public class AbilityActivator : MonoBehaviour 
{
	public Ability _abilityPrefab;
	public float _abilityStartUp;
	public AbilityType _type;

    private void Update()
    {
		Debug.Log(Input.GetAxis("Fire1"));
        if (Input.GetAxis("Fire1") > 0)
		{
			// do the thing
			Debug.Log("Doing it!");
		}
    }
}
