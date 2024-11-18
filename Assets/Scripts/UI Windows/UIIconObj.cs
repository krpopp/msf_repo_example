using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIIconObj : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    UIManager uiManager;

    float clickTime = 0;

    [SerializeField]
    float maxClickTime;

    bool wasClickedFirst = false;

    [SerializeField]
    GameObject windowObj;

    GameObject newWindowObj;
    Vector3 newWindowPos;

    bool highlighted = false;

    [SerializeField]
    GameObject highlightObj;

    bool holding = false;

    GameObject followObject, canvasObject;

    Rect myRect;
    Rect trashRect;
    RectTransform myRectTrans;
    RectTransform trashRectTrans;

    [SerializeField]
    Transform trashWindowTransform;

    AudioSource uiSource;
    [SerializeField]
    AudioClip trashClip;

    // Start is called before the first frame update
    void Start()
    {
        newWindowObj = GameObject.Find("NewWindowPos");
        newWindowPos = newWindowObj.transform.position;
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        newWindowPos.z = 0;
        followObject = GameObject.Find("UIFollowObj");
        canvasObject = transform.parent.gameObject;

        myRectTrans = transform.GetComponent<RectTransform>();
        trashRectTrans = GameObject.Find("Bin Icon").transform.GetComponent<RectTransform>();
        myRect = transform.GetComponent<RectTransform>().rect;
        trashRect = GameObject.Find("Bin Icon").transform.GetComponent<RectTransform>().rect;

        uiSource = canvasObject.transform.parent.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wasClickedFirst)
        {
            CountClickTime();
        }
        if(holding && highlighted)
        {
            highlightObj.transform.position = transform.position;
        }
    }

    void CountClickTime()
    {
        clickTime += 1;
        if (clickTime > maxClickTime)
        {
            wasClickedFirst = false;
            clickTime = 0;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.SetParent(canvasObject.transform);
        if (!wasClickedFirst)
        {
            wasClickedFirst = true;
        }
        else
        {
            DoubleClick();
        }
        CheckOverlap();
        holding = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateHighlight();
        transform.SetParent(followObject.transform);
        holding = true;
    }

    void DoubleClick()
    {
        //Vector3 newPos = transform.position;
        //newPos.z = 0;
        //windowObj.transform.position = newPos;
        //windowObj.GetComponent<WindowManager>().StartMoveToNewPos(newWindowPos);
        windowObj.SetActive(true);
        wasClickedFirst = false;
        clickTime = 0;
    }

    void ActivateHighlight()
    {
        highlighted = true;
        UIManager.ActivateIcon(gameObject);
        highlightObj.SetActive(true);
        SetHighlightHierachy();
    }

    void SetHighlightHierachy()
    {
        highlightObj.transform.SetParent(transform);
        highlightObj.transform.SetAsFirstSibling();
        highlightObj.transform.position = transform.position;
    }

    public void DeactivateIcon()
    {
        highlighted = false;
        highlightObj.SetActive(false);
    }


    void CheckOverlap()
    {
        if(!transform.CompareTag("trash"))
        {
            if (myRectTrans.localPosition.x < trashRectTrans.localPosition.x + trashRect.width &&
                myRectTrans.localPosition.x + myRect.width > trashRectTrans.localPosition.x &&
                myRectTrans.localPosition.y < trashRectTrans.localPosition.y + trashRect.height &&
                myRectTrans.localPosition.y + myRect.height > trashRectTrans.localPosition.y)
            {
                TrashMe();
            }
        }
    }

    void TrashMe()
    {
        uiSource.PlayOneShot(trashClip);
        transform.SetParent(trashWindowTransform);
        transform.localPosition = Vector3.zero;
        DeactivateIcon();
    }

}
