using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public SerializableDictionary<Resource, int> Resources { get; private set; }
    [field: SerializeField] public SerializableDictionary<Resource, int> Gains {get; private set; } = new();
    [field: SerializeField] public SerializableDictionary<Resource, int> Limit {get; private set; } = new();

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
