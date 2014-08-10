using UnityEngine;
using System.Collections;

public class SampleViewLayout : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Camera cam = GetComponentInChildren<Camera>();
		if (cam != null)
		{
			cam.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
