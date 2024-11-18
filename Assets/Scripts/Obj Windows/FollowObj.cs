using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{

    Vector3 mPos;
    GameObject childObj;

    delegate void MoveDel();
    MoveDel onMove = null;

    void Update()
    {

        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mPos.x, mPos.y, transform.position.z);
        if (childObj != null)
        {
            onMove();
        }
    }

    public void SetObj(GameObject _childObj)
    {
        childObj = _childObj;
        onMove = _childObj.GetComponentInChildren<BodyScene>().OnMoveWindow;
    }

    public void UnSetObj()
    {
        childObj = null;
        onMove = null;
    }
}
