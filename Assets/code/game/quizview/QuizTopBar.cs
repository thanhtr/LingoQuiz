using UnityEngine;
using System.Collections;

public class QuizTopBar : MonoBehaviour 
{
	public GameObject yellowFlag;

	// Use this for initialization
	void Start () 
	{
		AnimationHelper.SetToBeginning(yellowFlag.animation);
		Invoke("ShowYellowFlag", 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void ShowYellowFlag()
	{
		AnimationHelper.SetToBeginningAndPlay(yellowFlag.animation);
	}
}
