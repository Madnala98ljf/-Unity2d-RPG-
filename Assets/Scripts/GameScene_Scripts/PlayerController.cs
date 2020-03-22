using System.Collections;
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
    private float HorizontalDirection;
    private float VerticalDirection;
    public GameObject IceBall;
    private bool isSkill = false;
    private GameObject skill;
    private float skillFaceTo;

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
        //输入检测
        InputSystem.ProcessInput();
        //角色控制
        RoleCtr();
        //技能控制
        SkillCtr();
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

                        AnimatorStateInfo stateinfo = myAnimator.GetCurrentAnimatorStateInfo(0);
                        if ((stateinfo.normalizedTime >= 0.8f))//normalizedTime：0-1在播放、0开始、1结束
                        {  
                            //完成后的逻辑
                            if(IsAttack)
                                IsAttack = false;
                        }

                        if (InputKey.TriggerSkill)
                        {
                            if(InputKey.IsPressingSkill)
                                return;
                            
                            if(IsAttack)
                                return;

                            InputKey.IsPressingSkill = true;
                            myAnimator.SetTrigger("Attack");
                            IsAttack = true;
                            if (dir.x < 2.0f && dir.x > -2.0f && dir.y < 0.3f && dir.y > -0.3f)
                            {
                                if (dir.x < 0)
                                {
                                    UnityEngine.Debug.Log(3);
                                    if (transform.rotation.y == 0)
                                    {
                                        UnityEngine.Debug.Log(1);
                                        gameController.hp -= Aggressivity;
                                    }
                                }
                                else
                                {
                                    UnityEngine.Debug.Log(4);
                                    if (transform.rotation.y == -1)
                                    {
                                        UnityEngine.Debug.Log(2);
                                        gameController.hp -= Aggressivity;
                                    }
                                }
                            }
                            return;
                        }
                        else
                        {
                            InputKey.IsPressingSkill = false;
                        }

                        if (!IsAttack)
                        {
                            if (InputKey.TriggerUp || InputKey.TriggerLeft || InputKey.TriggerRight ||
                                InputKey.TriggerDown)
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

                                if (InputKey.HorizontalDirection == 1)
                                {
                                    HorizontalDirection = Const.MoveSpeed * speed;
                                    if (transform.rotation.y == -1)
                                        transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                                }
                                else if (InputKey.HorizontalDirection == -1)
                                {
                                    HorizontalDirection = -Const.MoveSpeed * speed;
                                    if (transform.rotation.y == 0)
                                        transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                                }
                                else
                                    HorizontalDirection = 0.0f;

                                if (InputKey.VerticalDirection == 1)
                                    VerticalDirection = Const.MoveSpeed * speed;
                                else if (InputKey.VerticalDirection == -1)
                                    VerticalDirection = -Const.MoveSpeed * speed;
                                else
                                    VerticalDirection = 0.0f;
                                Vector3 move = new Vector3(HorizontalDirection, VerticalDirection, 0.0f);
                                //rb.velocity = move * speed;
                                //rb.MovePosition(move);
                                transform.position += move;
                            }
                            else
                            {
                                myAnimator.SetBool("isRun", false);
                                myAnimator.SetBool("isWalk", false);
                                isWalk = false;
                            }
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

    public void SkillCtr()
    {
        if(pause.isPause)
            return;
        if (isSkill)
        {
            skillBall();
            return;
        }

        if (InputKey.TriggerIceSkill && !Variable.IsIceSkilling)
        {
            Variable.IsIceSkillTrigger = true;
            Variable.IsIceSkilling = true;
            isSkill = true;
            if(transform.rotation.y == 0)
                skill = Instantiate(IceBall, transform.position + new Vector3(0.5f,-0.2f,0.0f),transform.rotation);
            else
                skill = Instantiate(IceBall, transform.position - new Vector3(0.5f,0.2f,0.0f),transform.rotation);
            skillFaceTo = transform.rotation.y;
            return;
        }
        
        if (InputKey.TriggerFireSkill && !Variable.IsFireSkilling)
        {
            
        }
    }

    private void skillBall()
    {
        Vector3 move = new Vector3(Const.SkillSpeed, 0.0f, 0.0f);
        if(skillFaceTo == 0)
            skill.transform.position += move;
        else
            skill.transform.position -= move;
        Vector3 dir = skill.transform.position - emeny.position;
        if (dir.x < 1.0f && dir.x > -1.0f && dir.y < 0.8f && dir.y > -0.8f)
        {
            if (dir.x < 0)
            {
                if (skill.transform.rotation.y == 0)
                {
                    DestoryBall();
                    return;
                }
            }
            else
            {
                if (skill.transform.rotation.y == -1)
                {
                    DestoryBall();
                    return;
                }
            }
        }

        if (skill.transform.position.x > boundary.xMax || skill.transform.position.x < boundary.xMin)
            DestoryBall();
    }

    private void DestoryBall()
    {
        Destroy(skill.gameObject);
        isSkill = false;
    }

    public void end_game()
    {
        Destroy(this.gameObject, 2.5f);
    }
    
}