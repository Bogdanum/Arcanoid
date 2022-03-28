using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnergyActionList
{
    [SerializeField] private ActionValuePair[] actionValuePairs;
    private Dictionary<ActionWithEnergy, int> valuePerActionMap;

    public void Init()
    {
        valuePerActionMap = new Dictionary<ActionWithEnergy, int>();
        foreach (var pair in actionValuePairs)
        {
            valuePerActionMap.Add(pair.actionType, pair.value);
        }
    }

    public int GetEnergyValue(ActionWithEnergy actionWithEnergy) => valuePerActionMap[actionWithEnergy];

    [Serializable]
    internal class ActionValuePair
    {
        public ActionWithEnergy actionType;
        public int value;
    }
}
