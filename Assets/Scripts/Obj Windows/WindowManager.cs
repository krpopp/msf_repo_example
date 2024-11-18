using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static List<GameObject> openWindows = new List<GameObject>();

    public static void AddToOpenWindows(GameObject addedObj)
    {
        openWindows.Add(addedObj);
        UpdateListOrder();
    }

    public static void RemoveFromOpenWindows(GameObject removedObj)
    {
        openWindows.Remove(removedObj);
        UpdateListOrder();
    }

    static void UpdateListOrder()
    {
        for(int i = 0; i < openWindows.Count; i++)
        {
            openWindows[i].GetComponent<WindowControl>().baseSort = (i * 10) + 5;
        }
        UpdateAllSprites();
    }

    public static void SendToEndUpdateList(GameObject newEndObj)
    {
        openWindows.Remove(newEndObj);
        openWindows.Add(newEndObj);
        UpdateListOrder();
    }

    static void UpdateAllSprites()
    {
        foreach(var i in openWindows)
        {
            i.GetComponent<WindowControl>().DoNotUpdateSprites();
        }
    }
}
