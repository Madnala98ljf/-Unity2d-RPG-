using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Back_lose",3);
    }

    public void Back_lose(){
        SceneManager.LoadScene("main");
    }
}
