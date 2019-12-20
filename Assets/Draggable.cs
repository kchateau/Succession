using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;

    GameObject placeholder = null;
    const string CARD_DROP_AREA = "Card drop area";

    public void OnBeginDrag(PointerEventData eventData) {
        if(this.transform.parent.name == CARD_DROP_AREA) {
            eventData.pointerDrag = null;
            return;
        }

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        //when we start dragging, remove the card from the hand and set it's new parent to be the canvas
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false; //otherwise can't tell when mouse enters a zone
    }

    public void OnDrag(PointerEventData eventData) {
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent) {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++) {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x) {

                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex) {
                    newSiblingIndex--;
                }                   
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData) {
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        DestroyImmediate(placeholder);
        //updateDateVisibility();
        foreach (Transform child in this.transform.parent) {
        }

        var myDate = getMyDate();
        var myLeftDate = getLeftDate();
        var myRightDate = getRightDate();

        checkCardPlacement(myDate, myLeftDate, myRightDate);
        checkWin();

    }

    public void updateDateVisibility() { 
            Transform gameDateTransform = this.transform.Find("date");
            Text gameDateText = gameDateTransform.GetComponent<Text>();
            gameDateText.enabled = true;
    } 

    private System.DateTime? getLeftDate() {
        return getDateFromIndex(this.transform.GetSiblingIndex() - 1);
    }

    private System.DateTime? getRightDate() {
        return getDateFromIndex(this.transform.GetSiblingIndex() + 1);
    }

    private System.DateTime? getMyDate() {
        return getDateFromIndex(this.transform.GetSiblingIndex());
    }

    private System.DateTime? getDateFromIndex(int index) {
        try {
            Transform temp = this.transform.parent.GetChild(index).Find("date");
            Text myDate = temp.GetComponent<Text>();
            return System.Convert.ToDateTime(myDate.text);
        } catch (Exception e) {
            return null;
        }                    
    }

    private void checkCardPlacement(DateTime? myDate, DateTime? myLeftDate, DateTime? myRightDate) {
        if (myLeftDate == null) {
            if (myDate <= myRightDate) {
                onCorrectPlacement();
            }
            else {
                onIncorrectPlacement();
            }
        }
        else if (myRightDate == null) {
            if (myDate >= myLeftDate) {
                onCorrectPlacement();
            }
            else {
                onIncorrectPlacement();
            }
        }
        else {
            if (myDate >= myLeftDate && myDate <= myRightDate) {
                onCorrectPlacement();
            }
            else {
                onIncorrectPlacement();
            }
        }
    }

    private void onCorrectPlacement() {
        updateDateVisibility();
        DealHand.Instance.inc_win();
    }

    private void onIncorrectPlacement() {
        Destroy(this.transform.gameObject);
        DealHand.dealCard("Hand");
    }

    private void checkWin(){
        if(DealHand.Instance.win_counter >= 5){
            Debug.Log("u win");
            SceneManager.LoadScene("win_scene");
        }
    }
}
