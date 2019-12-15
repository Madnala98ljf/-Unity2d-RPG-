using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginGame : MonoBehaviour
{
    public GameObject game;
    public GameObject gameself;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Next_gameobject",3);
    }

    public void Next_gameobject(){
        gameself.SetActive(false);
        game.SetActive(true);
    }
}
