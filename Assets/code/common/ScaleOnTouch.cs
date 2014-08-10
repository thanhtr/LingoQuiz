using UnityEngine;
using System.Collections;

public class ScaleOnTouch : MonoBehaviour 
{
	public float scaleToSize = 0.9f;
	public float time = 0.15f;
	public iTween.EaseType easing = iTween.EaseType.easeOutQuad;		
	
	private bool touched = false;
	private Vector3 initialSize;
	private InputDetector inputDetector;

	// Use this for initialization
	void Start () 
	{
		inputDetector = GetComponent<InputDetector>();
		inputDetector.OnTouchReleaseAnywhere += OnTouchReleaseAnywhere;
		inputDetector.OnTouch += OnTouch;

		initialSize = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnTouchReleaseAnywhere(InputDetectorTouch touch)
	{
		if (touched == true)
		{
		    ScaleOnReleased();
		}
		touched = false;
	}
	
	public void OnTouch(InputDetectorTouch touch)
	{	
		touched = true;
		ScaleTouch();
	}
	
	void ScaleTouch()	
	{
		Hashtable param = new Hashtable();
  		param.Add("scale", new Vector3(scaleToSize, scaleToSize, 1f));
  		param.Add("time", this.time);
 		param.Add("easetype", this.easing);
  		iTween.ScaleTo(this.gameObject, param);
	}
	
	void ScaleOnReleased()	
	{
		Hashtable param = new Hashtable();
  		param.Add("scale", this.initialSize);
  		param.Add("time", this.time);
 		param.Add("easetype", iTween.EaseType.linear);
  		iTween.ScaleTo(this.gameObject, param);
	}	
}
