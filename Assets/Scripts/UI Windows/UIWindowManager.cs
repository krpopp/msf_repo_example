using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{

    private bool clickBar;

    public bool ClickBar
    {
        get
        {
            return clickBar;
        }
        set
        {
            if (value)
            {
                transform.SetParent(followObject.transform);
                followObject.GetComponent<UIFollowObj>().SetObj(gameObject);
            }
            else
            {
                followObject.GetComponent<UIFollowObj>().UnSetObj();
                transform.SetParent(canvasObj.transform);
            }
            clickBar = value;
        }
    }

    [SerializeField]
    GameObject followObject;

    GameObject canvasObj;

    bool moveToPos = false;

    void Start()
    {
        canvasObj = transform.parent.gameObject;
        followObject = GameObject.Find("UIFollowObj");
    }
}