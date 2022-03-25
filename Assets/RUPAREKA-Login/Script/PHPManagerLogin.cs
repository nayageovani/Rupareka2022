using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(PHPHelper))]

public class PHPManagerLogin : MonoBehaviour
{
    private PHPHelper phphelper;
    [Header("Login Setting"), Space(5)]
    public TMP_InputField nameLoginField;
    public TMP_InputField passwordLoginField;
    public Button loginButton;
   
    [Header("Panel"), Space(5)]
    [SerializeField] UnityEvent onLoginSuccess;
    private bool isLogin;
    public Text Textfield;
    public GameObject Complete;

    void Start()
    {

        isLogin = false;
        phphelper = GetComponent<PHPHelper>();
    }

    public void SetText()
    {
        Textfield.text = "username or password is incorrect";
    }

    public void Login()
    {
        phphelper.CallLogin(nameLoginField.text, passwordLoginField.text, (string success) =>
        {
            char[] texted = success.ToCharArray();
            if (texted[0] == '0')
            {
                DBManager.username = nameLoginField.text;
                Debug.Log(success);
                onLoginSuccess.Invoke();

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


    public void VerifyLoginInputs()
    {
        loginButton.interactable = (nameLoginField.text.Length >= 1 && passwordLoginField.text.Length >= 1);
    }

}
