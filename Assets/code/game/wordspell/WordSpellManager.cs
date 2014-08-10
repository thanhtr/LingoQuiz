using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordSpellManager : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject choicePrefab;
    private Vector3 offset;
    public GameObject choiceContent;
    private static string alphabetString = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
    private List<string> alphabetList;
    public List<string> randomizedAlphabetList;
    public List<string> correctLetterList;
    public List<LetterChoice> choiceList;
    public Transform testAnswerPosition;
    public Transform container;
    private List<Transform> answerBlocksList;
    public List<bool> checkList;
    private WordGuessAnswer wordGuessAnswer;
    // Use this for initialization
    void Start()
    {
        checkList = new List<bool>(new bool[container.childCount]);
        wordGuessAnswer = container.GetComponent<WordGuessAnswer>();
        List<string> temp = wordGuessAnswer.correctAnswerLettersList;
        for (int i = 0; i < checkList.Count; i++)
        {
            checkList[i] = false;
        }

        answerBlocksList = new List<Transform>();
        for (int i = 0; i < container.childCount; i++)
        {
            answerBlocksList.Add(container.GetChild(i));
        }
        offset = Vector3.zero;

        alphabetList = new List<string>(alphabetString.Split(','));
        randomizedAlphabetList = new List<string>(new string[alphabetList.Count]);
        correctLetterList = new List<string>(new string[temp.Count]);

        RandomizedGenerate();
        SpawnChoices();

        foreach (LetterChoice choice in choiceList)
        {
            choice.OnWordChoiceClicked += ChoiceClickHandle;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(wordGuessAnswer.CheckWordPicked())
        {
            Debug.Log("CORRECT");
        }
    }

    void ChoiceClickHandle(LetterChoice choice)
    {
        if (choice.transform.position == choice.location)
        {
            for (int i = 0; i < checkList.Count; i++)
            {
                if (checkList[i] == false)
                {
                    iTween.MoveTo(choice.gameObject, answerBlocksList[i].position, 1);
                    choice.tempAnswerIndex = i;
                    checkList[i] = true;
                    break;
                }
            }
        }
        else
        {
            iTween.MoveTo(choice.gameObject, choice.location, 1);
            checkList[choice.tempAnswerIndex] = false;
        }
    }

    void SpawnChoices()
    {
        for (int i = 0; i < 14; i++)
        {
            if (i < 7)
            {
                offset.y = 0;
                GameObject temp = (GameObject)Instantiate(choicePrefab, spawnPosition.position + offset, Quaternion.identity);
                temp.transform.parent = choiceContent.transform;
                offset.x += 0.19f;
                offset.y -= 0.2f;
                temp.GetComponent<LetterChoice>().SetText(randomizedAlphabetList[i]);
                temp.GetComponent<LetterChoice>().location = temp.transform.position;
                choiceList.Add(temp.GetComponent<LetterChoice>());
            }
            else
            {
                offset.x -= 0.19f;
                GameObject temp = (GameObject)Instantiate(choicePrefab, spawnPosition.position + offset, Quaternion.identity);
                temp.transform.parent = choiceContent.transform;
                temp.GetComponent<LetterChoice>().SetText(randomizedAlphabetList[i]);
                temp.GetComponent<LetterChoice>().location = temp.transform.position;
                choiceList.Add(temp.GetComponent<LetterChoice>());
            }
        }
    }

    void RandomizedGenerate()
    {
        for (int i = 0; i < randomizedAlphabetList.Count; i++)
        {
            int random = Random.Range(0, alphabetList.Count);
            for (int j = 0; j < wordGuessAnswer.correctAnswerLettersList.Count; j++)
            {
                if (alphabetList[random] == wordGuessAnswer.correctAnswerLettersList[j])
                {
                    correctLetterList[j] = alphabetList[random];
                }

            }
            randomizedAlphabetList[i] = alphabetList[random];
            alphabetList.RemoveAt(random);
        }
        int count = 0;
        for (int i = 0; i < correctLetterList.Count; i++)
        {
            int random = Random.Range(0, 14);
            while (wordGuessAnswer.correctAnswerLettersList.Contains(randomizedAlphabetList[random]))
            {
                random = Random.Range(0, 14);

            }
            randomizedAlphabetList[random] = correctLetterList[i];
            count++;
        }

    }
}
