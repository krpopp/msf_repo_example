using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilControl : MonoBehaviour
{
    Vector3 lastMPos;
    Vector2 currMPos;

    [SerializeField]
    Sprite[] allFrames;

    int frameIndex = 0;

    SpriteRenderer myRend;

    [SerializeField]
    float distCheck;

    [SerializeField]
    GameObject waterObj;

    [SerializeField]
    GameObject mainSprite, onlyHand, finalSprite;

    Animator myAnim;

    BiteManager biteManager;

    // Start is called before the first frame update
    void Start()
    {
        lastMPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myRend = GetComponentInChildren<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        biteManager = GameObject.Find("BiteManager").GetComponent<BiteManager>();
        myRend.sprite = allFrames[frameIndex];
    }

    // Update is called once per frame
    void Update()
    {
        currMPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckMove();
        
    }

    void CheckMove()
    {
        float dist = Mathf.Abs(currMPos.x - lastMPos.x);
        if(frameIndex < 15)
        {
            if (dist > distCheck)
            {
                UpdateSprite();
                UpdateMousePos();
            }
        } else
        {
            if (currMPos.x < lastMPos.x && dist > distCheck)
            {
                UpdateSprite();
                UpdateMousePos();
            }
        }
        
    }

    void UpdateSprite()
    {
        if(allFrames.Length - 1 > frameIndex)
        {
            frameIndex += 1;
            myRend.sprite = allFrames[frameIndex];
        } else
        {
            StartTransitionToWater();
        }
    }

    void UpdateMousePos()
    {
        lastMPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void StartTransitionToWater()
    {
        mainSprite.SetActive(false);
        onlyHand.SetActive(true);
        finalSprite.SetActive(true);
        myAnim.SetBool("transitionWater", true);
    }

    public void TransitionToWater()
    {
        waterObj.SetActive(true);
        gameObject.SetActive(false);
        biteManager.SetCursor();
    }

}
