using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Generic store/load system for Game own data (not include settings).
/// </summary>
public class GameDataStore
{
    public string quizzesData;
    public string weekQuizId;
    public string selectedQuiz;
    public float exp;
    public int level;
	public int playTimes;
	public bool rated;

    public string achievementList;

    private static GameDataStore instance;
    public static GameDataStore Get()
    {
        if (instance == null)
        {
            instance = new GameDataStore();
            instance.Load();
        }
        return instance;
    }

    ~GameDataStore()
    {
        if (instance == this)
		{
			Debug.Log("destroy GameDataStore");
			instance = null;
		}
    }

    private void Load()
    {
		//ClearAllData();

        quizzesData = PlayerPrefs.GetString("quizzesData", "");
        weekQuizId = PlayerPrefs.GetString("weekQuizId", "");
        selectedQuiz = PlayerPrefs.GetString("selectedQuiz", "1");
        exp = PlayerPrefs.GetFloat("exp", 0);
		level = PlayerPrefs.GetInt("level", 0);
		playTimes = PlayerPrefs.GetInt("playTimes", 0);
		rated = PlayerPrefs.GetInt("rated", 0) == 1;

        achievementList = PlayerPrefs.GetString("achievementList", "");
    }

    public void Save()
    {
        PlayerPrefs.SetString("quizzesData", quizzesData);
        PlayerPrefs.SetString("weekQuizId", weekQuizId);
        PlayerPrefs.SetString("selectedQuiz", selectedQuiz);
        PlayerPrefs.SetFloat("exp", exp);
        PlayerPrefs.SetInt("level", level);
		PlayerPrefs.SetInt("playTimes", playTimes);
		PlayerPrefs.SetInt("rated", rated ? 1 : 0);

        PlayerPrefs.SetString("achievementList", achievementList);
        PlayerPrefs.Save();
    }

	public void ClearAllData()
	{
		quizzesData = "";
		weekQuizId = "";
		selectedQuiz = "";
		exp = 0;
		level = 0;
		achievementList = "";
		playTimes = 0;
		rated = false;
		Save();
	}
}
