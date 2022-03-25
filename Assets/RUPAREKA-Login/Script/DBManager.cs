using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    // Start is called before the first frame update
    public static string username;
    public static int score;

    public static bool LoggedIn {  get {  return username != null; } }

    public static void LogOut()
    {
        username = null;
    }
}
