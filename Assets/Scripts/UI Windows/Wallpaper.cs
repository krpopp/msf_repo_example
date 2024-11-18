using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wallpaper : MonoBehaviour
{
    [SerializeField] GameObject eyeWindow;

    private void Start()
    {
        eyeWindow.SetActive(false);
    }

    private void OnMouseDown()
    { 
        UIManager.DeactivateIcon();
        IconObj.DeactivateObjHighlight();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
}
