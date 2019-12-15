using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoadSystem : MonoBehaviour
{
    public Text text_Username;
    public InputField text_Password;
    public GameObject error;
    public GameObject game;

    public void Load_click() {
        if((text_Username.text=="liujiafa"||text_Username.text=="LIUJIAFA")&&text_Password.text=="1611620028"){
            //SceneManager.LoadScene("main");
            game.SetActive(true);
        }
            
        else
            error.SetActive(true);
    }
}
