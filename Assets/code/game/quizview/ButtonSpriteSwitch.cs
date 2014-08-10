using UnityEngine;
using System.Collections;
using tk2dRuntime;

public class ButtonSpriteSwitch : MonoBehaviour
{

    public Transform normal;
    public Transform pressed;
    private MeshRenderer meshNormal;
    private MeshRenderer meshPressed;
    // Use this for initialization
    void Start()
    {
        meshNormal = normal.GetComponent<MeshRenderer>();
        meshPressed = pressed.GetComponent<MeshRenderer>();
        meshNormal.enabled = true;
        meshPressed.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        tk2dUIItem uiNormal = transform.GetComponent<tk2dUIItem>();
        uiNormal.OnDown += HideRenderer;
        uiNormal.OnUp += ShowRenderer;
    }

    private void HideRenderer()
    {
        meshNormal.enabled = false;
    }
    private void ShowRenderer()
    {
        meshNormal.enabled = true;
    }
}
