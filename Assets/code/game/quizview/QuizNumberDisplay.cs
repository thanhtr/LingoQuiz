using UnityEngine;
using System.Collections;

public class QuizNumberDisplay : MonoBehaviour {

	private tk2dSprite number;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNumber(int index)
	{
		if (number == null) {
			number = GetComponentInChildren<tk2dSprite>();
		}

		number.spriteId = number.GetSpriteIdByName(index.ToString());
	}
}
