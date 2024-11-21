using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static GaiaSkyLoader;

public class StarGameObject : MonoBehaviour
{
    public string StarName;
    public string starType;
    private string starcolour;
    public string starAge;
    public float radius;
    public float distance;
    public float distanceFromCamera;

    public Renderer renderer;
    public GameObject particleGameobject;



    public void SetValues(string newstarAge, float newradius, string newstarType,float newdistance)
    {
        radius = newradius; starAge = newstarAge; starType = newstarType; distance = newdistance;
    }
    void Start()
    {
        particleGameobject.SetActive(false);
    }
    public void SetDistance(float newDistance, float renderDist)
    {
        distanceFromCamera = newDistance;

        if (renderDist > distanceFromCamera)
        {
            renderer.enabled = false;
            particleGameobject.SetActive(true);
        }
        else if (renderDist <= distanceFromCamera)
        {
            renderer.enabled = true;
            particleGameobject.SetActive(false);
        }
    }
}
