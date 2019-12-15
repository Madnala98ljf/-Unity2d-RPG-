using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class PauseGame : MonoBehaviour
{
    public bool isPause=false;
    public GameObject game_pause;
    public GameObject game_back;
    public GameObject[] game;
    public Text value_sum;
    public Text value_sum2;
    public Slider slider;
    public Slider slider2;
    public AudioSource asound;
    public AudioSource[] Gameasound;

    void Start () {
        slider.onValueChanged.AddListener(onSlider);    //当slider数值变化时，回调onSlider方法
        slider2.onValueChanged.AddListener(onSlider02);
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isPause==false){
                Time.timeScale = 0;
                isPause=true;
                game_pause.SetActive(true);
            }else{
                Time.timeScale = 1;
                isPause=false;
                game_pause.SetActive(false);
            }
        }
    }

    void onSlider(float value)
    {
        value_sum.text = Math.Floor(value*100).ToString();
    }

    void onSlider02(float value)
    {
        value_sum2.text = Math.Floor(value*100).ToString();
    }

    public void  Con_sound(){
        asound.volume=slider.value;
    }

    public void  Game_sound(){
        for(int i=0;i<Gameasound.Length;i++){
            Gameasound[i].volume=slider2.value;
        }
    }

    public void click_main(){
        for(int i=0;i<game.Length;i++)
            game[i].SetActive(false);
        game_back.SetActive(true);
        Time.timeScale = 1;
    }
}
