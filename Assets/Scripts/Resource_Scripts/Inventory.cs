using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public SerializableDictionary<Resource, int> Resources { get; private set; } = new();
    [field: SerializeField] public SerializableDictionary<Resource, int> Gains {get; private set; } = new();
    [field: SerializeField] public SerializableDictionary<Resource, int> Limit {get; private set; } = new();


    /// Functions for setting the initial values for the resources amounts, gains and limit
    public void SetInitialAmount(Resource resource, int initialAmount){
        if (resource == null){
            return;
        }
        if (!Resources.TryGetValue(resource, out var current) || current == 0){
            Resources[resource] = initialAmount;
        }
    }

    public void SetInitialGain(Resource resource, int initialGain){
        if (resource == null){
            return;
        }
        if (!Gains.TryGetValue(resource, out var current) || current == 0){
            Gains[resource] = initialGain;
        }
    }

    public void SetInitialLimit(Resource resource, int initialLimit){
        if (resource == null){
            return;
        }
        if (!Limit.TryGetValue(resource, out var current) || current == 0){
            Limit[resource] = initialLimit;
        }
    }




    public int GetResourceAmount(Resource resource)
    {
        if (Resources.TryGetValue(resource, out int amount))
        {
            return amount;
        }
        else
        {
            return 0;
        }
    }

    public int GetResourceGain(Resource resource){
        if (Gains.TryGetValue(resource, out int gain)){
            return gain;
        }
        else {
            return 0;
        }
    }

    public int GetResourceLimit(Resource resource){
        if (Limit.TryGetValue(resource, out int limit)){
            return limit;
        }
        else {
            return 0;
        }
    }

    /// Adding the resources to the current amount
    public int AddResource(Resource resource, int amount)
    {
        int currentAmount = GetResourceAmount(resource);
        int lim = GetResourceLimit(resource);
        int newAmount = currentAmount + amount;
        if (lim > 0){
            newAmount = Mathf.Min(lim, newAmount);
        }
        Resources[resource] = newAmount;
        return newAmount;
    }


    /// Passing to the next turn
    public void ApplyTurn(){
        Debug.Log("!!!!!!!!!!!!! All resources updated !!!!!!!!!!!!!");
        var keys = GetAllTrackedResources();
        foreach (var resource in keys){
            int gain = GetResourceGain(resource);
            int currentAmount = GetResourceAmount(resource);
            int lim = GetResourceLimit(resource);
            int newAmount = currentAmount + gain;
            if (lim > 0){
                newAmount = Mathf.Min(newAmount, lim);
            }
            Resources[resource] = newAmount;

            Debug.Log($"Resource {resource.ResourceName} | Current: {currentAmount} | Gain: {gain} | Limit: {lim} | New Amount: {newAmount}");
        }
    }


    private List<Resource>GetAllTrackedResources(){
        var set = new HashSet<Resource>();
        foreach (var r in Resources.Keys){
            if (r != null){
                set.Add(r);
            }
        }
        foreach (var r in Gains.Keys){
            if (r != null){
                set.Add(r);
            }
        }
        foreach (var r in Limit.Keys){
            if (r != null){
                set.Add(r);
            }
        }

        return new List<Resource>(set);
    }
}
