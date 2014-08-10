using UnityEngine;
using System.Collections;

public class GetAllQuizzID : MonoBehaviour {
    public string xmltext;
	// Use this for initialization
	void Start () {
        
        //This call get all quizz id into xml
        string url = "http://kidesolutions.fi/lingoquizGetAllQuizzID.php";
        Requestwww wwwcall = gameObject.AddComponent<Requestwww>();

        WWW link = wwwcall.getxml(url);
        while (!link.isDone)
        {
            //Do nothing
            Debug.Log("wait for completion");
            if (link.isDone)
            {
                //Get string of xml from server, contain only one value, quiz id of weekquiz.
                xmltext = link.text;

                break;
            }
        }
        Debug.Log("Here is all Quizz ID" + xmltext);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //Start Coroutine to get www result

   
}
