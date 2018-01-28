using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModifierQueue : MonoBehaviour 
{
    [SerializeField]
    private int numModifiersToHold = 5;

    public System.Action<Modifier.ModifierData> OnModAdded;
    public System.Action<Modifier.ModifierData> OnModRemoved;

    private Queue<Modifier> _modifierQueue;

    private void Awake()
    {
        _modifierQueue = new Queue<Modifier>(numModifiersToHold);
    }

    public List<Modifier.ModifierData> GetAllModifierData()
    {
        var results =
            from mod in _modifierQueue
            select mod.ModData;
        return results.ToList();
    }

    public void AddModifier(Modifier.ModifierData modifierData)
    {
        if (_modifierQueue.Count == numModifiersToHold)
        {
            var oldMod = _modifierQueue.Dequeue();
            OnModRemoved(oldMod.ModData);
        }
        var modifier = gameObject.AddComponent<Modifier>();
        modifier.ModData = modifierData;
        _modifierQueue.Enqueue(modifier);
        OnModAdded(modifier.ModData);
    }
}
