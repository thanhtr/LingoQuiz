using UnityEngine;
using System.Collections;

public class MainView : MonoBehaviour
{
    public TextAsset xmlTest;

    private XMLManager xmlManager;
    private string testQuizId = "1";
    private QuizzesManager quizzesManager;

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteAll();

        xmlManager = new XMLManager();
        Quiz newQuiz = xmlManager.LoadXml(xmlTest.text)[0];

        quizzesManager = GetComponentInChildren<QuizzesManager>();

        quizzesManager.HasQuiz(testQuizId);
        if (!quizzesManager.HasQuiz(testQuizId))
        {
            Debug.Log("store new quiz");
            quizzesManager.StoreNewQuiz(newQuiz);
        }

        quizzesManager = GetComponentInChildren<QuizzesManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Start"))
        {
            Loader.LoadQuiz();
        }
        if (GUI.Button(new Rect(200, 0, 100, 100), "Quiz list"))
        {
            Loader.LoadQuizList();
        }

    }
}
