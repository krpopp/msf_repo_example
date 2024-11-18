using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControl : MonoBehaviour
{

    [SerializeField]
    Sprite[] allFrames;

    public int frameIndex = 0;

    SpriteRenderer myRenderer;

    Vector3 openPos;
    Vector3 mPos;

    public int teethOut;

    [SerializeField]
    GameObject[] allTeeth;

    // Start is called before the first frame update
    void Start()
    {
        SetVars();
    }

    // Update is called once per frame
    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        OpenMouth();
    }

    void SetVars()
    {
        myRenderer = GameObject.Find("FaceSprite").GetComponent<SpriteRenderer>();

        //total frames in the mouth's animation
        frameIndex = allFrames.Length - 1;
        //set to the last frame
        myRenderer.sprite = allFrames[frameIndex];
        //set the position to open mouth relative to mouse position
        openPos = GameObject.Find("OpenPoint").transform.position;

        teethOut = allTeeth.Length;
    }

    void OpenMouth()
    {
        //find dist b/t mouth open point and mouse
        int dist = Mathf.RoundToInt(Vector2.Distance(mPos, openPos));
        //if the distance is within the number of frames
        if (dist < allFrames.Length - 1)
        {
            //set the frame to that distance
            frameIndex = dist;
            myRenderer.sprite = allFrames[frameIndex];
        }
    }
}