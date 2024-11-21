using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public string source_ID;

    public void OnClick()
    {
        Debug.Log("ID: " + source_ID);
    }
}
