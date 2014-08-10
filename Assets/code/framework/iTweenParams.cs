using UnityEngine;
using System.Collections;

public class iTweenParams  
{
	public iTween.EaseType easing = iTween.EaseType.linear;
	public Vector3 position;
	public bool isLocal = false;
	public Vector3 scale;
	public float time = 0f;
	public float speed = 0f;
	public Vector3 amount;
	public string onComplete = "";
	public string onUpdate = "";
	public string name = "";
	public float from = 0f;
	public float to = 0f;
	public Color toColor = Color.black;
	public Color fromColor = Color.black;
	public string namedColorValue = "";
	public iTween.LoopType loopType = iTween.LoopType.none;
	public GameObject onCompleteTarget = null;
	public GameObject onUpdateTarget = null;
	
	public Hashtable ToHash()
	{
		Hashtable param = new Hashtable();		
		
		if (this.name != "")
		{
			param.Add ("name", this.name);
		}
		if (this.scale != null)
		{
			param.Add("scale", this.scale);
		}
		if (this.position != null)
		{
			param.Add("position", this.position);
		}	
		if (this.amount != null)
		{
			param.Add("amount", this.amount);
		}
		if (this.speed != 0f)
		{
			param.Add("speed", this.speed);
		}
		if (this.time != 0f)
		{
			param.Add("time", this.time);
		}
		if (this.isLocal)
		{
			param.Add("islocal", this.isLocal);
		}
		if (this.onComplete != "")
		{
			param.Add("oncomplete", this.onComplete);
			param.Add("oncompletetarget", this.onCompleteTarget);
		}
		if (this.from != 0f || this.to != 0f)
		{
			param.Add("from", this.from);
			param.Add("to", this.to);
		}
		if (this.toColor != Color.black && this.fromColor != Color.black)
		{
			param.Add("to", this.toColor);
			param.Add("from", this.fromColor);
		}
		if (this.onUpdate != "")
		{
			param.Add("onupdate", this.onUpdate);
			param.Add("onupdatetarget", this.onUpdateTarget);
		}
		if (this.namedColorValue != "")
		{
			param.Add("NamedColorValue", this.namedColorValue);
		}
			
		param.Add("easetype", this.easing);
		param.Add("looptype", this.loopType);
		
		return new Hashtable(param);
	}
	
}
