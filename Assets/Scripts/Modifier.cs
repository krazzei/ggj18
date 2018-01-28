using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Modifier : MonoBehaviour 
{
    [System.Serializable]
    public struct ModifierData
    {
        public ModifierData(ModifierData data)
        {
            healthPercentage = data.healthPercentage;
            movementSpeedPercentage = data.movementSpeedPercentage;
            dodgeCooldownPercentage = data.dodgeCooldownPercentage;
            damageReductionPercentage = data.damageReductionPercentage;
            damageTakenPercentage = data.damageTakenPercentage;
            damageDealtPercentage = data.damageDealtPercentage;
            healthGrantedPercentage = data.healthGrantedPercentage;
            weakness = data.weakness;
        }

        /*
            Reducing HP by X%
            Reducing Movement speed by x%
            Increasing dodge cooldown by x%
            Reducing damage by x%
            Gaining weakness to a type of attack
            Increased damage taken and dealt.
            increase or decrease size.
        */
        public float healthPercentage;
        public float movementSpeedPercentage;
        public float dodgeCooldownPercentage;
        public float damageReductionPercentage;
        public float damageTakenPercentage;
        public float damageDealtPercentage;
        public float healthGrantedPercentage;

        public AbilityWeaknesses weakness;

    };

    public ModifierData ModData;

}
