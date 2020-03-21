using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class PauseGame : MonoBehaviour
{
    public bool isPause = false;
    public GameObject game_pause;
    public GameObject game_back;
    public GameObject[] game;
    public Text value_sum;
    public Text value_sum2;
    public Slider slider;
    public Slider slider2;
    public AudioSource asound;
    public AudioSource[] Gameasound;

    void Awake()
    {
        slider.onValueChanged.AddListener(onSlider); //当slider数值变化时，回调onSlider方法
        slider2.onValueChanged.AddListener(onSlider02);

        float BackAsound;
        if (PlayerPrefs.HasKey(PlayerPrefsConst.BackAsound))
            BackAsound = PlayerPrefs.GetFloat(PlayerPrefsConst.BackAsound);
        else
            BackAsound = Variable.BackAsound;
        float GameAsound;
        if (PlayerPrefs.HasKey(PlayerPrefsConst.GameAsound))
            GameAsound = PlayerPrefs.GetFloat(PlayerPrefsConst.GameAsound);
        else
            GameAsound = Variable.GameAsound;
        value_sum.text = Math.Floor(BackAsound * 100).ToString();
        value_sum2.text = Math.Floor(GameAsound * 100).ToString();
        slider.value = BackAsound;
        slider2.value = GameAsound;
        asound.volume = BackAsound;
        for (int i = 0; i < Gameasound.Length; i++)
        {
            Gameasound[i].volume = GameAsound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Variable.CanPaues)
        {
            if (isPause == false)
            {
                Time.timeScale = 0;
                isPause = true;
                Variable.IsPause = true;
                game_pause.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                isPause = false;
                Variable.IsPause = false;
                game_pause.SetActive(false);
                PlayerPrefs.SetFloat(PlayerPrefsConst.BackAsound, Variable.BackAsound);
                PlayerPrefs.SetFloat(PlayerPrefsConst.GameAsound, Variable.GameAsound);
                PlayerPrefs.Save();
            }
        }
    }

    void onSlider(float value)
    {
        value_sum.text = Math.Floor(value * 100).ToString();
        Variable.BackAsound = value;
    }

    void onSlider02(float value)
    {
        value_sum2.text = Math.Floor(value * 100).ToString();
        Variable.GameAsound = value;
    }

    public void Con_sound()
    {
        asound.volume = slider.value;
    }

    public void Game_sound()
    {
        for (int i = 0; i < Gameasound.Length; i++)
        {
            Gameasound[i].volume = slider2.value;
        }
    }

    public void click_main()
    {
        for (int i = 0; i < game.Length; i++)
            game[i].SetActive(false);
        Variable.IsPause = false;
        game_back.SetActive(true);
        PlayerPrefs.SetFloat(PlayerPrefsConst.BackAsound, Variable.BackAsound);
        PlayerPrefs.SetFloat(PlayerPrefsConst.GameAsound, Variable.GameAsound);
        PlayerPrefs.Save();
        Time.timeScale = 1;
    }
}