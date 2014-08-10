using UnityEngine;
using System.Collections;

public static class Loader
{
	#region Public static functions
	public static void LoadMain()
	{
		LoadScene("startscreen");
	}

	public static void LoadQuiz()
	{
		LoadScene("quiz");
	}

    public static void LoadQuizList()
    {
        LoadScene("quizListFinal");
    }
	#endregion

	#region Private static functions
	private static void LoadScene(string scene)
	{
		Application.LoadLevel(scene);
	}
	#endregion
}
