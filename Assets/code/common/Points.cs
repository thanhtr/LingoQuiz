using UnityEngine;
using System.Collections;

// Stores, saves and load player points
public class Points : MonoBehaviour {
	
	public static Points instance { get; private set; }
	
	public int points { get; set; }
	
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
		
		LoadPlayerPrefs();
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
		PlayerPrefs.SetInt("Points", points);
	}

	private void LoadPlayerPrefs()
	{
		if (PlayerPrefs.HasKey("Points")) {
			points = PlayerPrefs.GetInt("Points");
		}
		else {
			points = 0;
		}

	}
}
