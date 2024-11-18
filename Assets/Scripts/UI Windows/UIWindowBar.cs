using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIWindowBar : MonoBehaviour,
    IPointerUpHandler,
    IPointerDownHandler
{

    UIWindowManager myManager;

    void Start()
    {
        myManager = GetComponentInParent<UIWindowManager>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        myManager.ClickBar = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        myManager.ClickBar = true;
    }
}
