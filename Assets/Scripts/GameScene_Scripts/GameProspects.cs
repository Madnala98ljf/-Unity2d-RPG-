using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProspects : MonoBehaviour
{
    public GameObject[] game;
    public GameObject gameself;
    public GameObject game_ing;
    private int i=0;
    private PauseGame pause;

    void Start()
    {
        GameObject gameControllerObject1 = GameObject.FindWithTag ("Pause");
        pause = gameControllerObject1.GetComponent<PauseGame> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(pause.isPause==false){
            if(Input.GetMouseButtonDown(0)&&i<game.Length){
                game[i].SetActive(false);
                i++;
                if(i<game.Length){
                    game[i].SetActive(true);
                }else{
                    gameself.SetActive(false);
                    game_ing.SetActive(true);
                }
            }
        }
        
    }
}
