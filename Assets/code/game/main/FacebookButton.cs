using UnityEngine;
using System.Collections;

public class FacebookButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<InputDetector>().OnTouchAndRelease += OnTouchAndRelease;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchAndRelease(InputDetectorTouch touch)
    {
        Application.OpenURL("https://www.facebook.com/lingoteach");
    }
}
