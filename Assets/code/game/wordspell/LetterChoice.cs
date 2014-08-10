using UnityEngine;
using System.Collections;

public class LetterChoice : MonoBehaviour {
    public tk2dTextMesh title;
    public Transform bg;
    public Vector3 location;
    public delegate void WordChoice(LetterChoice choice);
    public event WordChoice OnWordChoiceClicked;
    public int tempAnswerIndex;
	// Use this for initialization
	void Start () {
        bg.GetComponent<InputDetector>().OnTouchAndRelease += OnTouchAndRelease;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetText(string s)
    {
        title.text = s;
    }

    public string GetText()
    {
        return title.text;
    }

    void OnTouchAndRelease(InputDetectorTouch touch)
    {
        if(OnWordChoiceClicked != null)
        {
            OnWordChoiceClicked(this);
        }
    }
}
