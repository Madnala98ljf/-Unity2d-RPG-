using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{  
    public GameObject game;

    public void Retry() {
        game.SetActive(false);
    }
}
