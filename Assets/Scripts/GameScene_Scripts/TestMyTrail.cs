using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMyTrail : MonoBehaviour {

    public WeaponTrail myTrail;
    private PlayerController gameController;

    private float t = 0.033f;
    private float tempT = 0;
    private float animationIncrement = 0.003f;

    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag ("Player");
        gameController = gameControllerObject.GetComponent<PlayerController> ();
    }

    void LateUpdate () {
        t = Mathf.Clamp (Time.deltaTime, 0, 0.066f);

        if (t > 0) {
            while (tempT < t) {
                tempT += animationIncrement;

                if (myTrail.time > 0) {
                    if(gameController.speed==4)
                        myTrail.Itterate (Time.time - t + tempT);
                } else {
                    myTrail.ClearTrail ();
                }
            }

            tempT -= t;

            if (myTrail.time > 0) {
                myTrail.UpdateTrail (Time.time, t);
            }
        }
    }


    public void StartTrails () {
        //设置拖尾时长
        myTrail.SetTime (2.0f, 0.0f, 1.0f);

        myTrail.StartTrail (0.5f, 0.4f);
    }

    //清除拖尾

    public void ClearTrails () {

        myTrail.ClearTrail ();
    }
}