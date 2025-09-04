using UnityEngine;
using UnityEngine.UIElements;

public class RuntimeUI : MonoBehaviour
{
    [SerializeField] public UIDocument UIDocument;
    // [SerializeField] public VisualTreeAsset Template;
    [SerializeField] public Inventory Inv;
    [SerializeField] public Resource metalResource;
    [SerializeField] public Resource gasResource;
    [SerializeField] public Resource energyResource;

    private Label metalLabel;
    private Label gasLabel;
    private Label energyLabel;

    private void Awake()
    {
        // Get references to the UI labels
        var root = UIDocument.rootVisualElement;
        metalLabel = new Label();
        gasLabel = new Label();
        energyLabel = new Label();
        
        // Add labels to the amount containers
        root.Q<VisualElement>("AmountMetal").Add(metalLabel);
        root.Q<VisualElement>("AmountGas").Add(gasLabel);
        root.Q<VisualElement>("AmountEnergy").Add(energyLabel);
        
        // Style the labels
        metalLabel.AddToClassList("amount-text");
        gasLabel.AddToClassList("amount-text");
        energyLabel.AddToClassList("amount-text");
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (Inv != null && metalResource != null && gasResource != null && energyResource != null)
        {
            metalLabel.text = Inv.GetResourceAmount(metalResource).ToString();
            gasLabel.text = Inv.GetResourceAmount(gasResource).ToString();
            energyLabel.text = Inv.GetResourceAmount(energyResource).ToString();
        }
    }
}
