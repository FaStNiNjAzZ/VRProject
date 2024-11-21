using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceLoaderScript : MonoBehaviour
{
    public Camera cam;
    public BCStarLoader starLoader; // Assuming this is your star loading script
    public float DistancefromCamera; // Maximum distance from the camera before unloading stars

    private List<GameObject> stars; // To store references to star GameObjects

    private void Start()
    {
        stars = starLoader.stars;
    }

    public void FixedUpdate()
    {
        foreach (GameObject star in stars)
        {
            StarGameObject starScript = star.GetComponent<StarGameObject>();
            if (star != null) 
            {
                // Calculate distance from camera
                float distance = Vector3.Distance(cam.transform.position, star.transform.position);

                // Check if the star should be active or not
                starScript.SetDistance(distance, DistancefromCamera);
            }
        }
    }
}
