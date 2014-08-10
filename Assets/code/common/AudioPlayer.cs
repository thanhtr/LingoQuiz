using UnityEngine;
using System.Collections;

/// <summary>
/// Audio player manager. Play all sound and music through this instead of using directly AudioSource.Play()
/// </summary>
/// 
public class AudioPlayer : MonoBehaviour {

	public static AudioPlayer instance { get; private set; }

	#region Unity callbacks
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	#endregion

	#region Public Methdos
	public void PlaySound(AudioSource sound)
	{
		if (Settings.instance.soundOn)
		{
			sound.Play();
		}
	}

	public void PlayMusic(AudioSource sound)
	{
		if (Settings.instance.musicOn)
		{
			sound.Play();
		}
	}

	public void Stop(AudioSource sound)
	{
		sound.Stop();
	}

	#endregion
}
