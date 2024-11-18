using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconObj : MonoBehaviour
{

    [SerializeField]
    AudioClip dropClip;

    AudioSource mySource;

    GameObject followObject;

    bool holding;

    public static GameObject selectObj;

  

    // Start is called before the first frame update
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        mySource.PlayOneShot(dropClip);
        followObject = GameObject.Find("FollowObject");
        if(selectObj == null)
        {
            selectObj = GameObject.Find("o");
            if (selectObj.activeSelf)
            {
                selectObj.SetActive(false);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(holding)
        {
            if(Input.GetMouseButtonUp(0))
            {
                transform.SetParent(null);
                holding = false;
            }
        }
    }

    private void OnMouseDown()
    {
        transform.SetParent(followObject.transform);
        holding = true;  
        UIManager.DeactivateIcon();
        ActivateObjHighlight();
    }

    void ActivateObjHighlight()
    {
        selectObj.SetActive(true);
        selectObj.transform.SetParent(transform);
        selectObj.transform.localPosition = Vector3.zero;
    }

    public static void DeactivateObjHighlight()
    {
        if(selectObj != null)
        {
            selectObj.SetActive(false);
        }
    }

}
