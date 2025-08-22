using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public SerializableDictionary<Resource, int> Resources {get; private set;}

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

    public int AddResource(Resource resource, int amount)
    {
        if (Resources.TryGetValue(resource, out int amount2))
        {
            return Resources[resource] += amount;
        }
        else
        {
            Resources.Add(resource,amount);
            return amount;
        }
    }    
}
