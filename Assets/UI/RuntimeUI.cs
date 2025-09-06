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
    [SerializeField] public Resource researchResource;
    [SerializeField] public Resource shipResource;
    [SerializeField] public Resource planetResource;

    private Label metalLabel;
    private Label gasLabel;
    private Label energyLabel;
    private Label researchLabel;
    private Label shipLabel;
    private Label planetLabel;

    private Label metalGainLabel;
    private Label gasGainLabel;
    private Label energyGainLabel;
    private Label researchGainLabel;
    private Label shipLimitLabel;

    private void Awake()
    {
        // Get references to the UI labels
        var root = UIDocument.rootVisualElement;
        metalLabel = new Label("0");
        gasLabel = new Label("0");
        energyLabel = new Label("0");
        researchLabel = new Label("1");
        shipLabel = new Label("1");
        planetLabel = new Label("1");

        metalGainLabel = new Label("+0");
        gasGainLabel = new Label("+0");
        energyGainLabel = new Label("+0");
        researchGainLabel = new Label("+1");
        
        shipLimitLabel = new Label("/5");
        
        // Add labels to the amount containers
        root.Q<VisualElement>("AmountMetal").Add(metalLabel);
        root.Q<VisualElement>("AmountMetal").Add(metalGainLabel);

        root.Q<VisualElement>("AmountGas").Add(gasLabel);
        root.Q<VisualElement>("AmountGas").Add(gasGainLabel);

        root.Q<VisualElement>("AmountEnergy").Add(energyLabel);
        root.Q<VisualElement>("AmountEnergy").Add(energyGainLabel);

        root.Q<VisualElement>("AmountResearch").Add(researchLabel);
        root.Q<VisualElement>("AmountResearch").Add(researchGainLabel);

        root.Q<VisualElement>("AmountShip").Add(shipLabel);
        root.Q<VisualElement>("AmountShip").Add(shipLimitLabel);

        root.Q<VisualElement>("AmountPlanet").Add(planetLabel);
        
        // Style the labels
        metalLabel.AddToClassList("amount-text");
        metalGainLabel.AddToClassList("gain-text");

        gasLabel.AddToClassList("amount-text");
        gasGainLabel.AddToClassList("gain-text");

        energyLabel.AddToClassList("amount-text");
        energyGainLabel.AddToClassList("gain-text");

        researchLabel.AddToClassList("amount-text");
        researchGainLabel.AddToClassList("gain-text");

        shipLabel.AddToClassList("amount-text");
        shipLimitLabel.AddToClassList("limit-text");

        planetLabel.AddToClassList("amount-text");
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
        if (Inv != null && metalResource != null && gasResource != null && energyResource != null && researchResource != null && shipResource != null && planetResource != null)
        {
            metalLabel.text = Inv.GetResourceAmount(metalResource).ToString();
            metalGainLabel.text = "+" + Inv.GetResourceGain(metalResource).ToString();

            gasLabel.text = Inv.GetResourceAmount(gasResource).ToString();
            gasGainLabel.text = "+" + Inv.GetResourceGain(gasResource).ToString();

            energyLabel.text = Inv.GetResourceAmount(energyResource).ToString();
            energyGainLabel.text = "+" + Inv.GetResourceGain(energyResource).ToString();

            researchLabel.text = Inv.GetResourceAmount(researchResource).ToString();
            researchGainLabel.text = "+" + Inv.GetResourceGain(researchResource).ToString();

            shipLabel.text = Inv.GetResourceAmount(shipResource).ToString();
            shipLimitLabel.text = Inv.GetResourceLimit(shipResource).ToString();

            planetLabel.text = Inv.GetResourceAmount(planetResource).ToString();
        }
    }
}
