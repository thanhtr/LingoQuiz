using UnityEngine;
using System.Collections;

public class ViewSlider : MonoBehaviour
{
    public float slideSpeed = 1f;

    private Vector3 initialPosition;
    private Vector3 firstViewInitialPosition;
    private Vector3 secondViewInitialPosition;

	public Transform viewsHolder;
	public Camera mainCamera;

	private Vector2 screenWorldSize;
	private Vector3 screenCenterPosition;

	private Transform currentView;
	private Transform nextView;
	private Transform oldView;
	private bool inTween = false;

    // Use this for initialization
    void Awake ()
    {
		Vector3 viewPortWorldSize = mainCamera.ViewportToWorldPoint(Vector3.one) - mainCamera.ViewportToWorldPoint(Vector3.zero);
		screenWorldSize = new Vector2();
		screenWorldSize.x = viewPortWorldSize.x + 0.05f;
		screenWorldSize.y = viewPortWorldSize.y;

		screenCenterPosition = transform.position;
		screenCenterPosition.z = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void AttachToNewScreenPoint(Transform view)
	{
		view.parent = viewsHolder;

		Vector3 fixedPos = Vector3.zero;
		fixedPos.x += screenWorldSize.x;
		fixedPos.z = 0f;
		fixedPos.y = 0f;
		view.position = fixedPos;

		nextView = view;
	}

	public void AttachToCenterOfScreen(Transform view)
	{
		view.parent = viewsHolder;

		Vector3 fixedPos = transform.TransformPoint(screenCenterPosition);
		fixedPos.z = 0f;
		fixedPos.y = 0f;
		view.localPosition = fixedPos;

		currentView = view;
	}

	public void SlideToLeft()
	{
		if (!inTween)
		{
			iTweenParams p = new iTweenParams();
			p.speed = slideSpeed;
			p.easing = iTween.EaseType.linear;
			p.amount = new Vector3(-screenWorldSize.x, 0f, 0f);
			p.onComplete = "OnTweenComplete";
			p.onCompleteTarget = gameObject;
			iTween.MoveBy(viewsHolder.gameObject, p.ToHash());
			inTween = true;
		}
	}

    private void OnTweenComplete()
    {
		oldView = currentView;
		currentView = nextView;
		nextView = null;
		inTween = false;

		Destroy(oldView.gameObject);
    }
}
