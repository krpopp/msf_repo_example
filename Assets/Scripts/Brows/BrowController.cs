using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowController : MonoBehaviour
{

    BrowManager myManager;

    // Start is called before the first frame update
    void Start()
    {
        myManager = GetComponentInParent<BrowManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.tag == "wallpaper" && myManager.finished)
        {
            myManager.BrowIsLoose();
        }
    }
}
