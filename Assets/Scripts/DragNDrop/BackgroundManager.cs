using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] List<DropSlot> Colliders = new List<DropSlot>();
    public List<GameObject> objectInSlotList = new List<GameObject>();
    UiCollider colliderFuncs = new UiCollider();

    private void Awake()
    {
        for (int i = 0; i < Colliders.Count; i++)
        {
            Colliders[i].SlotPlace = i;
            objectInSlotList[i] = null;
        }

    }

    private void FixedUpdate()
    {
        GameObject CollitionWith = colliderFuncs.ChackCollider("OnDrag", gameObject.GetComponent<RectTransform>());
        print(CollitionWith != null);
        DragCharacterOverBackground(CollitionWith != null);
    }

    /*מעלים את אחת הדמוית מהסצנה, בדרך כלל לאחר גרירה של הדמות למקום אחר*/
    public void removeCharacter(int ObjectInList)
    {
        objectInSlotList[ObjectInList].GetComponent<Animator>().SetInteger("Animation", -1);
        StartCoroutine(waitToRemoveCharacter(getAnimationTime(objectInSlotList[ObjectInList], "Exit"), objectInSlotList[ObjectInList]));
    }

    /*מחזיר כמה זמן אנימציה נמשכת*/
    float getAnimationTime(GameObject AnimatedObject,string AnimationName){
        Animator anim = AnimatedObject.GetComponent<Animator>();
        float time = 1;
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;	//Get Animator controller
        for(int i = 0; i<ac.animationClips.Length; i++)                 //For all animations
        {
            if(ac.animationClips[i].name == AnimationName)            //If it has the same name as your clip
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
    }

    public void DragCharacterOverBackground(bool arrowMode)
    {
        foreach (DropSlot thisSlot in Colliders)
        {
            thisSlot.CharacterArrow.SetBool("isShow", arrowMode);
        }
    }

}
