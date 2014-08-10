using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressBar : MonoBehaviour
{
	public delegate void LevelUpEvents(int newLevel);
	public event LevelUpEvents OnLevelUp;

	public delegate void ProgressBarEvents();
	public event ProgressBarEvents OnExpTweenComplete;

	public Transform background;
    public Transform progressBar;
	public tk2dTextMesh levelText;

   	private float[] levelRequire;
  
    private GameDataStore gameData;
  
	public float testAmount;
	private float tweenTime = 1f;
	private float amountLeft;

    // Use this for initialization
    void Awake ()
    {
		levelRequire = LevelsExperiencePointsArray.pointsArray;

        gameData = GameDataStore.Get();
  
		if (levelText != null) {
			levelText.text = gameData.level.ToString();
		}

		SetProgressBarScale(GetPercentage(gameData.exp, gameData.level));
    }

    public void AddExperience(float amount, float time)
    {
		float exp = gameData.exp;
		amountLeft = (gameData.exp + amount) - levelRequire[gameData.level];
		tweenTime = time;
		DoTween(exp, exp+amount);
    }

	private void DoTween(float fromExp, float toExp)
	{
		iTweenParams p = new iTweenParams();
		p.from = fromExp;
		p.to = toExp;
		p.time = tweenTime;
		p.name = "ExpTween";
		p.easing = iTween.EaseType.linear;
		p.onUpdate = "OnTweenUpdate";
		p.onUpdateTarget = gameObject;
		p.onComplete = "OnTweenFinished";
		p.onCompleteTarget = gameObject;
		iTween.ValueTo(gameObject, p.ToHash());
	}

	private void OnTweenUpdate(float newExpValue)
	{
		gameData.exp = newExpValue; 

		if (gameData.exp >= levelRequire[gameData.level])
		{
			LevelUp();
		}

		SetProgressBarScale(GetPercentage(gameData.exp, gameData.level));
	}

	private void LevelUp()
	{
		if (iTween.tweens.Count > 0)
		{
			iTween.StopByName("ExpTween");
		}

		progressBar.localScale = Vector3.one;

		gameData.exp = 0f;
		gameData.level++;

		if (OnLevelUp != null)
		{
			OnLevelUp(gameData.level);
		}

		if (levelText != null) {
			levelText.text = gameData.level.ToString();
		}

		AddExperience(amountLeft, tweenTime);
	}

	private void SetProgressBarScale(float perc)
	{
		progressBar.localScale = new Vector3(perc, 1f, 1f);
	}

	private float GetPercentage(float _exp, int _lvl)
	{
		float lvlMaxPoints = levelRequire[_lvl];
		float percentage = _exp / lvlMaxPoints;
		return Mathf.Clamp01(percentage);
	}

	private void OnTweenFinished()
	{
		Debug.Log("TWEEN FINISHED");
		gameData.Save();
		if (OnExpTweenComplete != null)
		{
			OnExpTweenComplete();
		}
	}

	//	void OnGUI()
	//	{
	//		if (Application.isEditor)
	//		{
	//			if (GUI.Button(new Rect(0f,0f,100f,100f), "Add"))
	//			{
	//				AddExperience(testAmount);
	//			}
	//		}
	//	}
}