using UnityEngine;
using static Unity.VisualScripting.Member;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameEvent exampleEvent;
    [SerializeField] private GameEvent Grab;
    [SerializeField] private GameEvent FinishLevel;

    [SerializeField] private AudioClip[] exampleClipsList;
    [SerializeField] private AudioClip GrabSound;
    [SerializeField] private AudioClip FinishLevelSound;
    [SerializeField] private AudioSource exampleSource;
    private void Awake()
    {
        exampleEvent.RegisterListener(OnExampleEvent);
        Grab.RegisterListener(PlayGrabSound);
        FinishLevel.RegisterListener(PlayFinishLevel);
        DontDestroyOnLoad(transform.gameObject);
    }

    private GameObject[] music;

    void Start()
    {
        music = GameObject.FindGameObjectsWithTag("GameMusic");
        if (music.Length>1)
        {
            for (int i = 1; i < music.Length; i++)
            {
                if (music[i] != this)
                {
                    exampleEvent.UnregisterListener(OnExampleEvent);
                    Grab.UnregisterListener(PlayGrabSound);
                    FinishLevel.UnregisterListener(PlayFinishLevel);
                    Destroy(music[i]);
                }
            }
        }



    }

    private void Update()
    {
        if (exampleSource == null)
        {
            print("Exist" + exampleSource != null);
            exampleSource = gameObject.transform.Find("ExampleAudioSource").GetComponent<AudioSource>();
        }
    }

    private void OnDestroy()
    {
        exampleEvent.UnregisterListener(OnExampleEvent);
    }

    private void OnExampleEvent()
    {
        PlayRandomClipFromList(exampleClipsList, exampleSource);
    }

    private void PlayRandomClipFromList(AudioClip[] clips, AudioSource source)
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }

    private void PlayGrabSound()
    {
        print("exampleSource" + exampleSource.gameObject.name);
        playSoundEffect(GrabSound, exampleSource);
    }

    private void PlayFinishLevel()
    {
        playSoundEffect(FinishLevelSound, exampleSource);
    }

    private void playSoundEffect(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }
}
