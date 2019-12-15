using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordButton : MonoBehaviour {

    // 输入框 和 Toggle 的参数
    public InputField inputField;
    private bool isLook;
    public Sprite sprit1;
    public Sprite sprit2;
    public Image image;

    void Start () {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick() {
        inputField.contentType = isLook ? InputField.ContentType.Standard : InputField.ContentType.Password;
        inputField.Select ();
        if(isLook==false){
            image.sprite = sprit2;
            isLook=true;
        }else
        {
            image.sprite = sprit1;
            isLook=false;
        }
    }

}