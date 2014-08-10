using UnityEngine;
using System.Collections;

public class GetQuizzbyID : MonoBehaviour {

	// Use this for initialization
	
        //This call get quizz by id into xml
    public string return_quizz(string id)
    {
        string url2 = "http://kidesolutions.fi/lingoquiz/GetQuizzbyID.php?id=";
        url2 = url2 += id;
        Requestwww wwwcall = gameObject.AddComponent<Requestwww>(); 
        WWW quiz = wwwcall.getxml(url2);
        string quiz_text = "";
        while (!quiz.isDone)
        {
            //Do nothing
            Debug.Log("wait for completion");
            if (quiz.isDone)
            {
                //Get string of xml from server, contain quizz data.
                quiz_text = quiz.text;
                Debug.Log(quiz_text);

                break;
            }
        }
        return quiz_text;
      
    }
	
	// Update is called once per frame
    void Update()
    {

    }
    

   
}
