using UnityEngine;
using System.Collections;

public class ToggleMusic : MonoBehaviour
{

    public Settings setting;
    private bool musicOn;
    public tk2dTextMesh textMesh;
    // Use this for initialization
    void Start()
    {

        //soundOn = setting.defaultSoundOn;
        musicOn = setting.musicOn;
        if (musicOn)
        {
            textMesh.text = "music: on";

        }
        else
        {
            textMesh.text = "music: off";

        }
        GetComponent<InputDetector>().OnTouchAndRelease += OnTouchAndRelease;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchAndRelease(InputDetectorTouch touch)
    {
        if (musicOn)
        {
            textMesh.text = "music: off";
            musicOn = false;
        }
        else
        {
            textMesh.text = "music: on";
            musicOn = true;
        }
        
        setting.musicOn = musicOn;
        setting.SaveAll();
    }

    void ChangeButtonText()
    {


    }
}
