using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteManager : MonoBehaviour
{

    [SerializeField]
    Texture2D openHand, closedHand;

    [SerializeField]
    CursorMode cursorMode = CursorMode.Auto;
    [SerializeField]
    Vector2 hotSpot = Vector2.zero;

    public enum Phase
    {
        Oil,
        Water
    }

    public Phase phase;

    [SerializeField]
    GameObject oilObj, waterObj;

    private void Start()
    {
        switch(phase)
        {
            case Phase.Oil:
                oilObj.SetActive(true);
                waterObj.SetActive(false);
                break;
            case Phase.Water:
                oilObj.SetActive(false);
                waterObj.SetActive(true);
                SetCursor();
                break;
        }
    }

    public void SetCursor()
    {
        Cursor.SetCursor(openHand, hotSpot, cursorMode);
        phase = Phase.Water;
    }

    private void Update()
    {
        switch(phase)
        {
            case Phase.Water:
                if(Input.GetMouseButtonDown(0))
                {
                    Cursor.SetCursor(closedHand, hotSpot, cursorMode);
                }
                if(Input.GetMouseButtonUp(0))
                {
                    Cursor.SetCursor(openHand, hotSpot, cursorMode);
                }
                break;
        }
    }

}
