using UnityEngine;
using System.Collections;


public class PlayButton : MonoBehaviour
{
    // Use this for initialization
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
