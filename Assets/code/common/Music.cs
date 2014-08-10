using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MusicTypeEnum
{
	None,
	Menu,
	Game
}

// A music player which serves as a handy refence to the gameobject 
// holding the music audiosource.
// It also destroys itself if another music player is already present
// (maybe from a previously loaded scene) such that we don't have to
// endure multiple music tracks playing simultaneously.
public class Music : MonoBehaviour {

	public static Music instance { get; private set; }
	
	public AudioSource menuMusic;
	public AudioSource gameMusic;

	public void Awake()
	{
		if (instance == null)
		{ 
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}

	public void PlayMusic(MusicTypeEnum musicType)
	{
		switch (musicType)
		{
			case MusicTypeEnum.Game:
				gameMusic.Play();
				break;
			case MusicTypeEnum.Menu:
				menuMusic.Play();
				break;
			case MusicTypeEnum.None:
				StopMusic();
				break;
		}
	}

	private void StopMusic()
	{
		gameMusic.Stop();
		menuMusic.Stop();
	}
}
