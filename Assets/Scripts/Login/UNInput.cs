using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UNInput : MonoBehaviour
{
    //[SerializeField]
    //TMP_InputField nameInput;

    [SerializeField]
    TMP_Text mainText;

    TMP_InputField inputField;

    string realName = "KARINA";
    public int nameIndex = -1;

    string currentName = "";

    // Start is called before the first frame update
    void Start()
    {
        // nameInput.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        inputField = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if(inputField.isFocused)
        {
            if(Input.anyKeyDown)
            {
                ValueChangeCheck();
            }
        }
    }

    public void ValueChangeCheck()
    {
        if(nameIndex < realName.Length - 1)
        {
            nameIndex++;
            char currChar = realName[nameIndex];
            currentName = currentName + currChar;
            inputField.text = currentName;
            if(nameIndex >= realName.Length - 1)
            {
                inputField.interactable = false;
                inputField.DeactivateInputField();
            }
        } 
    }

}
