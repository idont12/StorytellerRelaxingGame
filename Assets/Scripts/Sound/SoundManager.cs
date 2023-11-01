using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameEvent exampleEvent;
    
    [SerializeField] private AudioClip[] exampleClipsList;
    [SerializeField] private AudioSource exampleSource;
    private void Awake()
    {
        exampleEvent.RegisterListener(OnExampleEvent);
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
}
