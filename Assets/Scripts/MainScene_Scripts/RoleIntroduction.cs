using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleIntroduction : MonoBehaviour
{
    public GameObject[] game;
    private NumController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        gameController = gameControllerObject.GetComponent<NumController> ();
    }

    public void Next(){
        gameController.num=gameController.num+1;
        if(gameController.num>4){
            gameController.num=0;
            game[4].SetActive(false);
            game[0].SetActive(true);
        }else{
            game[gameController.num-1].SetActive(false);
            game[gameController.num].SetActive(true); 
        }  
    }

    public void Previous(){
        gameController.num=gameController.num-1;
        if(gameController.num<0){
            game[0].SetActive(false);
            game[4].SetActive(true);
            gameController.num=4;
        }else{
            game[gameController.num+1].SetActive(false);
            game[gameController.num].SetActive(true); 
        }
    }
}
