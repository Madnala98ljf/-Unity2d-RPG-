using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Serialization;

public class LoadSystem : MonoBehaviour
{
    public InputField text_Username;
    public InputField text_Password;
    public InputField text_Confirm;
    public InputField text_Code;
    public GameObject game;
    public GameObject errorRegister;
    public Text LoginText;
    public Text ErrorText;

    public Button LoginBtn;
    public Button RegisterBtn;
    public Button ForgetBtn;
    public Button ChangeBtn;

    private void Awake()
    {
        LoginBtn.onClick.AddListener(this.Load_click);
        RegisterBtn.onClick.AddListener(this.Register);
        ForgetBtn.onClick.AddListener(this.ForgetPassword);
        ChangeBtn.onClick.AddListener(this.ShowPassword);

        if (!PlayerPrefs.HasKey(PlayerPrefsConst.FirstLoginGame))
        {
            //首次登录
            text_Confirm.gameObject.SetActive(true);
            text_Code.gameObject.SetActive(true);

            RegisterBtn.gameObject.SetActive(false);
            ForgetBtn.gameObject.SetActive(false);
            LoginText.text = "注册";
        }
        else
        {
            //非首次登录
            text_Confirm.gameObject.SetActive(false);
            text_Code.gameObject.SetActive(false);
            RegisterBtn.gameObject.SetActive(true);
            ForgetBtn.gameObject.SetActive(true);
            LoginText.text = "登录";
        }
    }

    public void Load_click()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsConst.FirstLoginGame))
        {
            //首次登录
            if (CanRegister())
            {
                PlayerPrefs.SetString(PlayerPrefsConst.FirstLoginGame, "success");
                PlayerPrefs.SetString(PlayerPrefsConst.Username, text_Username.text);
                PlayerPrefs.SetString(PlayerPrefsConst.Passwoord, text_Password.text);
                PlayerPrefs.SetString(PlayerPrefsConst.SafeCode, text_Code.text);
                game.SetActive(true);
            }
            else
            {
                SetTextNUll();
                ErrorText.text = "注册失败n用户名和密码长度要大于七位且小于十二位，n且不能含有特殊字符".Replace('n', '\n');
                errorRegister.SetActive(true);
            }
        }
        else
        {
            //非首次登录
            if (text_Username.text == PlayerPrefs.GetString(PlayerPrefsConst.Username) &&
                text_Password.text == PlayerPrefs.GetString(PlayerPrefsConst.Passwoord))
            {
                game.SetActive(true);
            }

            else
            {
                SetTextNUll();
                ErrorText.text = "登录失败n请检查输入是否正确".Replace('n', '\n');
                errorRegister.SetActive(true);
            }
        }
    }

    public bool CanRegister()
    {
        //正则表达式
        Regex rex = new Regex(@"^[\u4E00-\u9FA5A-Za-z0-9]+$");
        var result1 = rex.Match(text_Username.text);
        var result2 = rex.Match(text_Password.text);
        if (text_Username.text.Length < 7 || !result1.Success || text_Username.text.Length > 11)
        {
            return false;
        }
        else if (text_Password.text.Length < 7 || !result2.Success || text_Password.text.Length > 11)
        {
            return false;
        }
        else if (!text_Password.text.Equals(text_Confirm.text))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Register()
    {
        RegisterBtn.gameObject.SetActive(false);
        ForgetBtn.gameObject.SetActive(false);
        PlayerPrefs.DeleteAll();
        text_Username.gameObject.SetActive(true);
        text_Password.gameObject.SetActive(true);
        text_Confirm.gameObject.SetActive(true);
        text_Code.gameObject.SetActive(true);
        LoginBtn.gameObject.SetActive(true);
        ChangeBtn.gameObject.SetActive(false);
        LoginText.text = "注册";

        SetTextNUll();
    }

    public void ForgetPassword()
    {
        SetTextNUll();
        ForgetBtn.gameObject.SetActive(false);
        text_Username.gameObject.SetActive(false);
        text_Password.gameObject.SetActive(false);
        text_Confirm.gameObject.SetActive(false);
        LoginBtn.gameObject.SetActive(false);
        text_Code.gameObject.SetActive(true);
        ChangeBtn.gameObject.SetActive(true);
    }

    private void ShowPassword()
    {
        if (text_Code.text.Equals(PlayerPrefs.GetString(PlayerPrefsConst.SafeCode)))
        {
            //安全检验通过
            SetTextNUll();
            ErrorText.text = ("用户名：" + PlayerPrefs.GetString(PlayerPrefsConst.Username) + "n" + "密  码：" +
                              PlayerPrefs.GetString(PlayerPrefsConst.Passwoord)).Replace('n', '\n');
            errorRegister.SetActive(true);
            text_Username.gameObject.SetActive(true);
            text_Password.gameObject.SetActive(true);
            text_Confirm.gameObject.SetActive(false);
            LoginBtn.gameObject.SetActive(true);
            text_Code.gameObject.SetActive(false);
            ChangeBtn.gameObject.SetActive(false);

            ForgetBtn.gameObject.SetActive(true);
            RegisterBtn.gameObject.SetActive(true);
        }
        else
        {
            //安全检验失败
            SetTextNUll();
            ErrorText.text = "安全密匙不正确";
            errorRegister.SetActive(true);
        }
    }

    private void SetTextNUll()
    {
        text_Username.text = null;
        text_Password.text = null;
        text_Confirm.text = null;
        text_Code.text = null;
    }
}