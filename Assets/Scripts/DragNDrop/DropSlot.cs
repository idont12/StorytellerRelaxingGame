using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour
{

    [SerializeField] Transform objectPosition;
    public GameObject objectInSlot;
    public BackgroundManager backgroundManager;
    public int SlotPlace;
    public Animator CharacterArrow;
    public int ObjectSlotID;

    LevelManagerCode levelManagerCode;

    private void Start()
    {
        levelManagerCode = GameObject.Find("LevelManager").GetComponent<LevelManagerCode>();
    }

    public void UpdateSlot(GameObject newObject, int objectID, Sprite backSprite)
    {
        if (gameObject.tag == "CharacterSlotCollider")
        {
            updateCharacter(newObject, objectID);
        }
        else if (gameObject.tag == "SlideCollider")
        {
            updateBackground(newObject, objectID, backSprite);
        }
    }

    /*מחזיר כמה זמן אנימציה נמשכת*/
    float getAnimationTime(GameObject AnimatedObject, string AnimationName)
    {
        Animator anim = AnimatedObject.GetComponent<Animator>();
        float time = 1;
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;	//Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
        {
            if (ac.animationClips[i].name == AnimationName)            //If it has the same name as your clip
            {
                time = ac.animationClips[i].length;
                return time;
            }
        }
        return time;
    }

    /*מוחק דמות לאחר זמן מסויים*/
    IEnumerator waitToRemoveCharacter(float waitTime, GameObject ObjectToRemove)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(ObjectToRemove);
        ObjectToRemove = null;
        levelManagerCode.ChackStoryInDelay();
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
                StartCoroutine(waitToRemoveCharacter(getAnimationTime(objectInSlot, "Exit"), objectInSlot));
            }
        }
        Vector3 newDropPosition = new Vector3(objectPosition.position.x, objectPosition.position.y + (newObject.GetComponent<RectTransform>().sizeDelta.y / 2), objectPosition.position.z - 0.1f);
        objectInSlot = Instantiate(newObject, newDropPosition, Quaternion.identity, objectPosition);
        objectInSlot.GetComponent<ItemCanDrag>().canvas = GameObject.Find("GameCanvas").GetComponent<Canvas>();
        objectInSlot.GetComponent<ItemCanDrag>().slideObject = backgroundManager.gameObject;
        Color originalColor = objectInSlot.GetComponent<UnityEngine.UI.Image>().color;
        objectInSlot.GetComponent<UnityEngine.UI.Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        backgroundManager.objectInSlotList[SlotPlace] = objectInSlot;
        ObjectSlotID = objectID;
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
        levelManagerCode.ChackStory();
    }

    void updateBackground(GameObject newObject, int objectID,Sprite backSprite)
    {
        if (objectInSlot != null)
        {
            Destroy(objectInSlot);
        }

        Vector3 newDropPosition = new Vector3(objectPosition.position.x, objectPosition.position.y, objectPosition.position.z - 2f);
        objectInSlot = Instantiate(newObject, newDropPosition, Quaternion.identity, objectPosition);
        backgroundManager = objectInSlot.GetComponent<BackgroundManager>();
        objectInSlot.GetComponent<DragSlide>().slideDrop = this;
        objectInSlot.GetComponent<DragSlide>().objectID = objectID;
        ObjectSlotID = objectID;

        if (backSprite != null)
        {
            print(backSprite.name);
            objectInSlot.GetComponent<UnityEngine.UI.Image>().sprite = backSprite;
        }

        //print("ObjectSlotID.ToString()[0]" + ObjectSlotID.ToString()[0]);
        //if (ObjectSlotID.ToString()[0] == '2')
        //{
        //    Sprite backSprite = GameObject.Find("LevelManager").GetComponent<LevelManagerCode>().generalInfo.getSpriteByID(ObjectSlotID);
        //    if (backSprite != null)
        //    {
        //        objectInSlot.GetComponent<UnityEngine.UIElements.Image>().sprite = backSprite;
        //    }

        //}
    }

    public void SwitchSlide(GameObject SwitchObject, DropSlot OriginalPerent,bool willSwich,int objectID)
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
                OriginalPerent.SwitchSlide(objectInSlot, OriginalPerent, false, ObjectSlotID);
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
        ObjectSlotID = objectID;
        backgroundManager = SwitchObject.GetComponent<BackgroundManager>();
        SwitchObject.GetComponent<DragSlide>().slideDrop = this;

        if (willSwich == false)
        {
            levelManagerCode.ChackStoryInDelay();
        }
    }

    public void SwitchCharacter(GameObject SwitchObject, DropSlot OriginalPerent, bool willSwich, int objectID)
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
                OriginalPerent.SwitchSlide(objectInSlot, OriginalPerent, false, ObjectSlotID);
            }
        }

        float speed = 0.2f;
        if (willSwich == false)
        {
            speed = 0.3f;
        }

        SwitchObject.transform.SetParent(objectPosition);
        LeanTween.moveLocal(SwitchObject, Vector3.zero, speed).setEase(LeanTweenType.easeOutCubic);
        objectInSlot = SwitchObject;
        ObjectSlotID = objectID;
        backgroundManager = SwitchObject.GetComponent<BackgroundManager>();
        SwitchObject.GetComponent<DragSlide>().slideDrop = this;

        if (willSwich == false)
        {
            levelManagerCode.ChackStoryInDelay();
        }
    }

}
