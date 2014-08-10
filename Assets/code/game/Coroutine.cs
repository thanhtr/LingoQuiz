using UnityEngine;
using System.Collections;


public class Coroutine : MonoBehaviour  {

	//On start, get url and call get method to get xml text
	void Start () {
        string url = "http://localhost/LingoQuizz/output.xml";
        WWW result = getxml(url);
     
        
	}
	
	//Start Coroutine to get www result
   
    public  WWW getxml(string url){
        WWW www = new WWW(url);
        Requestwww requestwww=new Requestwww();
        StartCoroutine(requestwww.WaitForRequest(www));
        return www;

    }
    }

