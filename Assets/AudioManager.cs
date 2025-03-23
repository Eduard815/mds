using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource; //audio for music
    [SerializeField] AudioSource soundSource; //audio for sounds

    [Header("Audio Clips")]
    public AudioClip eclipse;
    public AudioClip solstice;
    public AudioClip warfare;
    public AudioClip click;

    private AudioClip currentclip; //I wanted the game to play different soundtracks one after another so that no two consecutive soundtracks are the same
    
    [Header("Sliders")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    void Start() //the function executes right as the game starts
    {
        AudioClip[] v = {eclipse, solstice, warfare};
        if (!musicSource.playOnAwake)
        {
            currentclip = v[Random.Range(0, v.Length)];
            musicSource.clip = currentclip; //the music file is now handled and played
            musicSource.Play();
        }
        DontDestroyOnLoad(this.gameObject); //keeps the music playing for all scenes, or at least it should, gameObject refers to AudioManager

        //setting the volume for the sliders so that they represent the exact volume
        musicSlider.value = musicSource.volume;
        soundSlider.value = soundSource.volume;

        //setting the volume of the sliders
        musicSlider.onValueChanged.AddListener(SetVolume_Music);
        soundSlider.onValueChanged.AddListener(SetVolume_Sound);

        //finding all the buttons and applying the click sound for them
        Button[] buttons = GameObject.FindObjectsOfType<Button>(); //vector of all buttons
        foreach (Button b in buttons)
        {
            b.onClick.AddListener(ClickSound);
        }
    }
    //update per frame when music doesnt play
    void Update()
    {
        if (!musicSource.isPlaying)
        {
            AudioClip[] v = {eclipse, solstice, warfare}; 
            AudioClip newclip = v[Random.Range(0, v.Length)]; //we got a newclip and a currentclip. once the two are different we can finally play the newclip
            while (currentclip == newclip) newclip = v[Random.Range(0, v.Length)];
            currentclip = newclip; //and the current clip becomes the new clip
            musicSource.clip = currentclip;
            musicSource.Play(); 
        } //the cycle repeats itself while we are playing
    }
    void ClickSound()
    {
        soundSource.PlayOneShot(click); //click that thang, girl
    }
    
    //setting the sliders' volume
    void SetVolume_Music(float volume)
    {
        musicSource.volume = volume;
    }

    void SetVolume_Sound(float volume)
    {
        soundSource.volume = volume;
    }
}
