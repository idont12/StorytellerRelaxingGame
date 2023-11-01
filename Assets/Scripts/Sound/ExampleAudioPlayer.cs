using UnityEngine;

public class ExampleAudioPlayer : MonoBehaviour
{
    [SerializeField] private GameEvent exampleEvent;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            exampleEvent.Raise();
        }
    }
}
