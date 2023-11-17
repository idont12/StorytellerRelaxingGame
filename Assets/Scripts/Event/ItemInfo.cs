using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Storyteller/ItemInfo", fileName = "ItemInfo")]
public class ItemInfo : ScriptableObject
{
    public string objectName;
    public ObjectType objectType;
    public Sprite DragItemSprite;
    public int objectID;
    public ObjectName newObject;
    public GameObject dragObject;
    public GameObject dropObject;
    public GameEvent GrabSound;

    private void OnEnable()
    {
        objectID = ObjectToID(newObject);
    }

    public int ObjectToID(ObjectName objectName)
    {
        int returnVale = 0;

        if (objectName == ObjectName.Ch_Hedgehog)
        {
            returnVale = 11;
        }
        else if (objectName == ObjectName.Ch_Bear)
        {
            returnVale = 12;
        }
        else if (objectName == ObjectName.Ch_Giraffe)
        {
            returnVale = 13;
        }
        else if (objectName == ObjectName.Ch_Alligator)
        {
            returnVale = 14;
        }
        else if (objectName == ObjectName.Ch_Panda)
        {
            returnVale = 15;
        }
        else if (objectName == ObjectName.Ch_Rhinoceros)
        {
            returnVale = 16;
        }
        else if (objectName == ObjectName.Bg_Rain)
        {
            returnVale = 21;
        }
        else if (objectName == ObjectName.Bg_Street)
        {
            returnVale = 22;
        }
        else if (objectName == ObjectName.Bg_TreePark)
        {
            returnVale = 23;
        }
        else if (objectName == ObjectName.Bg_Livingroom)
        {
            returnVale = 24;
        }
        else if (objectName == ObjectName.Bg_Music)
        {
            returnVale = 25;
        }
        else if (objectName == ObjectName.Bg_LivingroomStorm)
        {
            returnVale = 26;
        }
        else if (objectName == ObjectName.Bg_BedroomSingle)
        {
            returnVale = 27;
        }
        else if (objectName == ObjectName.Bg_BedroomDouble)
        {
            returnVale = 28;
        }
        else if (objectName == ObjectName.Bg_Holeway)
        {
            returnVale = 29;
        }



        return returnVale;
    }
}
public enum ObjectName { Ch_Alligator, Ch_Bear, Ch_Giraffe, Ch_Hedgehog, Ch_Panda, Ch_Rhinoceros , Bg_Street, Bg_Rain, Bg_TreePark, Bg_Livingroom, Bg_Music, Bg_LivingroomStorm, Bg_BedroomSingle, Bg_BedroomDouble, Bg_Holeway };
