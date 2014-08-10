using UnityEngine;
using System.Collections;

public class LoadingText : MonoBehaviour 
{
	public string text;
	public float speed;
	private tk2dTextMesh textMesh;
	private int count;

	// Use this for initialization
	void Start () 
	{
		textMesh = GetComponentInChildren<tk2dTextMesh>();
		count = 0;
		InvokeRepeating("UpdateText", speed, speed);
	}

	void UpdateText()
	{
		string dots = "";
		if (count == 1) dots = ".";
		else if (count == 2) dots = "..";
		else if (count == 3) dots = "...";
		else count = 0;
		count++;

		textMesh.text = text  + dots;
	}

	public void Hide()
	{
		CancelInvoke("UpdateText");
		textMesh.renderer.enabled = false;
	}
}
