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
    public Transform target;


    private void Start() 
    {
        cam = GetComponent<Camera>();
        startingFOV = cam.fieldOfView;
        // transform.Rotate(0f, -90f, 0f);
        Transform FPSController = gameObject.GetComponentInParent<Transform>();
        FPSController.LookAt(target);
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