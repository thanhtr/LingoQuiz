using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour 
{
	public void Play () 
	{
		if (Settings.instance.soundOn)
		{
			audio.Play();
		}
	}
}
