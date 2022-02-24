using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField, Range(0f, 1f)] float breathingTime;
    [SerializeField] Color[] colors;
    float time;
    int len;
    int colorIndex;
    
    void Start()
    {
        len = colors.Length;
        colorIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        light.color = Color.Lerp(light.color, colors[colorIndex], breathingTime * Time.deltaTime);
        time = Mathf.Lerp(time, 1f, breathingTime * Time.deltaTime);
        if (time>.9f)
        {
            time = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= colors.Length) ? 0 : colorIndex;
        }
    }
}
