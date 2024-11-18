using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static GameObject activeIcon;

    [SerializeField]
    GameObject homeMenu;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hi");
            Canvas.ForceUpdateCanvases();
            //scrollViewObject.GetComponent().verticalNormalizedPosition = 0f;
        }
    }

    public static void ActivateIcon(GameObject newActive)
    {
        IconObj.DeactivateObjHighlight();
        DeactivateIcon();
        activeIcon = newActive;
    }

    public static void DeactivateIcon()
    {
        if (activeIcon != null)
        {
            activeIcon.GetComponent<UIIconObj>().DeactivateIcon();
        }
    }

    public static void CloseWindow(GameObject newWindow)
    {
        newWindow.SetActive(false);
    }

    public void OpenWindowFromMenu(GameObject newWindow)
    {
        newWindow.SetActive(true);
        homeMenu.SetActive(false);
    }

    public static void OpenElement(GameObject elementToOpen)
    {
        if (elementToOpen.activeSelf)
        {
            elementToOpen.SetActive(false);
        }
        else
        {
            elementToOpen.SetActive(true);
        }
    }

    public static void OpenMenu(GameObject newMenu)
    {
        newMenu.SetActive(true);
    }
}
