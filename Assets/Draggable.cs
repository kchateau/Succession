using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform parentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");

        parentToReturnTo = this.transform.parent;
        //when we start dragging, remove the card from the hand and set it's new parent to be the canvas
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false; //otherwise can't tell when mouse enters a zone
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDragDrag");

        this.transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
