using UnityEngine;
using System.Collections;
//This class handling the www call
public class Requestwww : MonoBehaviour {
    public static string text;
    
    //Request and return www.
   
   public IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {   
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

   public WWW getxml(string url)
   {
        WWW www = new WWW(url);
      
        StartCoroutine(WaitForRequest(www));
        
        return www;
    }
    

   

    
    
}
