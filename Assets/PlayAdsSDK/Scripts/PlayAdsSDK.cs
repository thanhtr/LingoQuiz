using UnityEngine;
using System;
using System.Collections;

public class PlayAdsSDK : MonoBehaviour
{
	public enum InterstitialType
	{
		Scratch,
		Lightweight,
		Gift,
		SlotMachine,
		Memory,
		CoverFlow,
		GameList,
		Smart
	}

	private const string INSTANCE_NAME = "PlayAdsSDK";
	private const string WARNING_MESSAGE = "PlayAdsSDK - Error ocurred while initializing the SDK";
	private static bool ready = false;

	private static bool initializing = false;
	private static InterstitialType waitingType;

	private static string waitingAction = "";
	private const string ACTION_CACHE = "Cache";
	private const string ACTION_SHOW = "Show";

	#region --- PUBLIC ---
	
	#region -- CALLBACKS --

	public static event Action 			InterstitialReady;
	public static event Action 			InterstitialShown;
	public static event Action<String> 	InterstitialFailed;
	public static event Action 			InterstitialClosed;
	
	#endregion

	#region -- SINGLETON INSTANCE --
	private static PlayAdsSDK instance;
	public static void EnsureInstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType(typeof(PlayAdsSDK) ) as PlayAdsSDK;
			if(instance == null)
			{
				instance = new GameObject(INSTANCE_NAME).AddComponent<PlayAdsSDK>();
			}
		}
	}
	private void Awake()
	{
		name = INSTANCE_NAME;
		DontDestroyOnLoad(transform.gameObject);
		Start();
	}

	#endregion

	#region -- METHODS --

	private static void Start()
	{
		PlayAdsSDK.Start(null, InterstitialType.Smart);
	}

	private static void Start(string action, InterstitialType type)
	{
		PlayAdsSDK.EnsureInstance();
		if(!PlayAdsSDK.initializing)
		{
			PlayAdsSDK.initializing = true;
			PlayAdsSDK.waitingAction = action;
			PlayAdsSDK.waitingType = type;

			string appID = "";
			string secretToken = "";
			
			#if UNITY_IPHONE
			appID = PlayAdsSDKSettings.IOSAppID;
			secretToken = PlayAdsSDKSettings.IOSSecretToken;
			#elif UNITY_ANDROID
			appID = PlayAdsSDKSettings.AndroidAppID;
			secretToken = PlayAdsSDKSettings.AndroidSecretToken;
			#endif
			
			PlayAdsSDK.PlayAdsSDKStart(appID, secretToken, INSTANCE_NAME);
		}
	}

	public static void Cache()
	{
		PlayAdsSDK.Cache(InterstitialType.Smart);
    }
    public static void Cache(InterstitialType interstitialType)
	{
		PlayAdsSDK.EnsureInstance();
		if(!PlayAdsSDK.ready)
		{
			PlayAdsSDK.Start(ACTION_CACHE, interstitialType);
			return;
		}
		PlayAdsSDK.PlayAdsSDKCache(PlayAdsSDK.GetTypeString(interstitialType));
    }
	
	public static void Show()
	{
		PlayAdsSDK.Show(InterstitialType.Smart);
    }
    public static void Show(InterstitialType interstitialType)
	{
		PlayAdsSDK.EnsureInstance();
		if(!PlayAdsSDK.ready)
		{
			PlayAdsSDK.Start(ACTION_SHOW, interstitialType);
			return;
		}
		PlayAdsSDK.PlayAdsSDKShow(PlayAdsSDK.GetTypeString(interstitialType));
    }

	#endregion
	
	#endregion
	
	#region --- PRIVATE ---

	#region -- BRIDGES --
	
#if UNITY_EDITOR
	private static void PlayAdsSDKStart(string appId, string secret, string instanceName)
	{
		instance.SDKStartedCallback("");
	}
	
	private static void PlayAdsSDKCache(string typeString)
	{
		instance.InterstitialReadyCallback("");
	}

	private static void PlayAdsSDKShow(string typeString)
	{
		instance.InterstitialShownCallback("");
		instance.InterstitialClosedCallback("");
	}
	
	private static void PlayAdsSDKGetVersion ()
    {
		instance.SDKVersionCallback("");
    }
	
