using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundStory : MonoBehaviour {
    private int i = 0;
    private float dis;
    private Transform king;
    private Transform point;
    private Rigidbody2D rb;
    private Animator myAnimator;
    public GameObject[] game;
    public GameObject load;
    private bool isGo = false;
    private Vector3 dir;
    private PauseGame pause;

    void Awake () {
        king = GameObject.FindGameObjectWithTag ("king").transform;
        point = GameObject.FindGameObjectWithTag ("Point").transform;
        rb = GetComponent<Rigidbody2D> ();
        myAnimator = GetComponent<Animator> ();
        GameObject gameControllerObject1 = GameObject.FindWithTag ("Pause");
        pause = gameControllerObject1.GetComponent<PauseGame> ();
    }

    void Update () {
        if (pause.isPause == false) {
            dir = king.position - this.transform.position;
            Vector3 dix = point.position - this.transform.position;
            if (isGo == true) {
                myAnimator.SetBool ("isIdle", false);
                if (dix.y < -0.8f) {
                    //用Vector3.Lerp移动
                    //float lerpValue = Mathf.Lerp(transform.position.x,point.position.x,(Time.time)* 0.0003f);
                    //float lerpValue1 = Mathf.Lerp(transform.position.y,point.position.y,(Time.time)* 0.0003f);
                    //transform.position = new Vector3(lerpValue,lerpValue1,-0.1f);
                    float distance2 = Vector3.Distance (transform.position, point.position);
                    float lerpT = 1.0f * Time.deltaTime / (1 * distance2);
                    transform.position = Vector3.Lerp (transform.position, point.position, lerpT);
                    this.transform.localScale += new Vector3 (0.0002f, 0.0002f, 0.0002f);
                } else {
                    load.SetActive (true);
                }
            } else {
                if (dir.y > 2.2f) {
                    //用Vector3.Lerp移动
                    //float lerpValue = Mathf.Lerp(transform.position.x,king.position.x,(Time.time)* 0.0003f);
                    //float lerpValue1 = Mathf.Lerp(transform.position.y,king.position.y,(Time.time)* 0.0002f);
                    //transform.position = new Vector3(lerpValue,lerpValue1,-0.1f);

                    float distance2 = Vector3.Distance (transform.position, king.position);
                    float lerpT = 1.0f * Time.deltaTime / (1 * distance2);
                    transform.position = Vector3.Lerp (transform.position, king.position, lerpT);
                    this.transform.localScale -= new Vector3 (0.0002f, 0.0002f, 0.0002f);

                } else {
                    myAnimator.SetBool ("isIdle", true);
                    if (i == 0) {
                        game[0].SetActive (true);
                    }
                    if (Input.GetMouseButtonDown (0) && i >= 0 && i < game.Length) {
                        game[i].SetActive (false);
                        i++;
                        if (i < game.Length) {
                            game[i].SetActive (true);
                        } else {
                            isGo = true;
                        }
                    }
                }
            }
        }

    }
}