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

    private Label metalLabel, gasLabel, energyLabel, researchLabel, shipLabel, planetLabel;
    private Label metalGainLabel, gasGainLabel, energyGainLabel, researchGainLabel,shipLimitLabel;

    private VisualElement root;
    private VisualElement metalVE, gasVE, energyVE, researchVE, shipVE, planetVE;

    private VisualElement tooltip;
    private Label tooltipTitle, tooltipBody;

    private void Awake()
    {
        // Get references to the UI labels
        root = UIDocument.rootVisualElement;
        Inv = FindObjectOfType<Inventory>();

        // At the awake moment, we use the functions declared in Inventory.cs to set the initial amounts, gains, and limits for the resources
        if (Inv != null)
        {
            Inv.SetInitialAmount(metalResource, 10);
            Inv.SetInitialGain(metalResource, 3);

            Inv.SetInitialAmount(gasResource, 10);
            Inv.SetInitialGain(gasResource, 3);

            Inv.SetInitialAmount(energyResource, 10);
            Inv.SetInitialGain(energyResource, 5);

            Inv.SetInitialAmount(researchResource, 1);
            Inv.SetInitialGain(researchResource, 1);

            Inv.SetInitialAmount(shipResource, 1);
            Inv.SetInitialLimit(shipResource, 5);

            Inv.SetInitialAmount(planetResource, 1);
        }


        metalVE = root.Q<VisualElement>("Metal");
        gasVE = root.Q<VisualElement>("Gas");
        energyVE = root.Q<VisualElement>("Energy");
        researchVE = root.Q<VisualElement>("Research");
        shipVE = root.Q<VisualElement>("Ship");
        planetVE = root.Q<VisualElement>("Planet");

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





        tooltip = new VisualElement();
        tooltip.AddToClassList("tooltip");
        tooltip.pickingMode = PickingMode.Ignore;

        tooltipTitle = new Label();
        tooltipTitle.AddToClassList("tooltip-title");
        tooltipBody = new Label();
        tooltipBody.AddToClassList("tooltip-body");

        tooltip.Add(tooltipTitle);
        tooltip.Add(tooltipBody);
        tooltip.style.display = DisplayStyle.None;
        root.Add(tooltip);



        metalVE.RegisterCallback<PointerEnterEvent>(OnMetalEnter);
        metalVE.RegisterCallback<PointerMoveEvent>(OnAnyMove);
        metalVE.RegisterCallback<PointerLeaveEvent>(OnAnyLeave);

        gasVE.RegisterCallback<PointerEnterEvent>(OnGasEnter);
        gasVE.RegisterCallback<PointerMoveEvent>(OnAnyMove);
        gasVE.RegisterCallback<PointerLeaveEvent>(OnAnyLeave);

        energyVE.RegisterCallback<PointerEnterEvent>(OnEnergyEnter);
        energyVE.RegisterCallback<PointerMoveEvent>(OnAnyMove);
        energyVE.RegisterCallback<PointerLeaveEvent>(OnAnyLeave);

        researchVE.RegisterCallback<PointerEnterEvent>(OnResearchEnter);
        researchVE.RegisterCallback<PointerMoveEvent>(OnAnyMove);
        researchVE.RegisterCallback<PointerLeaveEvent>(OnAnyLeave);

        shipVE.RegisterCallback<PointerEnterEvent>(OnShipEnter);
        shipVE.RegisterCallback<PointerMoveEvent>(OnAnyMove);
        shipVE.RegisterCallback<PointerLeaveEvent>(OnAnyLeave);

        planetVE.RegisterCallback<PointerEnterEvent>(OnPlanetEnter);
        planetVE.RegisterCallback<PointerMoveEvent>(OnAnyMove);
        planetVE.RegisterCallback<PointerLeaveEvent>(OnAnyLeave);
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
            shipLimitLabel.text = "/" + Inv.GetResourceLimit(shipResource).ToString();

            planetLabel.text = Inv.GetResourceAmount(planetResource).ToString();
        }
        return;
    }

    /// Calling UpdateUI to update the UI
    public void RefreshUI(){
        UpdateUI();
    }


    private void ShowTooltip(string title, string body, Vector2 panelPosition){
        tooltipTitle.text = title;
        tooltipBody.text = body;

        tooltip.style.display = DisplayStyle.Flex;
        MoveTooltip(panelPosition);
    }


    private void HideTooltip(){
        tooltip.style.display = DisplayStyle.None;
    }


    private void MoveTooltip(Vector2 panelPos){
        const float offset = 16f;
        float x = panelPos.x + offset;
        float y = panelPos.y + offset;
        float w = tooltip.layout.width > 0 ? tooltip.layout.width : 220f;
        float h = tooltip.layout.height > 0 ? tooltip.layout.height : 100f;

        x = Mathf.Min(x, root.layout.width - w - 6f);
        y = Mathf.Min(y, root.layout.height - h - 6f);

        tooltip.style.left = x;
        tooltip.style.top = y;
        tooltip.style.position = Position.Absolute;
    }


    private string BuildTooltipBodyPlanet(Resource res, int amount){
        return res.Description + "\n\nColonized worlds: " + amount;
    }

    private string BuildTooltipBody(Resource res, int amount, int gain){
        return res.Description + "\n\nIn storage: " + amount + "\nGaining per turn: " + gain;
    }

    private string BuildTooltipBodyWithLimit(Resource res, int amount, int limit){
        return res.Description + "\n\nOwned: " + amount + " / " + limit;
    }




    private void OnMetalEnter(PointerEnterEvent e){
        int amount = Inv.GetResourceAmount(metalResource);
        int gain = Inv.GetResourceGain(metalResource);
        ShowTooltip(metalResource.ResourceName, BuildTooltipBody(metalResource, amount, gain), e.position);
    }

    private void OnGasEnter(PointerEnterEvent e){
        int amount = Inv.GetResourceAmount(gasResource);
        int gain = Inv.GetResourceGain(gasResource);
        ShowTooltip(gasResource.ResourceName, BuildTooltipBody(gasResource, amount, gain), e.position);
    }

    private void OnEnergyEnter(PointerEnterEvent e){
        int amount = Inv.GetResourceAmount(energyResource);
        int gain = Inv.GetResourceGain(energyResource);
        ShowTooltip(energyResource.ResourceName, BuildTooltipBody(energyResource, amount, gain), e.position);
    }

    private void OnResearchEnter(PointerEnterEvent e){
        int amount = Inv.GetResourceAmount(researchResource);
        int gain = Inv.GetResourceGain(researchResource);
        ShowTooltip(researchResource.ResourceName, BuildTooltipBody(researchResource, amount, gain), e.position);
    }

    private void OnShipEnter(PointerEnterEvent e){
        int amount = Inv.GetResourceAmount(shipResource);
        int limit = Inv.GetResourceLimit(shipResource);
        ShowTooltip(shipResource.ResourceName, BuildTooltipBodyWithLimit(shipResource, amount, limit), e.position);
    }

    private void OnPlanetEnter(PointerEnterEvent e){
        int amount = Inv.GetResourceAmount(planetResource);
        ShowTooltip(planetResource.ResourceName, BuildTooltipBodyPlanet(planetResource, amount), e.position);
    }

    private void OnAnyMove(PointerMoveEvent e){
        if (tooltip.style.display == DisplayStyle.Flex){
            MoveTooltip(e.position);
        }
    }

    private void OnAnyLeave(PointerLeaveEvent e){
        HideTooltip();
    }

}