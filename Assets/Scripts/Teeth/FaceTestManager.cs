using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceTestManager : BodyScene
{

    //[SerializeField]
    //SpriteRenderer spriteRend;

    [SerializeField]
    Sprite[] openTopSprites, openBottomSprites;

    public int openIndex = 0;

    [SerializeField]
    GameObject[] allMouthPoints;

    List<Vector3> allMouthVectors = new List<Vector3>();

    Vector3 mPos;

    [SerializeField]
    float centerOpenDist;

    int mouthOpenPoint = 20;
    public bool mouthOpen = false;

    [SerializeField]
    SpriteRenderer topRenderer, bottomRenderer;

    WindowControl myWinManager;

    [SerializeField]
    List<GameObject> allTeef = new List<GameObject>();

    [SerializeField]
    GameObject tongueObj;

    int minFrame, maxFrame;

    enum State
    {
        Pulling,
        Tonguing
    }

    State state = State.Pulling;

    public AudioClip[] tapAudio;
    public AudioClip[] ripAudio;

    public static bool holdingTooth = false;

    AudioSource mySource;
    [SerializeField]
    AudioClip ahhClip;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(allMouthPoints.Length);
        for (var i = 0; i < allMouthPoints.Length - 1; i++)
        {
            allMouthVectors.Add(allMouthPoints[i].transform.localPosition);
        }
        myWinManager = GetComponentInParent<WindowControl>();

        for(int i = 0; i < allTeef.Count; i++)
        {
            if(allTeef[i].activeSelf == false)
            {
                allTeef.Remove(allTeef[i]);
            }
        }
        minFrame = 0;
        maxFrame = openTopSprites.Length - 1;
        ready = true;
        mySource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Pulling:
                if (ready && myWinManager.inWindow)
                {
                    mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mPos.z = 0;
                    //CheckClosestPoint();
                    if(!holdingTooth && allTeef.Count == 8)
                    {
                        CheckCenterPoint();
                    } else
                    {
                        openIndex = 0;
                        SetFaceAnim(openTopSprites, openBottomSprites, openIndex);
                    }
                }
                break;
            case State.Tonguing:
                break;
        }
    }

    private void CheckClosestPoint()
    {
        float closestDist = 100f;
        for(var i = 0; i < allMouthVectors.Count; i++)
        {
            float thisDist = Vector3.Distance(mPos, allMouthVectors[i]);
            if(thisDist < closestDist)
            {
                closestDist = thisDist;
            }

        }
    }

    private void CheckCenterPoint()
    {
        mPos.z = 0;
        float centerDist = Vector3.Distance(mPos, allMouthPoints[0].transform.position);
        if(centerDist < centerOpenDist)
        {
            openIndex = FindNewIndex(centerDist, centerOpenDist, openTopSprites.Length - 1);
            //openIndex = centerDist;
            SetFaceAnim(openTopSprites, openBottomSprites, openIndex);
            if(openIndex < 3)
            {
                //if(!mouthOpen)
                //{
                //    mySource.PlayOneShot(ahhClip);
                //}
                mouthOpen = true;
            } else
            {
                //if(mouthOpen)
                //{
                //    mySource.Stop();
                //}
                mouthOpen = false;
            }
        }

    }

    private int FindNewIndex(float currentDist, float maxDist, int frameCount)
    {
        float num = maxDist / frameCount;
        float nextFrame = currentDist / num;
        //nextFrame = Mathf.Clamp(nextFrame, minFrame, maxFrame);
        return Mathf.RoundToInt(nextFrame);

    }

    private void SetFaceAnim(Sprite[] currentTopAnim, Sprite[] currentBotAnim, int currentIndex)
    {
        topRenderer.sprite = currentTopAnim[currentIndex];
        bottomRenderer.sprite = currentBotAnim[currentIndex];
    }


    public override void OnMoveWindow()
    {
        Debug.Log("move da window");
        for (var i = 0; i < allMouthPoints.Length - 1; i++)
        {
            Debug.Log(i);
            allMouthVectors[i] = allMouthPoints[i].transform.position;
        }
    }


    public void RemoveTooth(GameObject theTooth)
    {
        allTeef.Remove(theTooth);
        if(allTeef.Count == 0)
        {
            StartTongue();
        }
    }

    void StartTongue()
    {
        tongueObj.SetActive(true);
        openIndex = 0;
        state = State.Tonguing;
    }

    public void SetMaxFrame(int frame)
    {
        if(frame < maxFrame)
        {
            maxFrame = frame;
        }
    }

}
