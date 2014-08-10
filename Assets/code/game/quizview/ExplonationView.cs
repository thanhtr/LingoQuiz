using UnityEngine;
using System.Collections;

public class ExplonationView : MonoBehaviour 
{
	public tk2dTextMesh bigTitle;
	public tk2dTextMesh title;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetText(string text)
	{
		title.text = text;
	}

	public void SetBigTitle(string text)
	{
		bigTitle.text = text;
	}

}
