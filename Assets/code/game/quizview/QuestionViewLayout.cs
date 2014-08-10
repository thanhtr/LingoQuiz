using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionViewLayout : MonoBehaviour
{
    [SerializeField]
    public MultiFontText title;
    private QuizMainView quizMainView;
	private List<AnswerButton> buttons;

    #region Unity callbacks
	void Awake()
	{
		buttons = new List<AnswerButton>(GetComponentsInChildren<AnswerButton>());
	}

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Public functions
    public void InitQuestion(Question question)
    {
        InitAnswers(question);
        SetTitle(question.text);
    }

    public int ButtonCount()
    {
        return buttons.Count;
    }

    public List<AnswerButton> GetAnswerButtons()
    {
        return buttons;
    }

	public void DisableAllButtons()
	{
		foreach (AnswerButton button in buttons)
		{
			button.DisableButton();
		}
	}

	public void ShowOnlyAnswer(string answerId)
	{
		foreach (AnswerButton button in buttons)
		{
			if (button.answerId != answerId)
			{
				button.PlayHideButton();
			}
		}
	}
    #endregion

    #region Private functions
    private void SetTitle(string questionText)
    {
		title.SetText(questionText.ToLower());
    }

    private void InitAnswers(Question question)
    {

        for (int i = 0; i < buttons.Count; i++)
        {
            string answerText = question.answers[i].text;
			string answerId = question.answers[i].answerId;
			buttons[i].InitAnswer(answerId, answerText.ToLower());
        }
    }
    #endregion
}
