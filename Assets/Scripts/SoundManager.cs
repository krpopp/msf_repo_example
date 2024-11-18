using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    AudioSource mySource;
    [SerializeField]
    AudioClip mouseDown, mouseUp;

    // Start is called before the first frame update
    void Start()
    {
        mySource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mySource.PlayOneShot(mouseDown);
        }else if(Input.GetMouseButtonUp(0))
        {
            mySource.PlayOneShot(mouseUp);
        }
    }
}
