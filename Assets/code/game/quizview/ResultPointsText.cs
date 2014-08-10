using UnityEngine;
using System.Collections;

public class ResultPointsText : MonoBehaviour 
{
	private tk2dTextMesh textMesh;
	private int points;

	// Use this for initialization
	void Start () {
		textMesh = GetComponentInChildren<tk2dTextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPoints(int _points)
	{
		points = _points;
		textMesh.text = points.ToString();
	}

	public void AnimateToZero(float time)
	{
		iTweenParams p = new iTweenParams();
		p.from = (float)points;
		p.to = 0f;
		p.easing = iTween.EaseType.linear;
		p.time = time;
		p.onUpdate = "OnTweenUpdate";
		p.onUpdateTarget = gameObject;
		p.onComplete = "OnComplete";
		p.onCompleteTarget = gameObject;
		iTween.ValueTo(gameObject, p.ToHash());
	}

	private void OnTweenUpdate(float newPoints)
	{
		int p = (int)newPoints;
		textMesh.text = p.ToString();
	}

	private void OnComplete()
	{
		textMesh.text = points.ToString();
		textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.2f);
	}

}
