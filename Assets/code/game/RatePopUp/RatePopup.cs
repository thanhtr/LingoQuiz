using UnityEngine;
using System.Collections;

public class RatePopup : MonoBehaviour 
{
	[SerializeField] private string iosUrl;
	[SerializeField] private string androidUrl;

	[SerializeField] private RateButton rateButton;
	[SerializeField] private Later laterButton;
	[SerializeField] private GameObject bg;
	[SerializeField] private GameObject popup;

	private const int showRateAfter = 3;
	private Collider bgCollider;

	// Use this for initialization
	void Start () 
	{
		rateButton.OnRatePressed += HandleOnRatePressed;
		laterButton.OnLaterPressed += HandleOnLaterPressed;

		bgCollider = bg.GetComponentInChildren<BoxCollider>();
		bgCollider.enabled = false;

		AnimationHelper.SetToBeginning(bg.animation);
		AnimationHelper.SetToBeginning(popup.animation);
	}

	public void TryShow()
	{
		bool rated = GameDataStore.Get().rated;
		int playTimes = GameDataStore.Get().playTimes;
		if (!rated && playTimes > showRateAfter)
		{
			AnimateIn();
		}
	}

	private void AnimateIn()
	{
		ShowPopup();
		AnimationHelper.SetToBeginningAndPlay(bg.animation);
		bgCollider.enabled = true;
	}

	private void AnimateOut()
	{
		HidePopup();
		AnimationHelper.SetToEnd(bg.animation);
		AnimationHelper.SetPlaySpeed(bg.animation, -1f);
		bg.animation.Play();
		bgCollider.enabled = false;
	}

	private void ShowPopup()
	{
		AnimationHelper.SetPlaySpeed(popup.animation, 1f);
		popup.animation.Play("popupin");
	}

	private void HidePopup()
	{
		popup.animation.Play("popupout");
	}

	void HandleOnLaterPressed ()
	{
		GameDataStore.Get().playTimes = 0;
		AnimateOut();
	}

	void HandleOnRatePressed ()
	{
		GameDataStore.Get().rated = true;
		GameDataStore.Get().Save();

		HidePopup();
#if UNITY_ANDROID
		Application.OpenURL(androidUrl);
#elif UNITY_IPHONE
		Application.OpenURL(iosUrl);
#endif		
	}

}
