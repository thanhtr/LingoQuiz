using UnityEngine;
using System.Collections;

public class NextQuestionButton : Button 
{
	private QuizMainView main;

	// Use this for initialization
	void Start () {
		InitButton();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OnButtonPressAndRelease(InputDetectorTouch touch)
	{
		Debug.Log("next press");
		if (main == null)
		{
			main = FindObjectOfType<QuizMainView>();
		}
		main.SendMessage("OnNextButtonPressed", this, SendMessageOptions.DontRequireReceiver);
	}
}
