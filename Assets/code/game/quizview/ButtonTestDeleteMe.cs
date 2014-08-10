using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonTestDeleteMe : MonoBehaviour {

	private AnswerButton[] buttons;

	// Use this for initialization
	void Start () 
	{
		buttons = (AnswerButton[]) FindObjectsOfType(typeof(AnswerButton));
		Debug.Log("count;"+buttons.Length);
		foreach (AnswerButton button in buttons)
		{
			button.OnAnswerClicked += OnAnswerClicked;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnAnswerClicked(AnswerButton button)
	{
		Debug.Log("Pressed: "+button.Text());
	}
}
