using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{

    GameObject windowParent;

    Vector3 mPos;
    Vector3 lastMPos;

    bool firstClick = false;

    // Start is called before the first frame update
    void Start()
    {
        windowParent = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        FindMPos();
        lastMPos = mPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(firstClick && Input.GetMouseButtonUp(0))
        {
            firstClick = false;
        }
    }

    void FindMPos()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;
    }

    private void OnMouseDrag()
    {
        if (!firstClick)
        {
            lastMPos = mPos;
            firstClick = true;
        }

        FindMPos();
        float dist = Vector3.Distance(lastMPos, mPos);
        if(dist > 0.4f)
        {
            FindMChange(dist);
        }
        //Vector3 currScale = windowParent.transform.localScale;
        //currScale.x += 0.1f;
        //currScale.y += 0.1f;
        //windowParent.transform.localScale = currScale;
    }


    void FindMChange(float dist)
    {
        Vector3 dir = mPos - lastMPos;
        dir = Vector3.Normalize(dir);
        Vector3 currScale = windowParent.transform.localScale;
        float newChange = dist / 2;
        if (dir.x > dir.y)
        {
            currScale.x += newChange;
            currScale.y += newChange;
        } else
        {
            if(currScale.x - newChange > 0 && currScale.y - newChange > 0)
            {
                currScale.x -= newChange;
                currScale.y -= newChange;
            }
        }
        windowParent.transform.localScale = currScale;
        lastMPos = mPos;
        Debug.Log(dir);
    }
}
