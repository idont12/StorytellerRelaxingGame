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
