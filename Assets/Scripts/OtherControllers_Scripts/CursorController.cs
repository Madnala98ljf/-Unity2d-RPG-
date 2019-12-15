using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    #pragma strict
    public Texture2D cursorTexture;
    private bool changeFlag = true;
    
    // Update is called once per frame
    void Update()
    {
        if(changeFlag){
            Cursor.SetCursor(cursorTexture,Vector2.zero,CursorMode.Auto);
        }else{
            Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        }
    }
}
