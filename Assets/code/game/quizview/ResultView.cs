using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultView : MonoBehaviour
{
	public delegate void ResultViewEvents();
	public event ResultViewEvents OnResultNextButtonPressed;
	public event ResultViewEvents OnResultRetryButtonPressed;

	[SerializeField] private Animation pointsFlagAnimation;
	[SerializeField] private Animation buttonsAnimation;
	[SerializeField] private ResultPointsText resultPointsText;
	[SerializeField] private ProgressBar progressBar;
	[SerializeField] private ParticleSystem pointDropParticles;
	[SerializeField] private MultiParticleEmiter levelUpParticles;
	[SerializeField] private InputDetector nextButton;
	[SerializeField] private InputDetector retryButton;
	[SerializeField] private GameObject viewMask;
	[SerializeField] private float tweenTime = 1f;
	[SerializeField] private Camera viewCamera;

	private Vector3 viewMaskInitScale;

	private int gainPoints;
	private float gainExpPoints;

    // Use this for initialization
    void Start()
    {
		nextButton.OnTouchAndRelease += OnNextPressed;
		retryButton.OnTouchAndRelease += OnRetryPressed;

		progressBar.OnLevelUp += OnLevelUp;
		progressBar.OnExpTweenComplete += OnExpTweenComplete;

		AnimationHelper.SetToBeginning(pointsFlagAnimation);
		AnimationHelper.SetToBeginning(buttonsAnimation);

		viewMaskInitScale = viewMask.transform.localScale;

		viewCamera.enabled = false;
		viewMask.SetActive(true);

		ToggleColliders(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

	private void ToggleColliders(bool toggle)
	{
		nextButton.collider.enabled = toggle;
		retryButton.collider.enabled = toggle;
		viewMask.collider.enabled = toggle;
	}

    public void ShowQuizResult(int totalPoints, float expGain)
    {
		viewCamera.enabled = true;
		HideViewMask();
		ToggleColliders(true);

		gainPoints = totalPoints;
		gainExpPoints = expGain;

		SetPointsText(gainPoints);

		Invoke("ShowPointsFlag", 0.7f);

		float addExpDealay = 2.3f;
		Invoke("PlayPointDropParticles", addExpDealay * 0.9f);
		Invoke("AddExperience", addExpDealay);
    }

	public void ResetView()
	{
		viewCamera.enabled = false;
		viewMask.transform.localScale = viewMaskInitScale;
		ToggleColliders(false);
	}

	private void SetPointsText(int _points)
	{
		resultPointsText.SetPoints(_points);
	}

	private void ShowPointsFlag()
	{
		AnimationHelper.SetToBeginningAndPlay(pointsFlagAnimation);
	}

	private void AddExperience()
	{
		float tweenTime = 2f;

		Invoke("StopPointDropParticles", tweenTime);

		resultPointsText.AnimateToZero(tweenTime);
		progressBar.AddExperience(gainExpPoints, tweenTime);
	}

	private void PlayPointDropParticles()
	{
		pointDropParticles.Play();
	}

	private void StopPointDropParticles()
	{
		pointDropParticles.Stop();
	}

	private void ShowButtons()
	{
		AnimationHelper.SetToBeginningAndPlay(buttonsAnimation);
	}

	private void OnNextPressed(InputDetectorTouch touch)
	{
		if (OnResultNextButtonPressed != null)
		{
			OnResultNextButtonPressed();
		}
	}

	private void OnRetryPressed(InputDetectorTouch touch)
	{
		if (OnResultRetryButtonPressed != null)
		{
			OnResultRetryButtonPressed();
		}
	}

	private void HideViewMask()
	{
		iTweenParams p = new iTweenParams();
		p.time = tweenTime;
		p.scale = Vector3.zero;
		p.easing = iTween.EaseType.linear;
		iTween.ScaleTo(viewMask, p.ToHash());
	}

	void OnLevelUp (int newLevel)
	{
		levelUpParticles.PlayParticles();
	}
	
	void OnExpTweenComplete ()
	{
		ShowButtons();
	}
}
