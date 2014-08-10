using UnityEngine;
using System.Collections;

public class ToggleSoundAndMusic : MonoBehaviour
{

    public Settings setting;
    private bool soundOn, musicOn;
    private tk2dSprite sprite;
    // Use this for initialization
    void Start()
    {
        GetComponent<InputDetector>().OnTouchAndRelease += OnTouchAndRelease;
        sprite = GetComponent<tk2dSprite>();
        soundOn = setting.soundOn;
        musicOn = setting.musicOn;
        if (soundOn)
        {
            //textMesh.text = "sound: on";
            sprite.SetSprite("sound");
        }
        else
        {
            //textMesh.text = "sound: off";
            sprite.SetSprite("mute");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchAndRelease(InputDetectorTouch touch)
    {
        if (soundOn)
        {
            //textMesh.text = "sound: off";
            sprite.SetSprite("mute");
            soundOn = musicOn = false;
        }
        else
        {
            //textMesh.text = "sound: on";
            sprite.SetSprite("sound");
            soundOn = musicOn = true;
        }

        setting.soundOn = soundOn;
        setting.musicOn = musicOn;
        setting.SaveAll();
    }
}
