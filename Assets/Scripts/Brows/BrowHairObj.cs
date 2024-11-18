using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowHairObj : MonoBehaviour
{

    LineRenderer myLine;
    Vector3 mPos;
    Vector3 lmPos;

    [SerializeField]
    float maxPointDist;

    bool holdingOne = false;
    bool holdingTwo = false;

    [SerializeField]
    bool looseOne, looseTwo = false;

    [SerializeField]
    bool isFirstBrow = false;

    BrowManager myManager;

    public bool tugging = false;

    public float startDist;
    float currentDist;
    float distTraveled;

    float lastDist;

    [SerializeField]
    GameObject tugObj;

    AudioSource mySource;
    AudioClip[] clothAudio;

    int audioIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        myLine = GetComponent<LineRenderer>();
        myManager = GetComponentInParent<BrowManager>();
        startDist = Vector3.Distance(myLine.GetPosition(0), myLine.GetPosition(1));
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;
        lmPos = mPos;
        mySource = GetComponentInParent<AudioSource>();
        clothAudio = myManager.clothAudio;
        ShuffleAudio(clothAudio);
    }

    // Update is called once per frame
    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;
        if(!isFirstBrow)
        {
            if (looseOne)
            {
                if (CheckLineDist(transform.TransformPoint(myLine.GetPosition(0))))
                {
                    if (Input.GetMouseButton(0))
                    {
                        holdingOne = true;
                    }
                }

                if (holdingOne)
                {
                    myLine.SetPosition(0, transform.InverseTransformPoint(mPos));
                    if (Input.GetMouseButtonUp(0))
                    {
                        holdingOne = false;
                    }
                }
            }

            if (looseTwo)
            {
                if (CheckLineDist(transform.TransformPoint(myLine.GetPosition(1))))
                {
                    if (Input.GetMouseButton(0))
                    {
                        holdingTwo = true;
                    }
                }
                if (holdingTwo)
                {
                    myLine.SetPosition(1, transform.InverseTransformPoint(mPos));
                    if (Input.GetMouseButtonUp(0))
                    {
                        holdingTwo = false;
                    }
                }
            }
        } else
        {
            if (looseTwo)
            {
                PhysThread();
                if (holdingTwo)
                {
                    if (Vector3.Distance(mPos, lmPos) > 0f)
                    {
                        PlayAudio();
                        tugging = true;
                    }
                    else
                    {
                        StopAudio();
                        tugging = false;
                    }
                }
                if (CheckLineDist(transform.TransformPoint(myLine.GetPosition(1))))
                {
                    if (Input.GetMouseButton(0))
                    {
                        holdingTwo = true;
                    }
                }
                if (holdingTwo)
                {
                    float dist = BrowManager.maxDist;
                    Vector3 newPos = transform.InverseTransformPoint(mPos) - myLine.GetPosition(0);
                    
                    if (Vector3.Distance(transform.InverseTransformPoint(mPos), myLine.GetPosition(0)) < BrowManager.maxDist)
                    {
                        dist = 1;
                    } else
                    {
                        newPos = newPos.normalized;
                    }
                    myLine.SetPosition(1, myLine.GetPosition(0) + (newPos * dist));
                    if (Input.GetMouseButtonUp(0))
                    {
                        StopAudio();
                        holdingTwo = false;
                        tugging = false;
                    }
                }
            }
        }
        lmPos = mPos;
    }

    bool CheckLineDist(Vector3 point)
    {
        if (Vector3.Distance(mPos, point) < maxPointDist)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void TugOutBrow()
    {
        Vector3 newTwoPos = Vector3.MoveTowards(myLine.GetPosition(1), myLine.GetPosition(0), 1f * Time.deltaTime);
        myLine.SetPosition(1, newTwoPos);
        currentDist = Vector3.Distance(myLine.GetPosition(0), myLine.GetPosition(1));
        distTraveled = startDist - currentDist;
        float newDist = distTraveled - lastDist;
        BrowManager.maxDist += newDist;
        lastDist = distTraveled;
        if (currentDist < 0.1)
        {
            myManager.RemoveBrow();
            Destroy(gameObject);
        }
    }

    void PlayAudio()
    {
        if(mySource.isPlaying)
        {
            if(mySource.time > mySource.clip.length - 0.1)
            {
                audioIndex++;
                if (audioIndex >= clothAudio.Length)
                { 
                    ShuffleAudio(clothAudio);
                    audioIndex = 0;
                }
                mySource.clip = clothAudio[audioIndex];
                mySource.Play();
            }
        } else
        {
            if(mySource.clip == null)
            {
                mySource.clip = clothAudio[audioIndex];
            }
            mySource.Play();
        }
    }

    void StopAudio()
    {
        mySource.Pause();
    }


    void PhysThread()
    {
        if(holdingTwo)
        {
            tugObj.transform.position = transform.TransformPoint(myLine.GetPosition(1));
        } else
        {
            myLine.SetPosition(1, transform.InverseTransformPoint(tugObj.transform.position));
        }
    }

    void ShuffleAudio(AudioClip[] clips)
    {
        for(int i = 0; i < clips.Length; i++)
        {
            AudioClip tmp = clips[i];
            int r = Random.Range(i, clips.Length);
            clips[i] = clips[r];
            clips[r] = tmp;
        }
    }

}
