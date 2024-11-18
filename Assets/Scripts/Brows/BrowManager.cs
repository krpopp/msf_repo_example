using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] allBrownLines;

    GameObject lastBrow;

    float currentDist;

    int browInd = 0;

    public bool finished = false;

    [SerializeField]
    GameObject firstBrow;

    public static float maxDist;

    [SerializeField]
    GameObject browHairIcon;

    [SerializeField]
    Transform detritusObj;

    [SerializeField]
    Animator myAnim;

    enum State
    {
        Closed,
        Open
    }

    State state = State.Closed;

    [SerializeField]
    GameObject eyeOpenObj;


    Vector3 mPos;

    int baseSort;

    WindowControl winControl;

    public AudioClip[] clothAudio;

    // Start is called before the first frame update
    void Start()
    {
        lastBrow = allBrownLines[browInd];
        maxDist = firstBrow.GetComponent<BrowHairObj>().startDist;

        myAnim.SetFloat("closeDist", 10);
        winControl = GetComponentInParent<WindowControl>();
        baseSort = winControl.baseSort;
        UpdateBrowSort(baseSort + 1);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Closed:
                if (!finished && firstBrow.GetComponent<BrowHairObj>().tugging) lastBrow.GetComponent<BrowHairObj>().TugOutBrow();
                break;
            case State.Open:
                mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mPos.z = 0;
                CheckCenterPoint();
                break;
        }
        Debug.Log(winControl.moveToPos);
        if (winControl.moved)
        {
            UpdateBrowSort(winControl.baseSort);
            winControl.moved = false;
        }
    }

    public void UpdateBrowSort(int newBaseOrder)
    {
        foreach(GameObject i in allBrownLines)
        {
            LineRenderer thisLine = i.GetComponent<LineRenderer>();
            thisLine.sortingOrder = newBaseOrder;
        }
        firstBrow.GetComponent<LineRenderer>().sortingOrder = newBaseOrder;
    }

    public void RemoveBrow()
    {
        browInd += 1;
        if(browInd < allBrownLines.Length)
        {
            lastBrow = allBrownLines[browInd];
        } else
        {
            finished = true;
        }

    }

    public void BrowIsLoose()
    {
        GameObject newIcon = Instantiate(browHairIcon, detritusObj);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        newIcon.transform.position = newPos;
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(1).gameObject);
        myAnim.SetTrigger("isOpen");
        state = State.Open;
    }

    private void CheckCenterPoint()
    {
        mPos.z = 0;
        float centerDist = Vector3.Distance(mPos, eyeOpenObj.transform.position);
        myAnim.SetFloat("closeDist", centerDist);
    }



}
