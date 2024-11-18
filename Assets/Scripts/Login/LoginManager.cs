using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    GameObject hintObj;

    [SerializeField]
    TMP_InputField pwInput;

    [SerializeField]
    UNInput unInput;

    public void ForgotPW()
    {
        hintObj.SetActive(true);
        StartCoroutine(TurnOffHint());
    }

    IEnumerator TurnOffHint()
    {
        yield return new WaitForSeconds(3f);
        hintObj.SetActive(false);
    }

    public void LoadDesktop()
    {
        if(unInput.nameIndex >= 5 && pwInput.text.Length > 0)
        {
            SceneManager.LoadScene("FaceTest");
        }
    }
}
