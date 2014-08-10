using UnityEngine;
using System.Collections;

public class PlayAdsSDKSample : MonoBehaviour 
{	
	void OnGUI ()
	{
		if(GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.3f, Screen.width * 0.8f, Screen.height * 0.1f), "Cache"))
		{
			this.PlayAds_Cache();
		}
		
		if(GUI.Button(new Rect(Screen.width * 0.1f, Screen.height * 0.5f, Screen.width * 0.8f, Screen.height * 0.1f), "Show"))
		{
			this.PlayAds_Show();
		}
	}

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
	private void PlayAds_Show()
	{
		PlayAdsSDK.Show();
	}
	
	/// <summary>
	/// Shows an asynchronoys interstitial (blocking the screen only when the interstitial is fully loaded)
	/// </summary>
	private void PlayAds_Cache()
	{
		PlayAdsSDK.Cache();
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
	}
	
	private void PlayAds_InterstitialShown()
	{
		// The Interstitial is in the screen
		Debug.Log("* PlayAdsSDK * - InterstitialShown");
	}
	
	private void PlayAds_InterstitialFailed(string errorMessage)
	{
		// Something bad happened with the interstitial
		// After this callback, the SDK will automatically raise Close event (if already shown)
		Debug.Log("* PlayAdsSDK * - InterstitialFailed: " + errorMessage);
	}
	
	private void PlayAds_InterstitialClosed()
	{
		//Do whatever is needed when the app is returned
		
		Debug.Log("* PlayAdsSDK * - InterstitialClosed");
	}
	
	#endregion
	
	#endregion
}