#elif UNITY_IPHONE
	
	[System.Runtime.InteropServices.DllImport("__Internal")]
   	private static extern void PlayAdsSDKStart(string appId, string secret, string instanceName);
   	[System.Runtime.InteropServices.DllImport("__Internal")]
   	private static extern void PlayAdsSDKCache(string typeString);
	[System.Runtime.InteropServices.DllImport("__Internal")]
   	private static extern void PlayAdsSDKShow(string typeString);
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void PlayAdsSDKGetVersion();
	
#elif UNITY_ANDROID

	private static void PlayAdsSDKStart(string appId, string secret, string instanceName)
	{
		PlayAdsSDK.CallAndroidSDK("PlayAdsSDKStart", appId, secret, instanceName);
	}

   	private static void PlayAdsSDKCache(string typeString)
	{
		PlayAdsSDK.CallAndroidSDK("PlayAdsSDKCache", typeString);
	}

   	private static void PlayAdsSDKShow(string typeString)
	{
		PlayAdsSDK.CallAndroidSDK("PlayAdsSDKShow", typeString);
	}

	private static void PlayAdsSDKGetVersion()
	{
		PlayAdsSDK.CallAndroidSDK("PlayAdsSDKGetVersion");
	}

	private static AndroidJavaClass AndroidSDK;
	private static void CallAndroidSDK(string methodName, params object[] args)
	{
		if(AndroidSDK == null)
		{
			AndroidSDK = new AndroidJavaClass("com.applift.playads.unity.PlayAdsSDKWrapper");
		}
		AndroidSDK.CallStatic(methodName, args);
	}


#endif
	#endregion
	
	#region -- CALLBACKS --
	
	private void SDKStartedCallback(string message)
	{
		PlayAdsSDK.ready = true;
		PlayAdsSDK.initializing = false;

		if(!string.IsNullOrEmpty(PlayAdsSDK.waitingAction))
		{
			if(ACTION_CACHE.Equals(PlayAdsSDK.waitingAction))
			{
				PlayAdsSDK.Cache(PlayAdsSDK.waitingType);
			}
			else if(ACTION_SHOW.Equals(PlayAdsSDK.waitingAction))
			{
				PlayAdsSDK.Show(PlayAdsSDK.waitingType);
			}
			PlayAdsSDK.waitingAction = null;
		}
	}

	private void SDKStartFailedCallback(string error)
	{
		PlayAdsSDK.ready = false;
		PlayAdsSDK.initializing = false;
		Debug.Log(WARNING_MESSAGE);
	}
	
	private void InterstitialReadyCallback(string message)
	{
		if(InterstitialReady != null)
		{
			InterstitialReady();
		}
	}
	
	private void InterstitialShownCallback(string message)
	{
		if(InterstitialShown != null)
		{
			InterstitialShown();
		}
	}
	
	private void InterstitialFailedCallback(string error)
	{
		if(InterstitialFailed != null)
		{
			InterstitialFailed(error);
		}
	}
	
	private void InterstitialClosedCallback(string message)
	{
		if(InterstitialClosed != null)
		{
			InterstitialClosed();
		}
	}

	private void SDKVersionCallback(string version)
	{
		Debug.Log("PlayAdsSDK - Native version: " + version);
	}
	
	#endregion
	
	#region -- HELPERS --

	private static String GetTypeString(InterstitialType type)
	{
		String result = null;

		switch(type)
		{
			case InterstitialType.Scratch:	 	result = "Scratch";		break;
			case InterstitialType.Lightweight:	result = "Light"; 		break;
			case InterstitialType.Gift: 		result = "Gifts"; 		break;
			case InterstitialType.SlotMachine: 	result = "SlotMachine"; break;
			case InterstitialType.Memory: 		result = "Memory"; 		break;
			case InterstitialType.CoverFlow: 	result = "CoverFlow"; 	break;
			case InterstitialType.GameList: 	result = "GameList"; 	break;
			case InterstitialType.Smart: 		result = "Smart"; 		break;
		}

		return result;
	}

	#endregion

	#endregion
}
