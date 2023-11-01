using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] List<DropSlot> Colliders = new List<DropSlot>();
    public List<GameObject> objectInSlotList = new List<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < Colliders.Count; i++)
        {
            Colliders[i].SlotPlace = i;
            objectInSlotList[i] = null;
        }

    }

    public void removeCharacter(int ObjectInList)
    {
        Destroy(objectInSlotList[ObjectInList]);
        objectInSlotList[ObjectInList] = null;
    }

    public void updateBackgroundSlide()
    {

    }
}
