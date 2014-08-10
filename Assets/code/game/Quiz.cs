using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum QuizDifficultyLevelEnum
{
	Easy = 1,
	Medium,
	Hard
}

public class Quiz
{
	public string id;
	public string name;
	public string intro;
	public string version;
	public string category;
	public int points;
	public QuizDifficultyLevelEnum level;
	public List<Question> questions;

	public Quiz()
	{
		id = "";
		name = "";
		intro = "";
		points = 0;
		questions = new List<Question>();
	}
}


