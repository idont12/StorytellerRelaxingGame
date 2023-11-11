using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControle : MonoBehaviour
{
    public string backSceane = "Menu";
    [SerializeField] GameObject SoundWindow;
    bool isSoundWindowOpen = false;
    float windowsOpenSpeed = 0.5f;


    public void GoBack()
    {
        SceneManager.LoadScene(backSceane);
    }
    public void SwitchSoundWindow()
    {
        LeanTween.cancel(SoundWindow);
        isSoundWindowOpen = !isSoundWindowOpen;
        Vector3 goalPosition = new Vector3(0, 620, 0);
        if (isSoundWindowOpen)
        {
            goalPosition = Vector3.zero;
        }
        LeanTween.moveLocal(SoundWindow, goalPosition, windowsOpenSpeed).setEase(LeanTweenType.easeOutCubic);


    }
}
