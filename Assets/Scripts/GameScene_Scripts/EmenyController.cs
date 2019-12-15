using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmenyController : MonoBehaviour {
    private Transform player;
    public Animator myAnimator;
    private Animator emenyAnimator;
    private Rigidbody2D rb;
    public Boundary boundary;
    public int hp;
    public Text hptext;
    public Image hpimage;
    private PlayerController gameController;
    private PauseGame pause;
    public float speed;
    public bool isDie = false;
    public int aggressivity; //攻击力
    public float hp_max;
    public float hp_min;
    public string s;
    public int count;
    private int count_del;

    void Awake () {
        emenyAnimator = GetComponent<Animator> ();
        myAnimator = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        rb = GetComponent<Rigidbody2D> ();
    }

    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag ("Player");
        gameController = gameControllerObject.GetComponent<PlayerController> ();
        GameObject gameControllerObject1 = GameObject.FindWithTag ("Pause");
        pause = gameControllerObject1.GetComponent<PauseGame> ();
        count_del = count;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (pause.isPause == false) {
            if (gameController.isDie == true)
                emenyAnimator.SetBool ("isIdle", true);
            if (isDie == false && gameController.isDie == false) {
                if (hp > hp_min) {
                    hpimage.fillAmount = hp / hp_max;
                    hptext.text = hp.ToString () + s;
                    Vector3 dir = player.position - this.transform.position;

                    if (dir.x > 2.0f || dir.x < -2.0f || dir.y > 0.3f || dir.y < -0.3f) {
                        //用Vector3.Lerp移动
                        transform.position = Vector3.Lerp (transform.position, player.position, speed);
                        emenyAnimator.SetBool ("isAttack", false);
                    } else {
                        emenyAnimator.SetBool ("isAttack", true);
                        if (gameController.isDefense == false)
                            count -= aggressivity;
                        if (count == 0) {
                            gameController.hp -= count_del;
                            count = count_del;
                        }

                    }

                    //敌人始终朝向主角
                    if (dir.x < 0) {
                        transform.localEulerAngles = new Vector3 (0.0f, 180.0f, 0.0f);
                    } else {
                        transform.localEulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
                    }
                    if (dir.y > 0) {
                        transform.localPosition = new Vector3 (transform.position.x, transform.position.y, -0.15f);
                    } else {
                        transform.localPosition = new Vector3 (transform.position.x, transform.position.y, -0.05f);
                    }
                } else {
                    hpimage.fillAmount = hp_min / hp_max;
                    hptext.text = hp_min.ToString () + s;
                    isDie = true;
                    if (hp_min == 0) {
                        emenyAnimator.SetBool ("isAttack", false);
                        emenyAnimator.SetTrigger ("Die");
                        Invoke ("end_game", 0.0f);
                    } else {
                        Invoke ("end_game1", 0.0f);
                    }

                }
            }
        }

    }

    public void end_game () {
        Destroy (this.gameObject, 2.5f);
    }

    public void end_game1 () {
        Destroy (this.gameObject);
    }
}