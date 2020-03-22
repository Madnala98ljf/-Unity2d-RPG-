﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

[System.Serializable]
public class Boundary
{
    public float xMax, xMin, yMax, yMin;
}

public class PlayerController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody2D rb;
    public Boundary boundary;
    public int hp; //生命值
    public Text hptext;
    public Image hpimage;
    public int speed; //移动速度
    private EmenyController gameController;
    private PauseGame pause;
    private Transform emeny;
    public bool IsDead = false;
    public bool isWalk = false;
    public bool IsAttack = false;
    public int Aggressivity; //攻击力
    public float hp_max;
    public string s;
    public bool IsDefense = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("Emeny");
        gameController = gameControllerObject.GetComponent<EmenyController>();
        emeny = GameObject.FindGameObjectWithTag("Emeny").transform;
        GameObject gameControllerObject1 = GameObject.FindWithTag("Pause");
        pause = gameControllerObject1.GetComponent<PauseGame>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputSystem.ProcessInput();
        RoleCtr();
    }

    public void RoleCtr()
    {
        if (!pause.isPause)
        {
            if (gameController.IsDead)
            {
                speed = 0;
                myAnimator.SetBool("isBlock", false);
                myAnimator.SetBool("isWalk", false);
                myAnimator.SetBool("isRun", false);
                return;
            }

            if (!IsDead && !gameController.IsDead)
            {
                if (hp > 0)
                {
                    hpimage.fillAmount = hp / hp_max;
                    hptext.text = hp.ToString() + s;
                    if (IsDefense)
                    {
                        IsAttack = false;
                        myAnimator.SetBool("isRun", false);
                        myAnimator.SetBool("isWalk", false);
                        myAnimator.SetBool("isBlock", true);
                        rb.constraints = RigidbodyConstraints2D.FreezePosition;
                        rb.bodyType = RigidbodyType2D.Dynamic;
                    }
                    else
                    {
                        rb.bodyType = RigidbodyType2D.Kinematic;
                        Vector3 dir = this.transform.position - emeny.position;
                        myAnimator.SetBool("isBlock", false);

                        if (InputKey.TriggerUp || InputKey.TriggerLeft || InputKey.TriggerRight || InputKey.TriggerDown)
                        {
                            if (speed == 4)
                            {
                                myAnimator.SetBool("isRun", true);
                            }
                            else
                            {
                                myAnimator.SetBool("isWalk", true);
                            }

                            isWalk = true;

                            if (InputKey.HorizontalDirection == 1 && transform.rotation.y == -1)
                            {
                                transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                            }
                            else if(InputKey.HorizontalDirection == -1 && transform.rotation.y == 0)
                            {
                                transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                            }
                        }
                        else
                        {
                            myAnimator.SetBool("isRun", false);
                            myAnimator.SetBool("isWalk", false);
                            isWalk = false;
                        }


                        if (Input.GetMouseButtonUp(0))
                        {
                            //myAnimator.SetBool ("isAttack", false);
                            IsAttack = false;
                        }

                        if (Input.GetMouseButtonDown(0) && isWalk == false)
                        {
                            myAnimator.SetTrigger("Attack");
                            //myAnimator.SetBool ("isAttack", true);
                            IsAttack = true;
                            if (dir.x < 2.0f && dir.x > -2.0f && dir.y < 0.3f && dir.y > -0.3f)
                            {
                                //gameController.hp-=100;
                                if (dir.x < 0)
                                {
                                    if (transform.rotation.y == 0)
                                    {
                                        gameController.hp -= Aggressivity;
                                    }
                                }
                                else
                                {
                                    if (transform.rotation.y == -1)
                                    {
                                        gameController.hp -= Aggressivity;
                                    }
                                }
                            }

                            return;
                        }

                        if (!IsAttack)
                        {
                            float h = Input.GetAxis("Horizontal");
                            float v = Input.GetAxis("Vertical");

                            Vector3 move = new Vector3(h, v, 0.0f);
                            //rb.velocity = move * speed;
                            rb.MovePosition(move);
                        }

                        rb.position = new Vector3(
                            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
                            0.0f
                        );
                    }
                }
                else
                {
                    hpimage.fillAmount = 0 / 100f;
                    hptext.text = "0" + s;
                    //rb.freezePosition = true;
                    myAnimator.SetBool("isWalk", false);
                    myAnimator.SetBool("isRun", false);
                    //myAnimator.SetBool ("isAttack", false);
                    myAnimator.SetTrigger("Die");
                    Invoke("end_game", 0.0f);
                    IsDead = true;
                }
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    public void end_game()
    {
        Destroy(this.gameObject, 2.5f);
    }
}