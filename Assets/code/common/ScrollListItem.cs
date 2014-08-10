using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Swipe scroller item base
/// </summary>
/// 

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(InputDetector))]
public class ScrollListItem : MonoBehaviour 
{
	public delegate void ItemEventHandler(ScrollListItem button);
	public Action<ScrollListItem> OnItemSelected;

	public float allowedTouchMove = 0.01f;

	private InputDetector inputDetector;
	private bool isTouched = false;
	private Vector3 touchPosNow;
	private Vector3 touchStartPos;
	private float touchMovement;

	protected virtual void Awake ()
	{
		inputDetector = GetComponent<InputDetector>();
		inputDetector.OnTouch += OnItemTouch;
		inputDetector.OnTouchReleaseAnywhere += OnTouchRelease;
	}
	
	protected virtual void Update ()
	{
		if (isTouched)
		{
			touchPosNow =  inputDetector.GetTouchScreenPosition();
			if (touchStartPos != null)
			{
				touchMovement = Vector3.Distance(touchPosNow, touchStartPos);

				if (touchMovement > allowedTouchMove)
				{
					isTouched = false;
					Debug.Log("Cancel touch! Movement: "+touchMovement);
				}
			}
		}
	}
	
	protected virtual void OnItemTouch(InputDetectorTouch touch)
	{
		isTouched = true;
		touchMovement = 0f;
		touchStartPos = inputDetector.GetTouchScreenPosition();
	}
	
	protected virtual void OnTouchRelease(InputDetectorTouch touch)
	{
		if (isTouched)
		{
			SendSelectionEvent();
		}

		isTouched = false;
	}

	private void SendSelectionEvent()
	{
		if (OnItemSelected != null)
		{
			OnItemSelected(this);
		}
	}
}
