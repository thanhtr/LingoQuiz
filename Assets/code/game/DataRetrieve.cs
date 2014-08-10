using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

public class DataRetrieve : MonoBehaviour
{

    private XMLManager xmlManager;
    public GameObject questionText;
    public GameObject[] answerButtons;

//    // Use this for initialization
//    void Start()
//    {
//        xmlManager = new XMLManager();
//        ParseDataToButton();
//    }
//
//    public void ParseDataToButton()
//    {
//
//        List<Quiz> quiz = new List<Quiz>(xmlManager.LoadXml());
//        Debug.Log(quiz[0].questions.Count);
//        List<Question> question = new List<Question>(quiz[Random.Range(0, quiz.Count)].questions);
//        int rand = Random.Range(0, question.Count);
//        List<Answer> answer = new List<Answer>(question[rand].answers);
//        //questionText.GetComponent<tk2dTextMesh>().text = question[rand].text;
//        Debug.Log(answer.Count);
//        for (int i = 0; i < answerButtons.Length; i++)
//        {
//            answerButtons[i].GetComponentInChildren<tk2dTextMesh>().text = answer[i].text;
//        }
//
//
//
//
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//
//    }
}
