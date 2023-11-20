using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionItem : MonoBehaviour
{
    [SerializeField] int LevelNum;
    [SerializeField] TMP_Text LevelName;
    [SerializeField] UnityEngine.UI.Image LevelStatus;
    [SerializeField] Sprite FinishIcon;
    [SerializeField] Sprite UnfinishIcon;
    [SerializeField] GameGeneralInfo Generalinfo;
    private void Start()
    {
        LevelName.text = "ωμα " + LevelNum.ToString();
        Sprite stauseIcon = UnfinishIcon;
        if (PlayerPrefs.GetInt("Level" + LevelNum.ToString())==1)
        {
            stauseIcon = FinishIcon;
        }
        LevelStatus.sprite = stauseIcon;
    }

    public void GoToLevel()
    {
        string levelName = Generalinfo.getLevelbyOrderNum(LevelNum-1);
        if (levelName !=null && levelName != "")
        {
            SceneManager.LoadScene(levelName);
        }
    }

}
