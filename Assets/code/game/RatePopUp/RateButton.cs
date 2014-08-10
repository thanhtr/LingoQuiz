using UnityEngine;
using System.Collections;

public class RateButton : MonoBehaviour
{
	public delegate void RateButtonEvents();
	public event RateButtonEvents OnRatePressed;

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
		if (OnRatePressed != null)
		{
			OnRatePressed();
		}
    }
}
