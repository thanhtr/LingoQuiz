using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Touch event handler. Detech touch with colliders
/// </summary>
/// 

public class InputDetectorTouch
{
	public Vector3 screenPosition;
	public GameObject target;
}

public class InputDetector : MonoBehaviour {

	public delegate void InputDetectorEventHandler(InputDetectorTouch touch);

	public event InputDetectorEventHandler OnTouchAndRelease;
	public event InputDetectorEventHandler OnTouch;
	public event InputDetectorEventHandler OnTouchRelease;
	public event InputDetectorEventHandler OnTouchReleaseAnywhere;
	public event InputDetectorEventHandler OnTouchAnywhere;

	private bool touching = false;
	
	private bool touchedObject = false;
	
	private List<GameObject> externalListeners;

	private Vector3 inputStartWorldPosition;
	
	private float maxMoveDistance = 0.1f;

	private List<Camera> cameras;

	private InputDetectorTouch inputDetectorTouch;
	
	// Use this for initialization
	void Awake() 
	{
		externalListeners = new List<GameObject>();
		inputDetectorTouch = new InputDetectorTouch();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Application.isEditor)
		{
			if (Input.GetMouseButtonDown(0))
			{
				inputDetectorTouch.target = null;
				
				if (!touching)
				{
					inputStartWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
				
				touching = true;
				
				SendTouchAnywhereEvent(inputDetectorTouch);
				
				if (RayHitsAnyCollider(Input.mousePosition))
				{
					SendTouchEvent(inputDetectorTouch);
					touchedObject = true;
				}
			}
			else if (Input.GetMouseButtonUp(0))
			{
				SendTouchReleaseAnywhereEvent(inputDetectorTouch);
				inputDetectorTouch.target = null;
				
				if (RayHitsAnyCollider(Input.mousePosition) && touchedObject && TouchNotMoved())
				{
					SendTouchReleaseEvent(inputDetectorTouch);
					SendTouchAndReleaseEvent(inputDetectorTouch);
				}
				
				touchedObject = false;
				touching = false;
			}
				
			
		}
		else if (Application.platform == RuntimePlatform.Android
				|| Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (Input.touchCount > 0) 
			{
				for (int i = 0; i < Input.touchCount; ++i)
				{
					inputDetectorTouch.screenPosition = Input.GetTouch(i).position;
					inputDetectorTouch.target = null;
					
					if (!touching)
					{
						inputStartWorldPosition = Camera.main.ScreenToWorldPoint(inputDetectorTouch.screenPosition);
					}					
					
					touching = true;
					
					if (Input.GetTouch(i).phase == TouchPhase.Began)
					{
						SendTouchAnywhereEvent(inputDetectorTouch);
						
						if (RayHitsAnyCollider(Input.GetTouch(i).position))
						{
							SendTouchEvent(inputDetectorTouch);
							touchedObject = true;
						}
					}
					if  (Input.GetTouch(i).phase == TouchPhase.Ended)
					{
						SendTouchReleaseAnywhereEvent(inputDetectorTouch);
						
						if (touchedObject)
						{
							// check if touch still in collider
							if (RayHitsAnyCollider(Input.GetTouch(i).position))
							{
								SendTouchReleaseEvent(inputDetectorTouch);
								
								if (TouchNotMoved())
								{
									SendTouchAndReleaseEvent(inputDetectorTouch);
								}
							}
						}
					}
				}
			}
			else 
			{
				if (touching) 
				{
					inputDetectorTouch.target = null;
					SendTouchReleaseAnywhereEvent(inputDetectorTouch);
					touching = false;
				}
				touchedObject = false;
			}
		}
	}

	private bool TouchNotMoved()
	{
		bool touchNotMoved = true;
		Vector3 inputPosNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
								
		float distace = Vector3.Distance(inputStartWorldPosition, inputPosNow);
										
		if (distace > maxMoveDistance)
		{
			touchNotMoved = false;
		}
		
		return touchNotMoved;
	}
	
	private bool RayHitsAnyCollider(Vector3 screenPoint)
	{
		bool raycastHitsCollider = false;
		
		List<Camera> cameras = new List<Camera>(GetCameras());
		cameras.Sort(SortCamera);

		foreach (Camera cam in cameras)
		{
			if (cam.enabled && RayHitsCollider(cam, screenPoint))
			{
				raycastHitsCollider = true;
				break;
			}
		}
		
		return raycastHitsCollider;
	}

	private int SortCamera(Camera a,Camera b)
	{
		if(a.depth > b.depth)
		{
			return -1;
		}
		else if(a.depth == b.depth)
		{
			return 0;
		}
		else
		{
			return 1;
		}
	}
	
	bool RayHitsCollider(Camera camera, Vector3 screenPoint)
	{
		bool raycastHitsCollider = false;
		
		RaycastHit hit = new RaycastHit();
		
		Ray ray = camera.ScreenPointToRay(screenPoint);
		
		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
		{
			inputDetectorTouch.target = hit.collider.gameObject;
			if(hit.collider.gameObject == gameObject) 
			{
				raycastHitsCollider = true;
			}
		}
		
		return raycastHitsCollider;		
	}
		
	Camera[] GetCameras()
	{
		Camera[] cameras = FindObjectsOfType(typeof(Camera)) as Camera[];
		return cameras;
	}
							

	#region Event Methdods
	private void SendTouchAndReleaseEvent(InputDetectorTouch touch)
	{
		if (OnTouchAndRelease != null)
		{
			OnTouchAndRelease(touch);
		}
	}

	private void SendTouchEvent(InputDetectorTouch touch)
	{
		if (OnTouch != null)
		{
			OnTouch(touch);
		}
	}

	private void SendTouchReleaseEvent(InputDetectorTouch touch)
	{
		if (OnTouchRelease != null)
		{
			OnTouchRelease(touch);
		}
	}

	private void SendTouchReleaseAnywhereEvent(InputDetectorTouch touch)
	{
		if (OnTouchReleaseAnywhere != null)
		{
			OnTouchReleaseAnywhere(touch);
		}
	}

	private void SendTouchAnywhereEvent(InputDetectorTouch touch)
	{
		if (OnTouchAnywhere != null)
		{
			OnTouchAnywhere(touch);
		}
	}
	#endregion

	public Vector3 GetTouchScreenPosition()
	{
		Vector3 inputPosNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return inputPosNow;
	}
}
