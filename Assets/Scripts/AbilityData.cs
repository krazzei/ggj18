using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "Ability/Data")]
public class AbilityData : ScriptableObject
{
	public Ability abilityPrefab;
	public int amount;
	public float abilityCooldown;
}
