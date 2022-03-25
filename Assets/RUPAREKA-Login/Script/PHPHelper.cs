using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;


public class PHPHelper : MonoBehaviour
{

    [SerializeField] string loginurl = "https://cubestudio.id/PHP/kominfo/login.php";
    [SerializeField] string registerurl = "https://cubestudio.id/PHP/kominfo/register.php";


    public void CallLogin(string username, string password, Action<string> onSuccess, Action<string> onFailed)
    {
        StartCoroutine(LoginPlayer(username, password, onSuccess, onFailed));
    }
    IEnumerator LoginPlayer (string username, string password, Action<string> onSuccess, Action<string> onFailed)
    {
        string encryptPass = Enkripsi.DESEncryption(password);
        

        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("password", encryptPass);
        
        using (UnityWebRequest www = UnityWebRequest.Post(loginurl, form))
        {
            yield return www.SendWebRequest();

            switch (www.result)
            {
                case UnityWebRequest.Result.ProtocolError:
                    onFailed(www.error);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    onFailed(www.error);
                    break;
                case UnityWebRequest.Result.Success:
                    onSuccess(www.downloadHandler.text);
                    break;
            }


        }
    }

    public void CallRegister(string username, string password, Action<string> onSuccess, Action<string> onFailed)
    {
        StartCoroutine(Register(username, password, onSuccess, onFailed));
        
    }

    IEnumerator Register(string username, string password, Action<string> onSuccess, Action<string> onFailed)
    {
        string encryptPass = Enkripsi.DESEncryption(password);

        WWWForm form = new WWWForm();
        form.AddField("name", username);
        form.AddField("password", encryptPass);
        Debug.Log(username + "" + encryptPass);
        using (UnityWebRequest www = UnityWebRequest.Post(registerurl, form))
        {
            yield return www.SendWebRequest();

            switch (www.result)
            {
                case UnityWebRequest.Result.ProtocolError:
                    onFailed(www.error);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    onFailed(www.error);
                    break;
                case UnityWebRequest.Result.Success:
                    onSuccess(www.downloadHandler.text);
                    break;
            }

            //if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
            //{
            //    Debug.Log(www.error);
            //}
            //else
            //{
            //    Debug.Log($"Register Sukses dengan nama : {nameField.text}");
            //    UnityEngine.SceneManagement.SceneManager.LoadScene("mainmenu");
            //}
        }
    }

}
