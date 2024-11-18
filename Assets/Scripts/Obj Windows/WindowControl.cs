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
    Vector3 mPos;

    GameObject followObject;

    GameObject myChild;

    Vector3 targ;
    public bool moveToPos = false;

    public bool inWindow = false;

    Collider2D myCollider;

    public int baseSort;

    public bool moved = false;

    Dictionary<SpriteRenderer, int> spriteOrdering = new Dictionary<SpriteRenderer, int>();

    //to keep track of player score
    public static int playerScore = 0;

    //for all the crazy audio sources we have
    List<AudioSource> audioSources = new List<AudioSource>();

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
    

    public void TurnOffColl()
    {
        myCollider.enabled = false;
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void StartMoveToNewPos(Vector3 _newPos)
    {
        targ = _newPos;
        moveToPos = true;
    }

    private void OnMouseEnter()
    {
        inWindow = true;
    }

    void OnMouseExit()
    {
        inWindow = false;
    }

    void OnEnable()
    {
        WindowManager.AddToOpenWindows(gameObject);
    }

    void OnDisable()
    {
        WindowManager.RemoveFromOpenWindows(gameObject);
    }

    public void UpdateAllSprites()
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
