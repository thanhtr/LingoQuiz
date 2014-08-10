using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;


public class BackendCommunicator : MonoBehaviour
{
    private XMLManager xmlManager;
    private QuizzesManager quizzesManager;
    private QuizRequestsManager quizRequestManager;
    private WeekQuizRequest weekQuizRequest;

    private int requestsCount;
    public delegate void QuizDownload();
    public event QuizDownload OnWeekQuizDownloadComplete;
    public event QuizDownload OnWeekQuizDownloadFail;

    public void GetWeeklyQuizzes()
    {
        quizzesManager = FindObjectOfType(typeof(QuizzesManager)) as QuizzesManager;
        xmlManager = new XMLManager();

        weekQuizRequest = new WeekQuizRequest();
        weekQuizRequest.OnRequestFinished += HandleOnWeekQuizzesRequestFinished;

        quizRequestManager = new QuizRequestsManager();
        quizRequestManager.OnQuizRequestFinished += HandleOnQuizRequestFinished;

        FetchWeekQuizIds();
    }

    private void DownloadComplete()
    {
        if (OnWeekQuizDownloadComplete != null)
        {
            OnWeekQuizDownloadComplete();
        }
    }

    private void DownloadFailed()
    {
        if (OnWeekQuizDownloadFail != null)
        {
            OnWeekQuizDownloadFail();
        }
    }
    private void FetchWeekQuizIds()
    {
        string url = "http://kidesolutions.fi/lingoquiz/GetWeekQuizz.php";

        StartCoroutine(weekQuizRequest.RequestWeekQuizzes(url));
    }

    private void HandleOnWeekQuizzesRequestFinished(WWW request)
    {
        if (request.isDone)
        {
            string xmltext = request.text;

            Debug.Log("Received week quiz xml: " + xmltext);

            if (xmltext != "")
            {
                List<WeekQuizData> weekQuizIds = ParseIdsFromWeekQuizXml(xmltext);

				bool downloadingQuiz = false;
                foreach (WeekQuizData data in weekQuizIds)
                {
                    string quizId = data.quizId;
                    string version = data.quizVersion;
                    
                    if (!quizzesManager.HasQuiz(quizId))
                    {
                        requestsCount++;
                        downloadingQuiz = true;
                        FetchQuizById(quizId);
                        Debug.Log("version+category=" + data.quizVersion + data.category);
                        
                    }
                    else if(quizzesManager.HasQuiz(quizId))
                    {
                        Debug.Log("quiz is already in quizzesManager");
                        if (quizzesManager.versioncheck(version, quizId)) 
                        {
                            Debug.Log("version check is complete, need to update");
                            requestsCount++;
                            downloadingQuiz = true;
                            FetchQuizById(quizId);
                            Debug.Log("version+category=" + data.quizVersion + data.category);

                        }
                        else
                        {
                            Debug.Log("version check is complete, quiz is updated");
                        }
                        
                        
                    }
                }

				if (!downloadingQuiz) {
					DownloadComplete();
				}
            }

        }
        else
        {
            Debug.Log("ERROR! Could not received week quizzes succesfully!");
            DownloadFailed();
        }
    }

    private List<WeekQuizData> ParseIdsFromWeekQuizXml(string xmltext)
     {
         XmlDocument xmldoc = new XmlDocument();
         xmldoc.LoadXml(xmltext);
         XmlNodeList week_quiz = xmldoc.GetElementsByTagName("quizzes_of_week_id");
 
 
         List<WeekQuizData> weekQuizIds = new List<WeekQuizData>();
        
       
         
         
        
         foreach (XmlNode node in week_quiz)
         {
            WeekQuizData data = gameObject.AddComponent<WeekQuizData>();
             string quizId = xmlManager.GetNodeValue(node, "id");
             string version = xmlManager.GetNodeValue(node, "version");
             string category = xmlManager.GetNodeValue(node, "category");
             data.quizId = quizId;
             data.quizVersion = version;
             data.category = category;
 
             weekQuizIds.Add(data);
             
              
         }
 
         return weekQuizIds;
     }

    private void FetchQuizById(string id)
    {
        string url = "http://kidesolutions.fi/lingoquiz/GetQuizzbyID.php?id=";
        url += id;

        StartCoroutine(quizRequestManager.RequestQuizXml(url));
    }

    private void HandleOnQuizRequestFinished(WWW request)
    {
        List<WWW> completedRequests = quizRequestManager.GetFinishedRequests();
        if (completedRequests.Count >= requestsCount)
        {
            // all requests completed
            foreach (WWW www in completedRequests)
            {
                if (www.isDone)
                {
                    StoreCompletedQuiz(www.text);
                }
            }

            quizzesManager.SaveQuizzes();
            
			DownloadComplete();
        }
    }

    private void StoreCompletedQuiz(string quizXmlText)
    {
        Debug.Log("StoreCompletedQuiz() xml= " + quizXmlText);
        Quiz newQuiz = xmlManager.LoadXml(quizXmlText)[0];

        quizzesManager.StoreNewQuiz(newQuiz);
    }
}
