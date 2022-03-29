using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class OpenNewTab : MonoBehaviour
{
    public void OpenURL(string url)
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
        OpenTab(url);
        #endif
    }

    [DllImport("__Internal")]
    private static extern void OpenTab(string url);
}
