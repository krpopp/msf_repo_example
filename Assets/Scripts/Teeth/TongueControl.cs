using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueControl : MonoBehaviour
{

    SpriteRenderer myRenderer;

    [SerializeField]
    Sprite[] allTongues;

    int tongueDex = 0;

    Vector3 mPos;
    Vector3 lastMPos;

    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;
        lastMPos = mPos;
    }

    // Update is called once per frame
    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;
    }

    private void OnMouseDrag()
    {
        Debug.Log("Draggin");
        if(Mathf.Abs(mPos.y - lastMPos.y) > 0.5f)
        {
            if(tongueDex < allTongues.Length - 1)
            {
                tongueDex++;
                myRenderer.sprite = allTongues[tongueDex];
                Vector2 currOffset = myCollider.offset;
                currOffset.y -= 0.1f;
                myCollider.offset = currOffset;
                lastMPos = mPos;
            }
        }   
    }
}
