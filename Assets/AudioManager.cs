using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; //to keep thangs going during scenes (see awake function)

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource soundSource;

    [Header("Audio Clips")]
    public AudioClip eclipse;
    public AudioClip solstice;
    public AudioClip warfare;
    public AudioClip drift;
    public AudioClip no_horizon;
    public AudioClip click;

    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private AudioClip currentclip;

    //happens from the start
    void Awake() 
    {
        //Audio manager is now saved between scenes with Dontdestroyonload
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);  //if we already have an audio manager, it gets destroyed for ours
            return;
        }
        DontDestroyOnLoad(this.gameObject);  //keeps Audio manager between scenes
        SceneManager.sceneLoaded += OnSceneLoaded; //on scene loaded, it calls the OnSceneLoaded function
    }

    //when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var allSliders = GameObject.FindObjectsOfType<Slider>(true);
        foreach (var slider in allSliders)
        {
            if (slider.name == "MusicSlider") musicSlider = slider;
            if (slider.name == "SoundSlider") soundSlider = slider;
        }

        if (musicSlider == null || soundSlider == null)
        {
            return; //debugging
        }
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        musicSlider.value = musicVolume;
        soundSlider.value = soundVolume;
        musicSource.volume = musicVolume;
        soundSource.volume = soundVolume;

        musicSlider.onValueChanged.RemoveAllListeners();
        soundSlider.onValueChanged.RemoveAllListeners();

        //the abilty to change volume by using sliders:
        musicSlider.onValueChanged.AddListener(SetVolume_Music);
        soundSlider.onValueChanged.AddListener(SetVolume_Sound);

        //finding every button from the scene
        Button[] buttons = GameObject.FindObjectsOfType<Button>(true);
        foreach (Button b in buttons)
        {
            b.onClick.AddListener(ClickSound);
        }

    }

    void Start()
    {
        AudioClip[] v = {eclipse, no_horizon, drift, solstice, warfare};
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1f); 

        if (!musicSource.playOnAwake)
        {
            currentclip = v[Random.Range(0, v.Length)];
            musicSource.clip = currentclip; //the music file is now handled and played
            musicSource.Play();
        }
    }

    //update per frame when music doesnt play
    void Update()
    {
        if (!musicSource.isPlaying)
        {
            AudioClip[] v = {eclipse, no_horizon, drift, solstice, warfare};
            AudioClip newclip;
            do
            {
                newclip = v[Random.Range(0, v.Length)];
            } while (newclip == currentclip);
            currentclip = newclip;
            musicSource.clip = currentclip;
            musicSource.Play(); //the cycle repeats itself while we are playing 
        } 
    }

    public void ClickSound()
    {
        soundSource.PlayOneShot(click);
    }

    void SetVolume_Music(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); //saving volume for next scene
    }

    void SetVolume_Sound(float volume)
    {
        soundSource.volume = volume;
        PlayerPrefs.SetFloat("SoundVolume", volume); //saving volume for next scene
    }

    
}