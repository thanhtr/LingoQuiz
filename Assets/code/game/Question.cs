using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Question
{
    public Question()
    {
        queId = "";
        text = "";
        explanation = "";
        correctAnswerId = "";
        answers = new List<Answer>();
    }
	public string queId;
	public string text;
	public string explanation;
	public string correctAnswerId;
	public bool isInput; 
    public List<Answer> answers { set; get; }
}
