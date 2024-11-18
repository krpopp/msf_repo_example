using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExObj : MonoBehaviour
{
    WindowControl myManager;

    void Start()
    {
        myManager = GetComponentInParent<WindowControl>();
    }

    private void OnMouseDown()
    {
        myManager.TurnOff();
    }

    public void ExButtonPress()
    {
        myManager.TurnOff();
    }
}
