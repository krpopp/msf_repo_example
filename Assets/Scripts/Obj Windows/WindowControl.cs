using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowControl : MonoBehaviour
{
    private bool clickBar;

    public bool ClickBar
    {
        get
        {
            return clickBar;
        }
        set
        {
            if (value)
            {
                WindowManager.SendToEndUpdateList(gameObject);
                transform.SetParent(followObject.transform);
                moved = true;
                followObject.GetComponent<FollowObj>().SetObj(gameObject);
            }
            else
            {
                followObject.GetComponent<FollowObj>().UnSetObj();
                transform.SetParent(null);
            }
        }
    }

    Vector3 lMPos;
    Vector2 mPos;
    Vector3 lastPos;

    GameObject followObject;

    GameObject myChild;

    Vector3 targ;
    private bool moveToPos = false;

    public bool inWindow = false;

    Collider2D myCollider;

    public int baseSort;

    public bool moved = false;

    Dictionary<SpriteRenderer, int> spriteOrdering = new Dictionary<SpriteRenderer, int>();


    //Window Objects to spawn!
    GameObject[] prefabs;

    private void Awake()
    {
        MakeSpriteDictionary();
        //if (enabled)
        //{
        //    WindowManager.AddToOpenWindows(gameObject);
        //}
        
        //yipeee
        Debug.Log(prefabs[3]);
    }

    // Start is called before the first frame update
    void Start()
    {
        followObject = GameObject.Find("FollowObject");
        myChild = gameObject.transform.GetChild(0).gameObject;
        myCollider = GetComponent<Collider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPos.z = 0;

        //math stuff
        int a = Random.Range(1, 3);
        int b;
        Debug.Log(a/b);

        if (moveToPos)
        {
            Debug.Log("move");
            //float step = 100.0f * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, targ, step);
            //if(Vector3.Distance(transform.position, targ) < 2f)
            //{
            //    myChild.GetComponent<BodyScene>().ready = true;
            //    myChild.GetComponent<BodyScene>().OnMoveWindow();
            //    moveToPos = false;
            //}
        }
        Debug.Log(1 + 1);
    }

    private void FixedUpdate()
    {
        Debug.Log("hiiiii");
    }

    public void DoNotUpdateSprites()
    {
        foreach(var i in spriteOrdering)
        {
            if(i.Key != null)
            {
                int newOrder = i.Value + baseSort;
                i.Key.sortingOrder = newOrder;
                if(i.Key.tag == "tooth")
                {
                    i.Key.GetComponent<TeethControl>().baseToothSort = newOrder;
                }
            }
        
    }

    void MakeSpriteDictionary()
    {
        SpriteRenderer[] allRenderers = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < allRenderers.Length; i++)
        {
            spriteOrdering.Add(allRenderers[i], allRenderers[i].sortingOrder);
        }
    }
}
