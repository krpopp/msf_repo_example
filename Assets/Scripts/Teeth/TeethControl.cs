using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethControl : MonoBehaviour
{

    //TO DO
    //update start position: use local scale? 

    [SerializeField]
    SpriteRenderer rootRenderer;

    Rigidbody2D myBody;
    SpriteRenderer myRenderer;
    SpringJoint2D mySpring;

    bool holding = false;

    enum State
    {
        Rooted,
        Loose,
        Free
    }

    State state = State.Rooted;

    [SerializeField]
    float freq = 1000;

    Vector3 lastMPos;
    Vector3 mPos;

    [SerializeField]
    float looseDist, breakGate;

    Vector3 startPos;

    LineRenderer myLine;

    [SerializeField]
    GameObject connectObj;
    Vector3 connectPoint;

    [SerializeField]
    FaceTestManager faceManager;

    [SerializeField]
    float rootedDist;

    [SerializeField]
    float rootedSpeed;

    [SerializeField]
    float rootedHealth, rootedReset;

    float rootedStep;

    public bool inWindow = true;

    [SerializeField]
    GameObject desktopIcon;

    public int baseToothSort;

    [SerializeField]
    ParticleSystem bloodParts;

    float startLineThickness;

    bool isBottom = false;

    ParticleSystem burstParts;

    AudioSource mySource;

    AudioClip[] tapAudio;
    AudioClip[] ripAudio;

    int ripAudioIndex = 0;
    int tapAudioIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetVars();
    }

    // Update is called once per frame
    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Rooted:
                if (holding)
                {
                    DamageStep();
                    RootedFollow();
                }
                break;
            case State.Loose:
                UpdateVein();
                UpdateSortingOrder();
                if (holding)
                {
                    LooseFollow();
                }
                break;
            case State.Free:
                if (holding)
                {
                    FreeFollow();
                }
                break;
        }
    }

    private void OnMouseDown()
    {
        holding = true;
        //mySpring.enabled = false;
        FaceTestManager.holdingTooth = true;
        myRenderer.sortingOrder = baseToothSort + 30;
        rootRenderer.sortingOrder = baseToothSort + 29;
        if (state == State.Rooted || state == State.Loose)
        {
            float bloodChance = Random.Range(0, rootedHealth);
            if(bloodChance < 30)
            {
                PlayRipAudio();
                burstParts.transform.position = mPos;
                burstParts.Play();
            }
        }
    }

    private void OnMouseUp()
    {
        FaceTestManager.holdingTooth = false;
        holding = false;
        switch(state)
        {
            case State.Rooted:
                StartCoroutine(MoveToPosition(startPos));
                myRenderer.sortingOrder = baseToothSort;
                rootRenderer.sortingOrder = baseToothSort - 1;
                break;
            case State.Loose:
                //mySpring.enabled = true;
                break;
            case State.Free:
                if(!inWindow)
                {
                    ReleaseToWinow();
                }
                break;
            default:
                break;
        }
    }

    void ReleaseToWinow()
    {
        GameObject newIcon = Instantiate(desktopIcon);
        newIcon.transform.position = transform.position;
        Destroy(gameObject);
    }

    void SetVars()
    {
        myBody = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        rootRenderer = GetComponentInChildren<SpriteRenderer>();
        mySpring = GetComponent<SpringJoint2D>();
        myLine = GetComponent<LineRenderer>();

        //point that the iine is connected to
        connectPoint = connectObj.transform.position;
        baseToothSort = myRenderer.sortingOrder;
        rootRenderer.sortingOrder = baseToothSort - 1;
        mySpring.frequency = freq;
        startPos = transform.localPosition;

        startLineThickness = myLine.startWidth;

        mySource = GameObject.Find("MouthWin").GetComponent<AudioSource>();
        tapAudio = faceManager.tapAudio;
        ripAudio = faceManager.ripAudio;

        ShuffleAudio(tapAudio);
        ShuffleAudio(ripAudio);

        if(transform.parent.name == "bottomgums")
        {
            isBottom = true;
        }
        burstParts = GameObject.Find("BurstBlood").GetComponent<ParticleSystem>();
    }

    void RootedFollow()
    {
        Vector3 newStart = transform.parent.transform.TransformPoint(startPos);
        //get the distance between the tooth's pos and its starting pos
        float dist = Vector3.Distance(transform.position, newStart);
        //if the disatnce is less that this
        if(dist < rootedDist)
        {
            //move towards the mouse
            myBody.MovePosition(mPos);
        } else
        {
            //otherwise drop the tooth
            holding = false;
            //have the tooth move back towards its starting position
            StartCoroutine(MoveToPosition(startPos));
        }
    }

    void UpdateSortingOrder()
    {
        //if (faceManager.openIndex > 9)
        //{
        //    myRenderer.sortingOrder = baseToothSort;
        //}
    }

    void LooseFollow()
    {
        myBody.MovePosition(mPos);
        Tugging();
    }

    void FreeFollow()
    {
        myBody.MovePosition(mPos);
    }

    void ActivateBody()
    {
        myBody.bodyType = RigidbodyType2D.Dynamic;
    }

    void ActivateSpring()
    {
        mySpring.enabled = true;
    }

    void Tugging()
    {
        //distance between the mouse and its last position
        float dist = Mathf.Abs(Vector3.Distance(mPos, lastMPos));
        //if it's less than the distance i want and the frequency is high enough
        if(dist > looseDist && freq > 1)
        {
            //decrease the frequency
            freq -= 2;
            mySpring.frequency = freq;
            //set the last position of the mouse
            lastMPos = mPos;
            //if the frequency has gotten too low then set when the spring should break
            if(freq <= 5)
            {
                mySpring.breakForce = breakGate;
            }
        }
    }

    void DamageStep()
    {
        //damage the conenction of a rooted tooth
        rootedStep -= 1;
        if(rootedStep <= 0)
        {
            rootedHealth -= 1;
            if(rootedHealth <= 0)
            {
                GetLoose();
            }
        }
    }

    void GetLoose()
    {
        ActivateBody();
        ActivateSpring();
        if(isBottom)
        {
            faceManager.SetMaxFrame(2);
        } else
        {
            faceManager.SetMaxFrame(18);
        }
        state = State.Loose;
    }

    void Release()
    {
        //when a tooth is released from the gums entirely
        myLine.enabled = false;
        myRenderer.sortingOrder = baseToothSort + 3;

        rootRenderer.sortingOrder = baseToothSort + 2;
        myBody.mass = 1;
        myBody.drag = 0;
        //waterManager.teethOut--;
        if(bloodParts != null)
        {
            PlayRipAudio();
            bloodParts.Play();
        }
        state = State.Free;
        faceManager.RemoveTooth(gameObject);
    }

    private void OnJointBreak2D(Joint2D joint)
    {
        Release();
    }

    void UpdateVein()
    {
        Vector3 lineA = transform.position;
        Vector3 lineB = connectObj.transform.position;
        //if the mouse is near its starting position
        if (Vector3.Distance(transform.position, startPos) > 0.1)
        {
            //draw the line above the skin
            myLine.sortingOrder = baseToothSort + 2;
        } else 
        {
            //otherwise draw the line under the skin
            myLine.sortingOrder = baseToothSort - 1;
        }
        float lineDist = Vector3.Distance(lineA, lineB);
        float newWidth = Mathf.Clamp(0.1f / lineDist, 0.01f, 0.1f);
        myLine.SetWidth(newWidth, newWidth);
        myLine.SetPosition(0, lineA);
        myLine.SetPosition(1, lineB);
    }

    //for moving the tooth back to its starting position (when its rooted)
    IEnumerator MoveToPosition(Vector3 end)
    {
        float time = 0;
        Vector3 start = transform.position;
        end = transform.parent.transform.TransformPoint(end);
        while (time <= 1)
        {
            time += Time.fixedDeltaTime / rootedSpeed;
            myBody.MovePosition(Vector3.Lerp(start, end, time));
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("window"))
        {
            inWindow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("window"))
        {
            inWindow = false;
        }
    }

    void ShuffleAudio(AudioClip[] clips)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            AudioClip tmp = clips[i];
            int r = Random.Range(i, clips.Length);
            clips[i] = clips[r];
            clips[r] = tmp;
        }
    }

    private void OnMouseEnter()
    {
        if(!FaceTestManager.holdingTooth && faceManager.mouthOpen)
        {
            mySource.PlayOneShot(tapAudio[tapAudioIndex]);
            tapAudioIndex++;
            if (tapAudioIndex >= tapAudio.Length)
            {
                tapAudioIndex = 0;
                ShuffleAudio(tapAudio);
            }
        }
    }

    void PlayRipAudio()
    {
        mySource.PlayOneShot(ripAudio[ripAudioIndex]);
        ripAudioIndex++;
        if(ripAudioIndex >= ripAudio.Length)
        {
            ripAudioIndex = 0;
            ShuffleAudio(ripAudio);
        }
    }
}
