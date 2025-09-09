using UnityEngine;

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

    public int AddResource(Resource resource, int amount)
    {
        if (Resources.TryGetValue(resource, out int amount2))
        {
            return Resources[resource] += amount;
        }
        else
        {
            Resources.Add(resource, amount);
            return amount;
        }
    }
}
