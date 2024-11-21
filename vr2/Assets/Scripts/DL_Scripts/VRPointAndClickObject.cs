using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;
using UnityEngine.InputSystem;
using static VRPointAndClickObject;
using System;

public class VRPointAndClickObject : MonoBehaviour
{
    // Sun Information Panel Variables
    public TMP_Text sun_Information_Text;

    // Raycast Related Variables
    public LineRenderer line_Renderer;
    public LayerMask interactable_Layer;
    RaycastHit hit;
    const float MAX_DISTANCE = 100000000000f;
    bool isWorking = false;


    float trigger_Value;
    public InputActionProperty trigger_Action;
    


    private void Start()
    {
        SunCanvasUIInformation(null, null, 0);
        line_Renderer = GetComponent<LineRenderer>();

        if (line_Renderer == null)
        {
            Debug.LogError("LineRenderer not assigned!");
            return;
        }

        // Set up LineRenderer
        line_Renderer.positionCount = 2;
        line_Renderer.startWidth = 0.2f;
        line_Renderer.endWidth = 0.2f;

        // Set positions
        line_Renderer.SetPosition(0, transform.position); // Start point
        line_Renderer.SetPosition(1, transform.position + transform.forward * MAX_DISTANCE);
    }

    public interface ILineRenderer
    {
        void SetPositions(Vector3[] positions);
        void SetWidth(float startWidth, float endWidth);
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, MAX_DISTANCE, interactable_Layer))
        {
            StarGameObject star_Game_Object = hit.collider.GetComponent<StarGameObject>();
            if (star_Game_Object != null)
            {
                SunCanvasUIInformation(star_Game_Object.name, star_Game_Object.starType, star_Game_Object.distance);
                //Debug.Log("Name: " + star_Game_Object.name);
                //Debug.Log("Cluster Type: " + star_Game_Object.starType);
                //Debug.Log("Distance: " + star_Game_Object.distance);
            }

        }
        //Debug.Log(trigger_Value);
        ////line_Renderer.SetPosition(0, transform.position);
        ////line_Renderer.SetPosition(1, transform.position + transform.forward * MAX_DISTANCE);
        //trigger_Value = trigger_Action.action.ReadValue<float>();
        //if (trigger_Value > 0.1f)
        //{
        //    if (Physics.Raycast(transform.position, transform.forward, out hit, MAX_DISTANCE, interactable_Layer))
        //    {
        //        StarGameObject star_Game_Object = hit.collider.GetComponent<StarGameObject>();
        //        if (star_Game_Object != null)
        //        {
        //            SunCanvasUIInformation(star_Game_Object.name, star_Game_Object.starType, star_Game_Object.distance);
        //            //Debug.Log("Name: " + star_Game_Object.name);
        //            //Debug.Log("Cluster Type: " + star_Game_Object.starType);
        //            //Debug.Log("Distance: " + star_Game_Object.distance);
        //        }

        //    }
        //}
        else
        {
            line_Renderer.SetPosition(1, transform.position + transform.forward * MAX_DISTANCE); // Default to max distance
        }


        
    }

    void SunCanvasUIInformation(string source_ID, string type, float distance)
    {
        if (source_ID == null || distance == 0 || type == null)
        {
            sun_Information_Text.text = "ID: NULL\r\nType: NULL\r\nDistance Away: NULL";
        }
        else 
        {
            sun_Information_Text.text = "ID: " + source_ID + "\r\nType:" + type + "\r\nDistance:" + distance;
        }

        
    }
}
