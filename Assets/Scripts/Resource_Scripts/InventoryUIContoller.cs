using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InventoryUIContoller : MonoBehaviour
{
    private static InventoryUIContoller instance;
    public string[] excludedScenes={"Main Menu"};//list with scenes names to hide ui
    public VisualElement ui;


    // Singleton pattern to ensure only one instance exists
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ui = GetComponentInChildren<UIDocument>().rootVisualElement;
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckScene();
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckScene();
    }


// Verifica daca scena curenta este in lista scenelor excluse si ascunde/afiseaza UI-ul.
    private void CheckScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        bool hide = false;
        foreach (string sceneName2 in excludedScenes)
        {
            if (sceneName.Equals(sceneName2))
            {
                hide = true;
                break;
            }
        }
        gameObject.SetActive(!hide);
    }
}
