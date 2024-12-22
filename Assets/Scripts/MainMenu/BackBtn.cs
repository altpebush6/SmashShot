using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        GameObject.Find("SceneManager").GetComponent<MainMenuManager>().GoBack();
    }
}
