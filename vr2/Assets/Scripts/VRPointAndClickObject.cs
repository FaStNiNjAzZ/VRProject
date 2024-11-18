using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

public class VRPointAndClickObject : MonoBehaviour
{
    public TMP_Text sun_Information_Text;

    private void Start()
    {
        SunCanvasUIInformation(0, 0, 0, 0, 0);
    }

    // Pointing at an object
    private void OnEnable()
    {
        //GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnSelectEntered);
    }

    // Pointing away from object
    private void OnDisable()
    {
        //GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnSelectEntered);
    }

    // Do the thing
    void OnSelectEntered(SelectEnterEventArgs args)
    {
        
    }

    void SunCanvasUIInformation(double source_ID, double distance, double mass, double gravitational_Force, double diameter)
    {
        if (source_ID == 0 || distance == 0 || mass == 0 || gravitational_Force == 0 || diameter == 0) 
        {
            sun_Information_Text.text = "ID: NULL\r\nDistance Away: NULL\r\nDiameter: NULL\r\nMass: NULL\r\nGravitational Force: NULL";
        }

        sun_Information_Text.text = "ID: " + source_ID + "\r\nDistance Away:" + distance + "\r\nDiameter:" + diameter + "\r\nMass:" + mass + "\r\nGravitational Force:" + gravitational_Force;
    }
}
