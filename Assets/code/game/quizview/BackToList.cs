using UnityEngine;
using System.Collections;

public class BackToList : MonoBehaviour {

    void Start()
    {
        GetComponent<InputDetector>().OnTouchAndRelease += OnTouchAndRelease;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchAndRelease(InputDetectorTouch touch)
    {
		Loader.LoadQuizList();
    }
}
