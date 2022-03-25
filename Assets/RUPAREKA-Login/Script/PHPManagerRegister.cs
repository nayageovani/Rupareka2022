using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(PHPHelper))]

public class PHPManagerRegister : MonoBehaviour
{
    private PHPHelper phphelper;
    [Header("Register Setting"), Space(5)]
    public TMP_InputField nameRegisterField;
    public TMP_InputField passwordRegisterField;
    public Button registerButton;

    [Header("Panel"), Space(5)]
    [SerializeField] UnityEvent onRegisterSuccess;
    private bool isLogin;
    public Text Textfield;
    

    void Start()
    {

        isLogin = false;
        phphelper = GetComponent<PHPHelper>();
    }

    public void SetText()
    {
        Textfield.text = "username already exist";
    }


    public void Register()
    {
        phphelper.CallRegister(nameRegisterField.text, passwordRegisterField.text, (string success) =>
        {
            char[] texted = success.ToCharArray();
            if (texted[0] == '0')
            {
                DBManager.username = nameRegisterField.text;
                Debug.Log(DBManager.username);
                onRegisterSuccess.Invoke();
            }
            else
            {
                Debug.Log($"user login failed. error # {success}");
                SetText();
            }
        }, (string failed) =>
        {
            Debug.Log(failed);
        });
    }


    public void VerifyRegisterInputs()
    {
        registerButton.interactable = (nameRegisterField.text.Length >= 1 && passwordRegisterField.text.Length >= 1);
    }

}
