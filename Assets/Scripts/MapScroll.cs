using UnityEngine;
 using System.Collections;
 
 public class MapScroll : MonoBehaviour 
 
 {
    private Camera cam;
    private float startingFOV;

    public float minFOV;
    public float maxFOV;
    public float zoomRate;

    private float currentFOV;


    private void Start() 
    {
        cam = GetComponent<Camera>();
        startingFOV = cam.fieldOfView;
    }

    private void Update() 
    {
        currentFOV = cam.fieldOfView;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentFOV -= zoomRate;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            currentFOV += zoomRate;
        }

        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            currentFOV = startingFOV;
        }

        currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        cam.fieldOfView = currentFOV;

    }

}