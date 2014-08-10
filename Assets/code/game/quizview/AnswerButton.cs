using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnswerButton : MonoBehaviour
{
	public delegate void AnswerButtonEvents(AnswerButton answerButton);
	public event AnswerButtonEvents OnAnswerClicked;
	
	[SerializeField] private MultiFontText buttonText;
	[SerializeField] private InputDetector inputDetector;
	[SerializeField] private tk2dSprite buttonSprite;
	
	[HideInInspector] public string answerId;

	private bool isPressed = false;
	private bool initialized = false;
	private Color origSpriteColor;
	
	void Start()
	{
		inputDetector.OnTouch += OnTouch;
		inputDetector.OnTouchReleaseAnywhere += OnTouchReleaseAnywhere;
		inputDetector.OnTouchAndRelease += OnSelected;

		origSpriteColor = buttonSprite.color;

		SwitchButtonPressed(isPressed);
	}

	public void InitAnswer(string id, string text)
	{
		answerId = id;
		buttonText.SetText(text);
	}
	
	public string Text()
	{
		return buttonText.GetText();
	}
	
	public string AnswerId()
	{
		return answerId;
	}

	public void DisableButton()
	{
		collider.enabled = false;
	}
	
	public void PlayHideButton()
	{
		iTweenParams p = new iTweenParams();
		p.time = 0.35f;
		p.from = 1f;
		p.to = 0.55f;
		p.onUpdate = "OnSpriteFadeOutUpdate";
		p.onUpdateTarget = gameObject;

		iTween.ValueTo(gameObject, p.ToHash());
	}

	private void OnSpriteFadeOutUpdate(float newValue)
	{
		buttonSprite.color = new Color(origSpriteColor.r * newValue, 
		                         		origSpriteColor.g * newValue, 
		                         		origSpriteColor.b * newValue,
		                               origSpriteColor.a * newValue);

		buttonText.SetColor(buttonSprite.color);
	}

	private void OnTouch(InputDetectorTouch touch)
	{
		isPressed = true;
		SwitchButtonPressed(isPressed);
	}
	
	private void OnTouchReleaseAnywhere(InputDetectorTouch touch)
	{
		if (isPressed)
		{
			SwitchButtonPressed(false);
		}
		isPressed = false;
	}

	private void OnSelected(InputDetectorTouch touch)
	{
		if (OnAnswerClicked != null)
		{
			OnAnswerClicked(this);
		}
	}

	private const float darkenPercentage = 0.87f;
	private void SwitchButtonPressed(bool press)
	{
		float colorPercentage = press ? darkenPercentage : 1f; 
		buttonSprite.color = new Color(origSpriteColor.r * colorPercentage, 
		                               origSpriteColor.g * colorPercentage, 
		                               origSpriteColor.b * colorPercentage,
		                               origSpriteColor.a * colorPercentage);
	}
}
