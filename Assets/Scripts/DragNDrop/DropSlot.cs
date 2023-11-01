using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropSlot : MonoBehaviour
{

    [SerializeField] Transform objectPosition;
    public GameObject objectInSlot;
    public BackgroundManager backgroundManager;
    public int SlotPlace;

    public void UpdateSlot(GameObject newObject, int objectID)
    {
        if (gameObject.tag == "CharacterSlotCollider")
        {
            updateCharacter(newObject, objectID);
        }
        else if (gameObject.tag == "SlideCollider")
        {
            updateBackground(newObject);
        }
    }

    void updateCharacter(GameObject newObject, int objectID)
    {
        if (objectInSlot != null)
        {
            if (objectInSlot.GetComponent<CharacterLogic>().ID == objectID)
            {
                return;      
            }
            else
            {
                Destroy(objectInSlot);
            }
        }

        

        Vector3 newDropPosition = new Vector3(objectPosition.position.x, objectPosition.position.y, objectPosition.position.z - 0.1f);
        objectInSlot = Instantiate(newObject, newDropPosition, Quaternion.identity, objectPosition);
        objectInSlot.GetComponent<ItemCanDrag>().canvas = GameObject.Find("GameCanvas").GetComponent<Canvas>();
        objectInSlot.GetComponent<ItemCanDrag>().slideObject = backgroundManager.gameObject;

        Color originalColor = objectInSlot.GetComponent<UnityEngine.UI.Image>().color;
        objectInSlot.GetComponent<UnityEngine.UI.Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);


        backgroundManager.objectInSlotList[SlotPlace] = objectInSlot;

        if (backgroundManager.objectInSlotList.Count > 1)
        {
            int i = 0;
            if (SlotPlace == 0)
            {
                i = 1;
            }

            GameObject otherSlot = backgroundManager.objectInSlotList[i];
            if (otherSlot != null)
            {
                if (otherSlot.GetComponent<CharacterLogic>().ID == objectID)
                {
                    backgroundManager.removeCharacter(i);
                }
            }

        }

        //if (objectInSlot.GetComponent<CharacterLogic>().enabled == false)
        //{
        //    objectInSlot.GetComponent<CharacterLogic>().enabled = true;
        //}
        //if (objectInSlot.GetComponent<UnityEngine.UI.Image>().enabled == false)
        //{
        //    objectInSlot .GetComponent<UnityEngine.UI.Image>().enabled = true;
        //}
    }

    void updateBackground(GameObject newObject)
    {
        if (objectInSlot != null)
        {
            Destroy(objectInSlot);
        }

        Vector3 newDropPosition = new Vector3(objectPosition.position.x, objectPosition.position.y, objectPosition.position.z - 2f);
        objectInSlot = Instantiate(newObject, newDropPosition, Quaternion.identity, objectPosition);
        backgroundManager = objectInSlot.GetComponent<BackgroundManager>();
        objectInSlot.GetComponent<DragSlide>().slideDrop = this;
    }

    public void SwitchSlide(GameObject SwitchObject, DropSlot OriginalPerent,bool willSwich)
    {
        if (willSwich)
        {
            if (objectInSlot == null)
            {
                OriginalPerent.backgroundManager = null;
                OriginalPerent.objectInSlot = null;
            }
            else
            {
                OriginalPerent.SwitchSlide(objectInSlot, OriginalPerent, false);
            }
        }

        float speed = 0.2f;
        if (willSwich==false)
        {
            speed = 0.3f;
        }

        SwitchObject.transform.SetParent(objectPosition);
        LeanTween.moveLocal(SwitchObject, Vector3.zero, speed).setEase(LeanTweenType.easeOutCubic);
        objectInSlot = SwitchObject;
        backgroundManager = SwitchObject.GetComponent<BackgroundManager>();
        SwitchObject.GetComponent<DragSlide>().slideDrop = this;
    }

}
