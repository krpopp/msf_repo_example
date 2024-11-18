using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PWInput : MonoBehaviour
{

    TMP_InputField pwInput;

    // Start is called before the first frame update
    void Start()
    {
        pwInput = GetComponent<TMP_InputField>();
        pwInput.contentType = TMP_InputField.ContentType.Password;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
