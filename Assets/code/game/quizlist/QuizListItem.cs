using UnityEngine;
using System.Collections;

public class QuizListItem : ScrollListItem
{
    public delegate void QuizListItemEvents(QuizListItem item);
    public event QuizListItemEvents OnQuizSelected;

    public MultiFontText quizText;
    public tk2dTextMesh pointTextMesh;
	public MultiFontText levelText;

    private string title;
    private string quizId;
    private int totalPoint;
	private QuizDifficultyLevelEnum level;
    

	void Start()
    {
        OnItemSelected = OnQuizItemSelected;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void InitQuizItem(string id, string name, int point, QuizDifficultyLevelEnum difficultyLvl)
    {
        quizId = id;
        title = name;
        totalPoint = point;
		level = difficultyLvl;

        quizText.SetText(title);
        pointTextMesh.text = totalPoint.ToString();

		SetDifficultyLevelText(difficultyLvl);
    }

    public string GetText()
    {
        return title;
    }

    public string GetQuizId()
    {
        return quizId;
    }

	private void SetDifficultyLevelText(QuizDifficultyLevelEnum lvl)
	{
		string lvlText = System.Enum.GetName(typeof(QuizDifficultyLevelEnum), (int)lvl);
		levelText.SetText(lvlText);
	}

    #region Button Overrides
    private void OnQuizItemSelected(ScrollListItem item)
    {
        if (OnQuizSelected != null)
        {
            OnQuizSelected(this);
        }
    }

    #endregion
}
