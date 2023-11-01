using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class ItemCanDrag : MonoBehaviour, IPointerDownHandler
{
    enum ObjectType { Background, Character }
    [SerializeField] ObjectType objectType;
    [SerializeField] Sprite DragItemSprite;
    [SerializeField] int objectID;
    public Canvas canvas;
    [SerializeField] GameObject dragObject;
    [SerializeField] GameObject dropObject;
    [SerializeField] bool isStageCharacter = false;
    public GameObject slideObject;
    public bool isMouseOver = false;

    private void Start()
    {
        if (DragItemSprite == null)
        {
            DragItemSprite = GetComponent<UnityEngine.UI.Image>().sprite;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isStageCharacter)
        {
            Color originalColor = gameObject.GetComponent<UnityEngine.UI.Image>().color;
            gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        }
        print("PressDown");
        GameObject newDrag = Instantiate(dragObject, transform.position, Quaternion.identity, canvas.gameObject.transform);
        newDrag.GetComponent<UnityEngine.UI.Image>().sprite = DragItemSprite;
        newDrag.GetComponent<DragGeneral>().dropObject = dropObject;
        newDrag.GetComponent<DragGeneral>().objectType = objectType.ToString();
        newDrag.GetComponent<DragGeneral>().objectID = objectID;
        if (isStageCharacter)
        {
            newDrag.GetComponent<DragGeneral>().isStageCharacter = isStageCharacter;
            newDrag.GetComponent<DragGeneral>().slideObject = slideObject;
            newDrag.GetComponent<DragGeneral>().characterInSlide = slideObject.GetComponent<BackgroundManager>().objectInSlotList.IndexOf(gameObject);
        }
    }

    public void changeMouseOver(bool newState)
    {
        isMouseOver=newState;
    }
}
