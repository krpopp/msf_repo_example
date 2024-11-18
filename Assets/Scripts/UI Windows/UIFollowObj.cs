using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObj : MonoBehaviour
{
    Vector3 mPos;

    GameObject childObj;

    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mPos.x, mPos.y, transform.position.z);
    }

    public void SetObj(GameObject _childObj)
    {
        childObj = _childObj;
    }

    public void UnSetObj()
    {
        childObj = null;
    }
}
