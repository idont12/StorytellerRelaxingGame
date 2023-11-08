using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Storyteller/ItemInfo", fileName = "ItemInfo")]
public class ItemInfo : ScriptableObject
{
    
    public ObjectType objectType;
    public Sprite DragItemSprite;
    public int objectID;
    public ObjectName newObject;
    public GameObject dragObject;
    public GameObject dropObject;
    public bool isStageCharacter;
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
        else if (objectName == ObjectName.Bg_Street)
        {
            returnVale = 22;
        }
        else if (objectName == ObjectName.Bg_Rain)
        {
            returnVale = 21;
        }

        return returnVale;
    }
}
public enum ObjectName { Ch_Alligator, Ch_Bear, Ch_Giraffe, Ch_Hedgehog, Ch_Panda, Ch_Rhinoceros , Bg_Street, Bg_Rain };
