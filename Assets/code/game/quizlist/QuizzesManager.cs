using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Contains all quizzes. Loads and saves quizzes to PlayerPrefs 
/// </summary>
public class QuizzesManager : MonoBehaviour
{
    private List<Quiz> storedQuizzes;
    private XMLManager xmlManager;
    private bool initialized = false;
    private string storedQuizzesDataStr;

    private const char PARAM_SEPAR = '¡';
    private const char OBJ_SEPAR = '|';

    #region Start functions
    void Awake()
    {
        QuizzesManager other = FindObjectOfType(typeof(QuizzesManager)) as QuizzesManager;
        if (other != null && other != this)
        {
            Destroy(this);
        }
        else
        {
            LoadStoredQuizzes();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void LoadStoredQuizzes()
    {
        if (!initialized)
        {
            initialized = true;
            xmlManager = new XMLManager();
            storedQuizzesDataStr = GameDataStore.Get().quizzesData;

            storedQuizzes = new List<Quiz>();

            if (storedQuizzesDataStr != "")
            {
                // get all quizzes from stored quiz data string
                storedQuizzes = ParseQuizzes(storedQuizzesDataStr);
            }
        }
    }
    #endregion

    #region Public functions
    public void StoreNewQuiz(Quiz quiz)
    {
        if (initialized)
        {
			Debug.Log("StoreNewQuiz() quidId="+quiz.id);
            storedQuizzes.Add(quiz);
        }
        else
        {
            Debug.LogWarning("ERROR: No QuizzesManager found in scene!");
        }
    }

    public void SaveQuizzes()
    {
        if (initialized)
        {
            string quizzesDataStr = QuizzesToString();

            GameDataStore.Get().quizzesData = quizzesDataStr;
            GameDataStore.Get().Save();

			Debug.Log("SaveQuizzes() quizzesData= "+quizzesDataStr);
        }
        else
        {
            Debug.LogWarning("ERROR: No QuizzesManager found in scene!");
        }
    }

    public bool HasQuiz(string _quizId)
    {
        bool quizIdFound = false;

		Quiz found = storedQuizzes.Find(p => p.id == _quizId);
		if (found != null) {
			quizIdFound = true;
		}

        return quizIdFound;
    }
    
    public bool versioncheck(string version, string quizID)
    {
        bool version_update = false;
        int version_int = int.Parse(version);
        if(HasQuiz(quizID)){
            Quiz quiz = GetQuiz(quizID);
            int stored_quiz_ver=int.Parse(quiz.version);
            if(version_int>stored_quiz_ver)
            {
                version_update = true;
            }
     
        }

    return version_update;
    }

    public Quiz GetQuiz(string quizId)
    {
        Quiz foundQuiz = null;
        if (initialized)
        {
            foreach (Quiz quiz in storedQuizzes)
            {
                if (quiz.id == quizId)
                {
                    foundQuiz = quiz;
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("ERROR: No QuizzesManager found in scene!");
        }

        return foundQuiz;
    }

    public List<Quiz> GetQuizList()
    {
        return storedQuizzes;
    }
    #endregion

    #region Private functions
    private List<Quiz> ParseQuizzes(string quizzesData)
    {
        List<Quiz> quizzesFromStore = new List<Quiz>();
        string[] quizDataObjs = quizzesData.Split(OBJ_SEPAR);

        try
        {
            foreach (string quizData in quizDataObjs)
            {
                if (quizData != "")
                {
                    string[] dataFields = quizData.Split(PARAM_SEPAR);

                    string xml = dataFields[0];
                    int points = int.Parse(dataFields[1]);

                    Quiz quiz = xmlManager.LoadXml(xml)[0];
                    quiz.points = points;

                    quizzesFromStore.Add(quiz);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Error: clear quizzesData " + e);
            GameDataStore.Get().quizzesData = "";
            GameDataStore.Get().Save();
        }

        return quizzesFromStore;
    }

    private string QuizzesToString()
    {
        string quizzesStr = "";
        foreach (Quiz quiz in storedQuizzes)
        {
            quizzesStr += xmlManager.WriteToString(quiz);
            quizzesStr += PARAM_SEPAR;
            quizzesStr += quiz.points.ToString();
            quizzesStr += OBJ_SEPAR;
        }
        return quizzesStr;
    }
    #endregion
}
