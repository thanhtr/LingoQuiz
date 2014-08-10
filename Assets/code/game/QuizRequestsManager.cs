using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizRequestsManager
{
	public delegate void QuizRequestEvents(WWW request);
	public event QuizRequestEvents OnQuizRequestFinished;

	private float maxLagTime = 10f; 
	private List<WWW> finishedRequests;

	public IEnumerator RequestQuizXml(string requestUrl)
	{
		Debug.Log("RequestQuizXml() start: '"+requestUrl+"'");
		WWW quizRequest = new WWW(requestUrl);

		float lagTime = 0f;
		float previousProgress = 0f;
		float requestStartTime = Time.time;

		while (!quizRequest.isDone)
		{
			if (!string.IsNullOrEmpty(quizRequest.error))
			{
				Debug.Log("RequestQuizXml failed! Error: "+ quizRequest.error);
				OnRequestError(quizRequest);
				yield break;
			}

			bool lagging = Mathf.Approximately(quizRequest.progress, previousProgress);
			if (lagging) 
			{
				lagTime += Time.deltaTime;
				if (lagTime > maxLagTime)
				{
					OnRequestTimedOut(quizRequest);
					yield break;			
				}
			}		
			previousProgress = quizRequest.progress;

			yield return null;
		}

		float totalTime = Time.time - requestStartTime;
		Debug.Log("RequestQuizXml() finished: '"+requestUrl+"' Took " + totalTime + " sec to complete, lag was " + lagTime + " sec");

		OnRequestQuizXmlDone(quizRequest);
	} 

	public List<WWW> GetFinishedRequests()
	{
		return finishedRequests;
	}

	private void OnRequestTimedOut(WWW request)
	{
		OnRequestFinished(request);
	}

	private void OnRequestError(WWW request)
	{
		OnRequestFinished(request);
	}

	private void OnRequestQuizXmlDone(WWW request)
	{
		OnRequestFinished(request);
	}

	private void OnRequestFinished(WWW request)
	{
		AddRequestToFinishedList(request);

		if (OnQuizRequestFinished != null)
		{
			OnQuizRequestFinished(request);
		}
	}

	private void AddRequestToFinishedList(WWW request)
	{
		if (finishedRequests == null) {
			finishedRequests = new List<WWW>();
		}
		finishedRequests.Add(request);
	}
}