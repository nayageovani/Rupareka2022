using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text playerDisplay;

    private void Start()
    {
        if (playerDisplay == null) return;

        if (DBManager.LoggedIn) 
        {
            playerDisplay.text = "Selamat Datang " + DBManager.username;
        }
    }
    
    public void GoToLoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
