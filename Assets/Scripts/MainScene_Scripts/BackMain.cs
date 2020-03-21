using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMain : MonoBehaviour
{
    public GameObject game;
    public GameObject game1;

    public void Back_click()
    {
        SceneManager.LoadScene("main");
    }

    public void Member_click()
    {
        SceneManager.LoadScene("Member");
    }

    public void GameBackGround_click()
    {
        SceneManager.LoadScene("GameBackGround");
    }

    public void End_game()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }

    public void GameFirst_game()
    {
        game1.SetActive(false);
        game.SetActive(true);
    }

    public void Operation_game()
    {
        SceneManager.LoadScene("instructions");
    }
}