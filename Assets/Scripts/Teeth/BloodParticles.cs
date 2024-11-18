using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{

    ParticleSystem mainParts;

    // Start is called before the first frame update
    void Start()
    {
        mainParts = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        int currFrame = Mathf.RoundToInt((mainParts.time / mainParts.textureSheetAnimation.spriteCount) * 100);
        //Debug.Log(currFrame);
        if (currFrame == 13)
        {
            Debug.Log("hello");
            mainParts.TriggerSubEmitter(0);
        }
    }
}
