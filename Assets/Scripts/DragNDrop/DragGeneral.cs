using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragGeneral : MonoBehaviour
{

    public Canvas canvas;
    public GameObject dropObject;
    public string objectType;
    private RectTransform RectTransform;
    private string CollitionType;
    public int objectID;

    public bool isStageCharacter;
    public int characterInSlide;
    public GameObject slideObject;
    public int OriginalSlotId;
    public Sprite backSprite;

    UiCollider uiCollider = new UiCollider();
    private void Start()
    {
        RectTransform = GetComponent<RectTransform>();

        if (objectType == "Background")
        {
            CollitionType = "SlideCollider";
        }
        else if (objectType == "Character")
        {
            CollitionType = "CharacterSlotCollider";
            gameObject.tag = "OnDragCharacter";
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            RectTransform.position = Input.mousePosition;
        }
        else
        {
            GameObject CollitionWith = uiCollider.ChackCollider(CollitionType, gameObject.GetComponent<RectTransform>());


            if (isStageCharacter)
            {
                try
                {
                    /*תנאי שאומר מספר דברים הראשון, תבדוק שקיים אובייקט בקוליידר בו התנגשנו אחר כך שסוג הדמות זו אותה דמות שאנחנו גוררים ולבסוף שהסלייד זה אותו סלייד ממנו לקחנו את האובייקט*/
                    print(" CollitionWith.transform.parent.parent.parent.Find().GetComponent<DropSlot>().SlotPlace" + CollitionWith.transform.parent.parent.parent.Find("Collider").GetComponent<DropSlot>().SlotPlace.ToString());
                    if (CollitionWith.GetComponent<DropSlot>().objectInSlot != null && CollitionWith.GetComponent<DropSlot>().ObjectSlotID == objectID && CollitionWith.transform.parent.parent.parent.Find("Collider").GetComponent<DropSlot>().SlotPlace == OriginalSlotId)
                    {
                        GameObject objectInSlot = CollitionWith.GetComponent<DropSlot>().objectInSlot;
                        Color originalColor = objectInSlot.GetComponent<UnityEngine.UI.Image>().color;
                        objectInSlot.GetComponent<UnityEngine.UI.Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
                        Destroy(gameObject);
                        return;
                    }
                    else
                    {
                        slideObject.GetComponent<BackgroundManager>().removeCharacter(characterInSlide);
                    }
                }
                catch
                {
                    slideObject.GetComponent<BackgroundManager>().removeCharacter(characterInSlide);
                }

            }

            if (CollitionWith != null)
            {
                CollitionWith.GetComponent<DropSlot>().UpdateSlot(dropObject, objectID, backSprite);
            }
            Destroy(gameObject);
            
        }
    }
    
    //bool isRectOverlap (RectTransform rect1, RectTransform rect2)
    //{
    //    Rect newRect1 = RectTransformToScreenSpace(rect1);
    //    Rect newRect2 = RectTransformToScreenSpace(rect2);
    //    return newRect1.Overlaps(newRect2);
    //} 

    //Rect RectTransformToScreenSpace(RectTransform rectTransform)
    //{
    //    Vector3[] corners = new Vector3[4];
    //    rectTransform.GetWorldCorners(corners);

    //    float minX = float.MaxValue;
    //    float maxX = float.MinValue;
    //    float minY = float.MaxValue;
    //    float maxY = float.MinValue;

    //    for (int i = 0; i < 4; i++)
    //    {
    //        minX = Mathf.Min(minX, corners[i].x);
    //        maxX = Mathf.Max(maxX, corners[i].x);
    //        minY = Mathf.Min(minY, corners[i].y);
    //        maxY = Mathf.Max(maxY, corners[i].y);
    //    }

    //    return new Rect(minX, minY, maxX - minX, maxY - minY);
    //}

    //GameObject ChackCollider(string TagCollider, RectTransform collisionZoon)
    //{
    //    GameObject[] AllColliders = GameObject.FindGameObjectsWithTag(TagCollider);
    //    foreach (GameObject collider in AllColliders)
    //    {
    //        if (isRectOverlap(collisionZoon, collider.GetComponent<RectTransform>()))
    //        {
    //            return collider;
    //        }
    //    }
    //    return null;
    //}
}

public class UiCollider
{
    public bool isRectOverlap(RectTransform rect1, RectTransform rect2)
    {
        Rect newRect1 = RectTransformToScreenSpace(rect1);
        Rect newRect2 = RectTransformToScreenSpace(rect2);
        return newRect1.Overlaps(newRect2);
    }

    public Rect RectTransformToScreenSpace(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        for (int i = 0; i < 4; i++)
        {
            minX = Mathf.Min(minX, corners[i].x);
            maxX = Mathf.Max(maxX, corners[i].x);
            minY = Mathf.Min(minY, corners[i].y);
            maxY = Mathf.Max(maxY, corners[i].y);
        }

        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }

    public GameObject ChackCollider(string TagCollider, RectTransform collisionZoon)
    {
        GameObject[] AllColliders = GameObject.FindGameObjectsWithTag(TagCollider);
        foreach (GameObject collider in AllColliders)
        {
            if (isRectOverlap(collisionZoon, collider.GetComponent<RectTransform>()))
            {
                return collider;
            }
        }
        return null;
    }
}