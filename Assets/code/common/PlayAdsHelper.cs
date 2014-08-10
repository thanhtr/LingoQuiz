using UnityEngine;
using System.Collections;

public class PlayAdsHelper : MonoBehaviour 
{
	public delegate void PlayAdHelperEvents();
	public event PlayAdHelperEvents OnPlayAdsIntersialReady;
	public event PlayAdHelperEvents OnPlayAdsIntersialShown;
	public event PlayAdHelperEvents OnPlayAdsIntersialClosed;
	public event PlayAdHelperEvents OnPlayAdsIntersialFailed;

	private bool isIntersialOnScreen = false;
	private bool isIntersialReady = false;

	private void Awake()
	{
		PlayAdsSDK.InterstitialReady 	+= PlayAds_InterstitialReady;
		PlayAdsSDK.InterstitialShown 	+= PlayAds_InterstitialShown;
		PlayAdsSDK.InterstitialFailed 	+= PlayAds_InterstitialFailed;
		PlayAdsSDK.InterstitialClosed 	+= PlayAds_InterstitialClosed;
	}
	
	private void Destroy()
	{
		PlayAdsSDK.InterstitialReady 	-= PlayAds_InterstitialReady;
		PlayAdsSDK.InterstitialShown 	-= PlayAds_InterstitialShown;
		PlayAdsSDK.InterstitialFailed 	-= PlayAds_InterstitialFailed;
		PlayAdsSDK.InterstitialClosed 	-= PlayAds_InterstitialClosed;
	}

	/// <summary>
	/// Shows a synchronoys interstitial (blocking the screen)
	/// </summary>
	public void PlayAds_Show()
	{
		PlayAdsSDK.Show();
	}
	
	/// <summary>
	/// Shows an asynchronoys interstitial (blocking the screen only when the interstitial is fully loaded)
	/// </summary>
	public void PlayAds_Cache()
	{
		PlayAdsSDK.Cache();
	}

	public bool IsIntersialReady()
	{
		return isIntersialReady;
	}

	public bool IsIntersialOnScreen()
	{
		return isIntersialOnScreen;
	}
	
	#region PlayAdsSDK Callbacks
	
	#region Interstitial callbacks
	
	/// <summary>
	/// This method is only needed for ASYNCHRONOUS interstitial mode
	/// </summary>
	private void PlayAds_InterstitialReady()
	{
		// The Interstitial was cached and it's ready to be shown
		Debug.Log("* PlayAdsSDK * - InterstitialReady");
		if (OnPlayAdsIntersialReady != null)
		{
			OnPlayAdsIntersialReady();
		}
		isIntersialReady = true;
	}
	
	private void PlayAds_InterstitialShown()
	{
		// The Interstitial is in the screen
		Debug.Log("* PlayAdsSDK * - InterstitialShown");
		if (OnPlayAdsIntersialShown != null)
		{
			OnPlayAdsIntersialShown();
		}
		isIntersialOnScreen = true;
	}
	
	private void PlayAds_InterstitialFailed(string errorMessage)
	{
		// Something bad happened with the interstitial
		// After this callback, the SDK will automatically raise Close event (if already shown)
		Debug.Log("* PlayAdsSDK * - InterstitialFailed: " + errorMessage);
		if (OnPlayAdsIntersialFailed != null)
		{
			OnPlayAdsIntersialFailed();
		}
		isIntersialOnScreen = false;
		isIntersialReady = false;
	}
	
	private void PlayAds_InterstitialClosed()
	{
		//Do whatever is needed when the app is returned
		Debug.Log("* PlayAdsSDK * - InterstitialClosed");
		if (OnPlayAdsIntersialClosed != null)
		{
			OnPlayAdsIntersialClosed();
		}
		isIntersialOnScreen = false;
		isIntersialReady = false;
	}
	
	#endregion
	
	#endregion
}
