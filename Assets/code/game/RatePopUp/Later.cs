using UnityEngine;
using System.Collections;

public class Later : MonoBehaviour 
{
	public delegate void LaterButtonEvents();
	public event LaterButtonEvents OnLaterPressed;

	// Use this for initialization
	void Start () {
        GetComponent<InputDetector>().OnTouchAndRelease += OnTouchAndRelease;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTouchAndRelease(InputDetectorTouch touch)
    {
		if (OnLaterPressed != null)
		{
			OnLaterPressed();
		}
    }
}
