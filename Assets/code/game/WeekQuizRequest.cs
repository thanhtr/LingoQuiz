using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class WeekQuizRequest
{
	public delegate void RequestEvents(WWW request);
	public event RequestEvents OnRequestFinished;
	
	private float maxLagTime = 10f; 
	
	public IEnumerator RequestWeekQuizzes(string requestUrl)
	{
		Debug.Log("RequestWeekQuizzes() start: '"+requestUrl+"'");
		WWW wwwRequest = new WWW(requestUrl);
		
		float lagTime = 0f;
		float previousProgress = 0f;
		float requestStartTime = Time.time;
		
		while (!wwwRequest.isDone)
		{
			if (!string.IsNullOrEmpty(wwwRequest.error))
			{
				Debug.Log("RequestWeekQuizzes failed! Error: "+ wwwRequest.error);
				HandleRequestError(wwwRequest);
				yield break;
			}
			
			bool lagging = Mathf.Approximately(wwwRequest.progress, previousProgress);
			if (lagging) 
			{
				lagTime += Time.deltaTime;
				if (lagTime > maxLagTime)
				{
					HandleRequestTimedOut(wwwRequest);
					yield break;			
				}
			}		
			previousProgress = wwwRequest.progress;
			
			yield return null;
		}
		
		float totalTime = Time.time - requestStartTime;
		Debug.Log("RequestWeekQuizzes() finished: '"+requestUrl+"' Took " + totalTime + " sec to complete, lag was " + lagTime + " sec");
		
		HandleRequestDone(wwwRequest);
	} 
	
	private void HandleRequestTimedOut(WWW request)
	{
		RequestFinished(request);
	}
	
	private void HandleRequestError(WWW request)
	{
		RequestFinished(request);
	}
	
	private void HandleRequestDone(WWW request)
	{
		RequestFinished(request);
	}
	
	private void RequestFinished(WWW request)
	{
		if (OnRequestFinished != null)
		{
			OnRequestFinished(request);
		}
	}
}
