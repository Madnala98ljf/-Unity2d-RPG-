using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleIntroduction : MonoBehaviour
{
    public GameObject[] game;

    private int num;

    void Start()
    {
        num = 0;
    }

    public void Next(){
        num=num+1;
        if(num>3){
            num=0;
            game[3].SetActive(false);
            game[0].SetActive(true);
        }else{
            game[num-1].SetActive(false);
            game[num].SetActive(true); 
        }  
    }

    public void Previous(){
        num=num-1;
        if(num<0){
            game[0].SetActive(false);
            game[3].SetActive(true);
            num=3;
        }else{
            game[num+1].SetActive(false);
            game[num].SetActive(true); 
        }
    }
}
