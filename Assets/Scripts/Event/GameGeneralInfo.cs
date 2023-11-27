using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Storyteller/GeneralInfo", fileName = "GeneralInfo")]

public class GameGeneralInfo : ScriptableObject
{
    //public List<UnityEditor.SceneAsset> LevelOrder = new List<UnityEditor.SceneAsset>();
    //public SceneAsset AfterFinalLevel;
    [Header("Level Order")]
    public List<string> LevelOrder;
    public string AfterFinalLevel;
    public string getLevelbyOrderNum(int currentLevelNum)
    {
        if (currentLevelNum < LevelOrder.Count)
        {
            return LevelOrder[currentLevelNum];
        }
        else
        {
            return AfterFinalLevel;
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
        return LevelOrder.IndexOf(levleName);
        //for (int i=0;i< LevelOrder.Count;i++)
        //{
        //    SceneAsset level = LevelOrder[i];
        //    if (level.name == levleName)
        //    {
        //        return i;
        //    }
        //}
        //return -1;
    }

    [Header("Background Sprite")]

    public List<backSpriteByObjectName> backgroundManeger = new List<backSpriteByObjectName>();

    [System.Serializable]
    public class backSpriteByObjectName
    {
        public ObjectName thisObject;
        public Sprite thisSprite;
    }

    public Sprite getSpriteByID(int objectId)
    {
        Console.WriteLine("objectId" + objectId.ToString());
        ItemInfo itemInfo = new ItemInfo();
        foreach (backSpriteByObjectName backSertch in backgroundManeger)
        {
            Console.WriteLine("itemInfo.ObjectToID(backSertch.thisObject)" + itemInfo.ObjectToID(backSertch.thisObject).ToString());
            if (itemInfo.ObjectToID(backSertch.thisObject) == objectId)
            {
                return backSertch.thisSprite;
            }
        }
        return null;
    }

    public Sprite getSpriteByObjectName(ObjectName objectName)
    {
        foreach (backSpriteByObjectName backSertch in backgroundManeger)
        {
            if (backSertch.thisObject == objectName)
            {
                return backSertch.thisSprite;
            }
        }
        return null;
    }

}
