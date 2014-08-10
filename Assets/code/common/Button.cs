using UnityEngine;
using System.Collections;

/// <summary>
/// Button base class
/// </summary>
/// 

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(InputDetector))]
[RequireComponent(typeof(ButtonSounds))]
public class Button : MonoBehaviour 
{
	public delegate void ButtonEventHandler(Button button);
	public event ButtonEventHandler OnPressed;

	private InputDetector inputDetector;

	protected void InitButton()
	{
		inputDetector = GetComponent<InputDetector>();
		inputDetector.OnTouch += OnButtonPress;
		inputDetector.OnTouchReleaseAnywhere += OnButtonRelease;
		inputDetector.OnTouchAndRelease += OnButtonPressAndRelease;
	}

	protected virtual void OnButtonPress(InputDetectorTouch touch)
	{
		if (OnPressed != null)
		{
			OnPressed(this);
		}
	}

	protected virtual void OnButtonRelease(InputDetectorTouch touch)
	{
	}

	protected virtual void OnButtonPressAndRelease(InputDetectorTouch touch)
	{
	}
}
