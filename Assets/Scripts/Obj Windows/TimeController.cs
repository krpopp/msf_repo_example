using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeController : MonoBehaviour
{

    TMP_Text timeText;
    DateTime now;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        now = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = now.ToString("ddd, MMM d h:mm tt");
    }
}
