using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBar : MonoBehaviour
{

    WindowControl myManager;

    private void Start()
    {
        myManager = GetComponentInParent<WindowControl>();
    }

    private void OnMouseDown()
    {
        myManager.ClickBar = true;
    }

    private void OnMouseUp()
    {
        myManager.ClickBar = false;
    }
}
