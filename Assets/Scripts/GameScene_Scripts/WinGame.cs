using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    public GameObject go;
    public Button Btn;
    // Start is called before the first frame update
    void Start()
    {
        Btn.onClick.AddListener(Back_lose);
        Invoke("SetInfoShow",3);
    }

    private void SetInfoShow()
    {
        this.gameObject.SetActive(false);
        go.gameObject.SetActive(true);
    }

    public void Back_lose(){
        SceneManager.LoadScene("main");
    }
}