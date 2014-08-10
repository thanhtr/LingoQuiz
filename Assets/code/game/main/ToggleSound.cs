using UnityEngine;
using System.Collections;

public class ToggleSound : MonoBehaviour
{

    public Settings setting;
    private bool soundOn;
    public tk2dTextMesh textMesh;
    // Use this for initialization
    void Start()
    {

        //soundOn = setting.defaultSoundOn;
        soundOn = setting.soundOn;
        if (soundOn)
        {
            textMesh.text = "sound: on";

        }
        else
        {
            textMesh.text = "sound: off";

        }
        GetComponent<InputDetector>().OnTouchAndRelease += OnTouchAndRelease;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchAndRelease(InputDetectorTouch touch)
    {
        if (soundOn)
        {
            textMesh.text = "sound: off";
            soundOn = false;
        }
        else
        {
            textMesh.text = "sound: on";
            soundOn = true;
        }

        setting.soundOn = soundOn;
        setting.SaveAll();
    }

    void ChangeButtonText()
    {

    }
}
