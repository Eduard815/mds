using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Scriptable Objects/Resource")]
public class Resource : ScriptableObject
{
    [field: SerializeField] public string ResourceName {get; private set;}
    [field: SerializeField] public Sprite Icon {get; private set;}
    [field: SerializeField] public string Description {get; private set;}
}
