using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class ItemCanDrag : MonoBehaviour, IPointerDownHandler
{
    public ItemInfo ItemInfo;
    public bool CanDrag = true;
    void UpdateByItemInfo(ItemInfo item)
    {
        objectType = item.objectType;
        if (item.DragItemSprite != null)
        {
            DragItemSprite = item.DragItemSprite;
        }
        if (item.objectID != 0)
        {
            objectID = item.objectID;
        }
        if (item.dragObject != null)
        {
            dragObject = item.dragObject;
        }
        if (item.dropObject != null)
        {
            dropObject = item.dropObject;
        }
        if (item.GrabSound != null)
        {
            GrabSound = item.GrabSound;
        }
        if (item.objectName != "" && NameDisplay != null && isStageCharacter == false)
        {
            NameDisplay.text = item.objectName;
        }
        if (item.backgroundSprite!=null && objectType==ObjectType.Background)
        {
            backgroundSprite = item.backgroundSprite;
        }
       
    }

    //enum ObjectType { Background, Character }
    [SerializeField] TMP_Text NameDisplay;
    [SerializeField] ObjectType objectType;
    [SerializeField] Sprite DragItemSprite;
    //[HideInInspector]
    public int objectID;
    //[HideInInspector]
    public Canvas canvas;
    [SerializeField] GameObject dragObject;
    [SerializeField] GameObject dropObject;
    public bool isStageCharacter = false;
    public GameObject slideObject;

    [SerializeField] GameEvent GrabSound;

    public bool isMouseOver = false;
    [SerializeField] Sprite backgroundSprite;

    private void Start()
    {
        if (ItemInfo!=null)
        {
            UpdateByItemInfo(ItemInfo);
 
        }

        if (DragItemSprite != null && isStageCharacter==false)
        {
            GetComponent<UnityEngine.UI.Image>().sprite = DragItemSprite;
        }
        if (canvas==null)
        {
            canvas = GameObject.Find("GameCanvas").GetComponent<Canvas>();
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (isStageCharacter== false && CanDrag)
        {
            if (GrabSound!=null)
            {
                GrabSound.Raise();
            }
            
            if (isStageCharacter)
            {
                Color originalColor = gameObject.GetComponent<UnityEngine.UI.Image>().color;
                gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
            }
            GameObject newDrag = Instantiate(dragObject, transform.position, Quaternion.identity, canvas.gameObject.transform);
            newDrag.GetComponent<UnityEngine.UI.Image>().sprite = DragItemSprite;
            newDrag.GetComponent<DragGeneral>().dropObject = dropObject;
            newDrag.GetComponent<DragGeneral>().objectType = objectType.ToString();
            newDrag.GetComponent<DragGeneral>().objectID = objectID;
            newDrag.GetComponent<DragGeneral>().backSprite = backgroundSprite;
            if (isStageCharacter)
            {
                newDrag.GetComponent<DragGeneral>().isStageCharacter = isStageCharacter;
                newDrag.GetComponent<DragGeneral>().slideObject = slideObject;
                newDrag.GetComponent<DragGeneral>().characterInSlide = slideObject.GetComponent<BackgroundManager>().objectInSlotList.IndexOf(gameObject);
                newDrag.GetComponent<DragGeneral>().OriginalSlotId = slideObject.transform.parent.parent.Find("Collider").GetComponent<DropSlot>().SlotPlace;
            }
        }
      
    }

    void click()
    {
        GrabSound.Raise();
        if (isStageCharacter)
        {
            Color originalColor = gameObject.GetComponent<UnityEngine.UI.Image>().color;
            gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        }
        GameObject newDrag = Instantiate(dragObject, transform.position, Quaternion.identity, canvas.gameObject.transform);
        newDrag.GetComponent<UnityEngine.UI.Image>().sprite = DragItemSprite;
        newDrag.GetComponent<DragGeneral>().dropObject = dropObject;
        newDrag.GetComponent<DragGeneral>().objectType = objectType.ToString();
        newDrag.GetComponent<DragGeneral>().objectID = objectID;
        newDrag.GetComponent<DragGeneral>().backSprite = backgroundSprite;
        if (isStageCharacter)
        {
            newDrag.GetComponent<DragGeneral>().isStageCharacter = isStageCharacter;
            newDrag.GetComponent<DragGeneral>().slideObject = slideObject;
            newDrag.GetComponent<DragGeneral>().characterInSlide = slideObject.GetComponent<BackgroundManager>().objectInSlotList.IndexOf(gameObject);
            newDrag.GetComponent<DragGeneral>().OriginalSlotId = slideObject.transform.parent.parent.Find("Collider").GetComponent<DropSlot>().SlotPlace;
        }
    }

    UiCollider uiCollider = new UiCollider();

    private void Update()
    {
        GameObject mouseCollid = uiCollider.ChackCollider("Mouse", gameObject.GetComponent<RectTransform>());
        isMouseOver = mouseCollid != null && isStageCharacter;

        if (Input.GetMouseButtonDown(0) && isMouseOver && CanDrag)
        {
            click();
        }
        
        if (GameObject.Find("LevelManager").GetComponent<LevelManagerCode>().canPlay != CanDrag)
        {
            CanDrag = GameObject.Find("LevelManager").GetComponent<LevelManagerCode>().canPlay;
        }
    }

    public void changeMouseOver(bool newState)
    {
        isMouseOver = newState;
    }
}
