using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpButton : MonoBehaviour
{
    public GameObject help;
    
    public void Help_click(){
        help.SetActive(true);
    }
}
