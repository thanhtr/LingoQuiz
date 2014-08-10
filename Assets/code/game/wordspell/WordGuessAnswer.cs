using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class WordGuessAnswer : MonoBehaviour
{

    public string correctAnswer;
    public List<string> correctAnswerLettersList;
    private string compareString;
    private List<LetterChoice> choiceList;
    public Transform container;
    // Use this for initialization
    void Awake()
    {
        correctAnswerLettersList = new List<string>(new string[correctAnswer.Length]);
        for (int i = 0; i < correctAnswerLettersList.Count; i++)
        {
            correctAnswerLettersList[i] = correctAnswer[i].ToString();
        }
        compareString = correctAnswer;
        
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckWordPicked()
    {
        choiceList = new List<LetterChoice>(container.GetComponentsInChildren<LetterChoice>());
        List<string> temp = new List<string>(new string[correctAnswer.Length]);
        for (int i = 0; i < correctAnswer.Length; i++)
        {
            for (int j = 0; j < choiceList.Count; j++)
            {
                if (choiceList[j].tempAnswerIndex == i)
                    temp[i] = choiceList[j].title.text;
            }
        }
        if (temp.SequenceEqual(correctAnswerLettersList))
            return true;
        else return false;
    }
}
