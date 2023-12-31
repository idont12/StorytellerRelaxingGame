using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragSlide : MonoBehaviour, IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField]private Canvas canvas;
    private RectTransform rectTransform;
    public DropSlot slideDrop;
    UiCollider uiCollider = new UiCollider();
    public int objectID;
    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        canvas = GameObject.Find("GameCanvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
      
    }

    public void OnDrag(PointerEventData eventData)
    {
        List<GameObject> objectInSlotList = gameObject.GetComponent<BackgroundManager>().objectInSlotList;
        for (int i=0 ; i< objectInSlotList.Count ; i++)
        {
            if (objectInSlotList[i] != null)
            {
                if (objectInSlotList[i].GetComponent<ItemCanDrag>().isMouseOver)
                {
                    return;
                }
            }
        }
        rectTransform.SetParent(canvas.gameObject.transform);
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject colitionObject = uiCollider.ChackCollider("SlideCollider", gameObject.GetComponent<RectTransform>());
        if (colitionObject!=null)
        {
            colitionObject.GetComponent<DropSlot>().SwitchSlide(gameObject, slideDrop,true,objectID);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetInteger("Animation", -1);
            StartCoroutine(waitToRemoveObject(0.4f, gameObject));
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    /*���� ���� ��������� ��� ������ ���� ����� ���� ������ ����� �� ���� ����� �� ����� �� ��� ����� ��� ���� ��� �� �� �� ���� ����� �� �������*/
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

    /*���� ���� ���� ��� ������*/
    IEnumerator waitToRemoveObject(float waitTime, GameObject ObjectToRemove)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(ObjectToRemove);
        ObjectToRemove = null;
        GameObject.Find("LevelManager").GetComponent<LevelManagerCode>().ChackStoryInDelay();
    }


    //bool isRectOverlap(RectTransform rect1, RectTransform rect2)
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
