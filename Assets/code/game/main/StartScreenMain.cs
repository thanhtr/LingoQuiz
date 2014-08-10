using UnityEngine;
using System.Collections;

public class StartScreenMain : MonoBehaviour {

	[SerializeField] private Animation logoAnimation;
	[SerializeField] private Animation playButtonAnimation;
	[SerializeField] private Animation bottomButtonsAnimation;
	[SerializeField] private LoadingText loadingText;

	private static bool isFirstTime = true;
	private BackendCommunicator backEnd;
	private RatePopup ratePopup;

	// Use this for initialization
	void Start () 
	{
        backEnd = FindObjectOfType<BackendCommunicator>();
        backEnd.OnWeekQuizDownloadComplete += OnWeekQuizDownloadComplete;
        backEnd.OnWeekQuizDownloadFail += OnWeekQuizDownloadFail;

		ratePopup = GetComponentInChildren<RatePopup>();

		AnimationHelper.SetToBeginning(logoAnimation);
		AnimationHelper.SetToBeginning(playButtonAnimation);
		AnimationHelper.SetToBeginning(bottomButtonsAnimation);

		GameDataStore.Get().playTimes++;
		GameDataStore.Get().Save();

		bool bol = true;
		Debug.Log("bool = " + bol.ToString());

		if (isFirstTime)
		{
			backEnd.GetWeeklyQuizzes();
			isFirstTime = false;
		}
		else 
		{
			PlayStartAnimations();
		}
	}

    void OnWeekQuizDownloadComplete()
    {
        PlayStartAnimations();
    }

    void OnWeekQuizDownloadFail() 
    {
        Debug.Log("ERROR occured in getting new quizzes from backend");
		PlayStartAnimations();
    }
	private void PlayStartAnimations()
	{
		PlayLogoAnimation();
		Invoke("PlayBottomButtonsAnimation", 1.0f);
		Invoke("PlayPlayButtonAnimation", 1.0f);
		loadingText.Hide();

		ratePopup.TryShow();
	}

	// Update is called once per frame
	void Update () {
	
	}

	private void PlayLogoAnimation()
	{
		AnimationHelper.SetToBeginningAndPlay(logoAnimation);
	}

	private void PlayPlayButtonAnimation()
	{
		AnimationHelper.SetToBeginningAndPlay(playButtonAnimation);
	}

	private void PlayBottomButtonsAnimation()
	{
		AnimationHelper.SetToBeginningAndPlay(bottomButtonsAnimation);
	}
}
