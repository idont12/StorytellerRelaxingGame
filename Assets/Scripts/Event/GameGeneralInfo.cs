using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Storyteller/GeneralInfo", fileName = "GeneralInfo")]

public class GameGeneralInfo : ScriptableObject
{
    public List<SceneAsset> LevelOrder = new List<SceneAsset>();
    public SceneAsset AfterFinalLevel;

    public string getLevelbyOrderNum(int currentLevelNum)
    {
        if (currentLevelNum < LevelOrder.Count)
        {
            return LevelOrder[currentLevelNum].name;
        }
        else
        {
            return AfterFinalLevel.name;
        }
    }

    public string getNextLevelbyName(string currentLevleName)
    {
        int cuttentLevelNum = GetLevelInOrder(currentLevleName);
        if (cuttentLevelNum>-1)
        {
           return getLevelbyOrderNum(cuttentLevelNum + 1);
        }
        return null;
    }

    public int GetLevelInOrder(string levleName)
    {
        for (int i=0;i< LevelOrder.Count;i++)
        {
            SceneAsset level = LevelOrder[i];
            if (level.name == levleName)
            {
                return i;
            }
        }
        return -1;
    }
}
