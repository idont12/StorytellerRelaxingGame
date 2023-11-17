using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCurser : MonoBehaviour
{
    public bool isMouseCollid = false;
    UiCollider uiCollider = new UiCollider();

    private void Update()
    {
        gameObject.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x+10f, Input.mousePosition.y + 10f, Input.mousePosition.z);
        GameObject mouseCollid = uiCollider.ChackCollider("CanDrag", gameObject.GetComponent<RectTransform>());
        isMouseCollid = mouseCollid != null && mouseCollid.GetComponent<ItemCanDrag>().isStageCharacter;
    }
}
