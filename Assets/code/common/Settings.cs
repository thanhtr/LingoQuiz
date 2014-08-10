using UnityEngine;
using System.Collections;

// Keeps, saves and loads a game settings.
public class Settings : MonoBehaviour {

	public enum SaveMode { UsePlayerPrefs, NoSave }
	public static Settings instance { get; private set; }

	[SerializeField]
	public float defaultMusicVolume;

	[SerializeField]
	public bool defaultMusicOn;

	[SerializeField]
	public bool defaultSoundOn;

	public SaveMode saveMode;

	public bool musicOn { get; set; }
	public bool soundOn { get; set; }

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		LoadDefaults();
		switch (saveMode)
		{
			case SaveMode.UsePlayerPrefs:
				LoadPlayerPrefs();
				break;
			default:
				break;
		}
	}

	void Start()
	{
		ApplyMusicSettings();
	}

	void OnDestroy()
	{
		SaveAll();
		PlayerPrefs.Save();

		if (instance == this)
		{
			instance = null;
		}
	}
	
	public void SaveAll()
	{
		switch (saveMode)
		{
			case SaveMode.UsePlayerPrefs:
				PlayerPrefs.SetInt("SoundOn", soundOn ? 1 : 0);
				PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
				break;
			default:
				break;
		}
	}

	public void ApplyMusicSettings()
	{
		if(Music.instance != null) 
		{
			float applyVolume = musicOn ? defaultMusicVolume : 0f;

			if (Music.instance.gameMusic != null) {
				Music.instance.gameMusic.volume = applyVolume;
			}

			if (Music.instance.menuMusic != null) {
				Music.instance.menuMusic.volume = applyVolume;
			}
		}
	}

	private void LoadDefaults()
	{
		musicOn = defaultMusicOn;
		soundOn = defaultSoundOn;
	}

	private void LoadPlayerPrefs()
	{
		if (PlayerPrefs.HasKey("MusicOn"))
			musicOn = PlayerPrefs.GetInt("MusicOn") > 0;
		else
			musicOn = defaultMusicOn;
		
		if (PlayerPrefs.HasKey("SoundOn"))
			soundOn = PlayerPrefs.GetInt("SoundOn") > 0;
		else
			soundOn = defaultSoundOn;
	}
}
