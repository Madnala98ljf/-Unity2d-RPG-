using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private Transform emeny;
    private EmenyController gameController_E;
    private PlayerController gameController_P;
    public GameObject[] game_win;
    public GameObject[] game_lose;
    public GameObject win_load;
    public GameObject lose_load;
    public GameObject key;
    public GameObject gameself;
    private PauseGame pause;
    private int i=0;
    private int t=0;
    public int level;
    private bool canClick=false;
    private Transform other;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag ("Emeny");
        gameController_E = gameControllerObject.GetComponent<EmenyController> ();
        GameObject gameControllerObject1 = GameObject.FindWithTag ("Player");
        gameController_P = gameControllerObject1.GetComponent<PlayerController> ();
        emeny = GameObject.FindGameObjectWithTag("Emeny").transform;
        GameObject gameControllerObject2 = GameObject.FindWithTag ("Pause");
        pause = gameControllerObject2.GetComponent<PauseGame> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(pause.isPause==false){
            if(gameController_P.isDie==true){
                if(t==0){
                    Invoke("lose",5);
                    t++;
                }
                if(Input.GetMouseButtonDown(0)&&i<game_lose.Length&&canClick==true){
                    game_lose[i].SetActive(false);
                    i++;
                    if(i<game_lose.Length){
                        game_lose[i].SetActive(true);
                    }else{
                        gameself.SetActive(false);
                        lose_load.SetActive(true);
                    }
                }
            }

            if(gameController_E.isDie==true){
                if(t==0){
                    if(level==0)
                        Invoke("win1",2);
                    Invoke("win",5);
                    t++;
                }
                if(Input.GetMouseButtonDown(0)&&i<game_win.Length&&canClick==true){
                    game_win[i].SetActive(false);
                    i++;
                    if(i<game_win.Length){
                        game_win[i].SetActive(true);
                    }else{
                        gameself.SetActive(false);
                        win_load.SetActive(true);
                    }
                }
            }
        }
        
    }

    public void win(){
        game_win[0].SetActive(true);
        canClick=true;
    }

    public void win1(){
        Instantiate(key, emeny.position-new Vector3(0.1f,1.2f,0.0f), emeny.rotation);
    }

    public void lose(){
        game_lose[0].SetActive(true);
        canClick=true;
    }
}
