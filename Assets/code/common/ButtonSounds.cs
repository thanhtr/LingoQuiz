using UnityEngine;
using System.Collections;

public class ButtonSounds : MonoBehaviour 
{
	private InputDetector inputDetector;
	public AudioSource touchSound;
	public AudioSource releaseSound;
	public AudioSource touchAndReleseSound;

	private bool isPressed = false;

	// Use this for initialization
	void Start () 
	{
		inputDetector = GetComponent<InputDetector>();
		inputDetector.OnTouch += OnTouch;
		inputDetector.OnTouchRelease += OnRelease;
		inputDetector.OnTouchAndRelease += OnTouchAndRelease;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTouch(InputDetectorTouch touch)
	{
		Debug.Log("play touch sound");
		if (touchSound != null)
		{
			PlaySound(touchSound);
			isPressed = true;
		}
	}

	private void OnRelease(InputDetectorTouch touch)
	{
		if (releaseSound != null && isPressed)
		{
			PlaySound(releaseSound);
			isPressed = false;
		}
	}

	private void OnTouchAndRelease(InputDetectorTouch touch)
	{
		if (touchAndReleseSound != null)
		{
			PlaySound(touchAndReleseSound);
		}
	}

	private void PlaySound(AudioSource sound)
	{
		if (sound.isPlaying)
		{
			sound.Stop();
		}

		AudioPlayer.instance.PlaySound(sound);
	}
}
