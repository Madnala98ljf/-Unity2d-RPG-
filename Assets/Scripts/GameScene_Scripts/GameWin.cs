using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour {
    public Transform point1;
    public Transform point2;
    private Vector3 dir1;
    private Vector3 dir2;
    private bool finash_1 = false;
    private Animator myAnimator;
    public GameObject[] game;
    private PauseGame pause;
    public GameObject win;
    private int i = 0;

    void Start () {
        myAnimator = GetComponent<Animator> ();
        myAnimator.SetBool ("isWalk", true);
        GameObject gameControllerObject1 = GameObject.FindWithTag ("Pause");
        pause = gameControllerObject1.GetComponent<PauseGame> ();
    }

    // Update is called once per frame
    void Update () {
        if (pause.isPause == false) {
            if (finash_1 == false) {
                dir1 = point1.position - this.transform.position;
                if (dir1.y < -0.2f || dir1.x > 0.2f) {
                    //float lerpValue = Mathf.Lerp(transform.position.x,point1.position.x,(Time.time)* 0.0001f);
                    //float lerpValue1 = Mathf.Lerp(transform.position.y,point1.position.y,(Time.time)* 0.0001f);
                    //transform.position = new Vector3(lerpValue,lerpValue1,-0.1f);
                    float distance2 = Vector3.Distance (transform.position, point1.position);
                    float lerpT = 1.0f * Time.deltaTime / (1 * distance2);
                    transform.position = Vector3.Lerp (transform.position, point1.position, lerpT);
                    this.transform.localScale += new Vector3 (0.00025f, 0.00025f, 0.00025f);
                } else {
                    finash_1 = true;
                }
            } else {
                dir2 = point2.position - this.transform.position;
                if (dir2.y < -0.1f || dir2.x < -0.1f) {
                    //float lerpValue = Mathf.Lerp(transform.position.x,point2.position.x,(Time.time-2.0f)* 0.0001f);
                    //float lerpValue1 = Mathf.Lerp(transform.position.y,point2.position.y,(Time.time-2.0f)* 0.0001f);
                    //transform.position = new Vector3(lerpValue,lerpValue1,-0.1f);
                    float distance2 = Vector3.Distance (transform.position, point2.position);
                    float lerpT = 1.0f * Time.deltaTime / (1 * distance2);
                    transform.position = Vector3.Lerp (transform.position, point2.position, lerpT);
                } else {
                    myAnimator.SetBool ("isWalk", false);
                    if (i == 0) {
                        game[0].SetActive (true);
                    }
                    if (Input.GetMouseButtonDown (0) && i >= 0 && i < game.Length) {
                        game[i].SetActive (false);
                        i++;
                        if (i < game.Length) {
                            game[i].SetActive (true);
                        } else {
                            win.SetActive (true);
                        }
                    }
                }
            }
        }

    }
}