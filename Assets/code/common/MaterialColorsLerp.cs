using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialColorsLerp : MonoBehaviour {

	[System.Serializable]
	public class ColorTweenProperties
	{
		public float speed;
		public float duration;
	}
	
	public Material material;
	public ColorTweenProperties tweenProperties;

	private bool isLerping = false;
	private Color newColor;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isLerping)
		{
			material.color = Color.Lerp(material.color, newColor, tweenProperties.speed * Time.deltaTime);
		}
	}

	public void ChangeToColor(Color color)
	{
		newColor = color;
		isLerping = true;

		CancelInvoke("StopLerp");
		Invoke("StopLerp", tweenProperties.duration);
	}

	public void InstantChangeColor(Color color)
	{
		CancelInvoke("StopLerp");
		isLerping = false;
		material.color = color;
	}

	private void StopLerp()
	{
		isLerping = false;
	}
}
