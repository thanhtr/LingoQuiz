using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizMainView : MonoBehaviour
{
    [SerializeField]
    private GameObject explonationViewPrefab;
    [SerializeField]
    private ResultView resultView;
    [SerializeField]
    private QuizNumberDisplay questionNumberDisplay;
    [SerializeField]
    private Animation hunderPointsAnimation;
    [SerializeField]
    private MultiFontText pointsText;
    [SerializeField]
    private ParticleSystem winParticles;
    [SerializeField]
    private KeyboardManager keyboardManager;

    enum EnumActionsAfterAd
    {
        None,
        QuitQuiz,
        RetryQuiz
    }
    private EnumActionsAfterAd actionAfterAd;

    private Quiz quiz;
    private Question currentQuestion;
    private ViewSlider viewSlider;
    private List<QuestionViewLayout> questionViewLayouts;
    private int questionIndex;
    private bool answeredCorrect;
    private ExplonationView explonationView;
    private QuizzesManager quizzesManager;
    private QuestionViewLayout currentQuestionViewLayout;

    private PlayAdsHelper playAdsHelper;

    private int totalPoints;
    private float gainExpPoints;

    public int correctAnswerCount { get; private set; }
    public int totalAnswer { get; private set; }
    public int characterIndex { get; private set; }

    private bool waitingForTouch = false;
    private bool hasNextQuestion = true;

    private const int CORRECT_ANSWER_POINTS = 100;

    // Use this for initialization
    void Start()
    {
        string quizId = GameDataStore.Get().selectedQuiz;
        if (quizId == "")
        {
            quizId = "1";
        }
        resultView.OnResultNextButtonPressed += OnResultNextButtonPressed;
        resultView.OnResultRetryButtonPressed += OnResultRetryButtonPressed;

        quizzesManager = FindObjectOfType(typeof(QuizzesManager)) as QuizzesManager;

        GetComponent<InputDetector>().OnTouchReleaseAnywhere += OnTouchReleaseAnywhere;

        viewSlider = GetComponentInChildren<ViewSlider>();

        questionViewLayouts = new List<QuestionViewLayout>(FindObjectsOfType<QuestionViewLayout>());
        keyboardManager = FindObjectOfType<KeyboardManager>();
        quiz = quizzesManager.GetQuiz(quizId);
        if (quiz != null)
        {
            StartQuiz();
        }

        InitializePlayAds();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentQuestion != null && currentQuestion.isInput)
            keyboardManager.ShowKeyboard(TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default));
    }

    #region Quiz create functions
    private void StartQuiz()
    {
        ResetQuizVariables();
        UpdateTotalPointsText();

        InitNewQuestion();

        viewSlider.AttachToCenterOfScreen(currentQuestionViewLayout.transform);
    }

    private void ResetQuizVariables()
    {
        correctAnswerCount = 0;
        totalAnswer = 1;
        characterIndex = 0;
        questionIndex = 0;
        totalPoints = 0;
        gainExpPoints = 0f;
    }

    private void InitNewQuestion()
    {
        currentQuestion = quiz.questions[questionIndex];
        CreateNewQuestionView(currentQuestion);

        totalAnswer++;
    }

    private void UpdateTotalPointsText()
    {
        pointsText.SetText(totalPoints.ToString());
    }

    private void CreateNewQuestionView(Question question)
    {
        int answersCount = question.answers.Count;
        QuestionViewLayout layoutTemplate = questionViewLayouts.Find(b => b.ButtonCount() == answersCount);

        currentQuestionViewLayout = InstantiateViewPrefab(layoutTemplate.gameObject).GetComponent<QuestionViewLayout>();
        currentQuestionViewLayout.InitQuestion(question);

        foreach (AnswerButton answerButton in currentQuestionViewLayout.GetAnswerButtons())
        {
            answerButton.OnAnswerClicked += OnAnswerSelected;
        }
    }

    private void CreateExplonationView(Question question)
    {
        explonationView = InstantiateViewPrefab(explonationViewPrefab).GetComponent<ExplonationView>();
    }

    private GameObject InstantiateViewPrefab(GameObject viewPrefab)
    {
        GameObject newView = (GameObject)Instantiate(viewPrefab);
        newView.transform.parent = transform;
        newView.transform.name = viewPrefab.transform.name;
        return newView;
    }

    #endregion

    private void OnNextButtonPressed(NextQuestionButton button)
    {
    }


    #region Quiz Answering
    private void OnAnswerSelected(AnswerButton answerButton)
    {
        if (answerButton.AnswerId() == currentQuestion.correctAnswerId)
        {
            CorrectAnswerSelected();
        }
        else
        {
            WrongAnswerSelected();
        }
    }

    private void WrongAnswerSelected()
    {
        PlayShowCorrectAnswer();
    }

    private void CorrectAnswerSelected()
    {
        AnimationHelper.SetToBeginningAndPlay(hunderPointsAnimation);
        PlayShowCorrectAnswer();

        totalPoints += CORRECT_ANSWER_POINTS;
        Invoke("UpdateTotalPointsText", 1.1f);

        winParticles.Play();
    }

    private void PlayShowCorrectAnswer()
    {
        currentQuestionViewLayout.DisableAllButtons();

        hasNextQuestion = true;
        if (questionIndex >= quiz.questions.Count - 1)
        {
            hasNextQuestion = false;
        }

        Invoke("ShowCorrectAnswer", 0.1f);
        if (hasNextQuestion)
        {
            Invoke("CreateNextQuestion", 0.2f);
        }
        else
        {
            Invoke("CreateResultView", 0.2f);
        }
        Invoke("StartWaitForTouch", 0.5f);
    }

    private void ShowCorrectAnswer()
    {
        currentQuestionViewLayout.ShowOnlyAnswer(currentQuestion.correctAnswerId);
    }

    private void StartWaitForTouch()
    {
        waitingForTouch = true;
    }

    private void CreateNextQuestion()
    {
        questionIndex++;
        InitNewQuestion();
        viewSlider.AttachToNewScreenPoint(currentQuestionViewLayout.transform);
    }

    private void OnTouchReleaseAnywhere(InputDetectorTouch touch)
    {
        if (waitingForTouch)
        {
            waitingForTouch = false;

            if (hasNextQuestion)
            {
                SlideToNextView();
            }
            else
            {
                ShowResultView();
            }

            Invoke("ChangeDisplayNumber", 0.25f);
        }
    }

    private void SlideToNextView()
    {
        viewSlider.SlideToLeft();
    }

    private void ChangeDisplayNumber()
    {
        questionNumberDisplay.ShowNumber(questionIndex + 1);
    }

    private void ShowResultView()
    {
        if (totalPoints > quiz.points)
        {
            quiz.points = totalPoints;
        }

        int points = totalPoints;

        float gainExp = GetEarnedExpPoints(points);

        resultView.ShowQuizResult(points, gainExp);

        PreparePlayAdsAd();
    }

    private float minGainExp = 5f;
    private float GetEarnedExpPoints(int _points)
    {
        float expPoints = minGainExp;

        /* If previous points from this quiz is 500, and new _points are 700, 
         * then pointFromPrevious is 200 (but never less than 0). If player have already scored max points
         * from the quiz, he will only get minGainExp if repeating same the quiz.
         */
        float pointFromPrevious = _points - quiz.points;
        expPoints += Mathf.Clamp(pointFromPrevious, 0f, pointFromPrevious);
        return expPoints;
    }

    #endregion

    #region Resultview events
    private void OnResultNextButtonPressed()
    {
        actionAfterAd = EnumActionsAfterAd.QuitQuiz;
        ShowPlayAdsAd();
    }

    private void OnResultRetryButtonPressed()
    {
        actionAfterAd = EnumActionsAfterAd.RetryQuiz;
        ShowPlayAdsAd();
    }
    #endregion


    #region PlayAds Functions
    private void InitializePlayAds()
    {
        playAdsHelper = GetComponentInChildren<PlayAdsHelper>();
        playAdsHelper.OnPlayAdsIntersialReady += OnPlayAdsIntersialReady;
        playAdsHelper.OnPlayAdsIntersialFailed += OnPlayAdsIntersialFailed;
        playAdsHelper.OnPlayAdsIntersialClosed += OnPlayAdsIntersialClosed;
    }

    private void ShowPlayAdsAd()
    {
        if (playAdsHelper != null)
        {
            playAdsHelper.PlayAds_Show();
        }
    }

    private void PreparePlayAdsAd()
    {
        if (playAdsHelper != null)
        {
            playAdsHelper.PlayAds_Cache();
        }
    }

    private void OnPlayAdsIntersialFailed()
    {
        AdFinished();
    }

    private void OnPlayAdsIntersialClosed()
    {
        AdFinished();
    }

    private void OnPlayAdsIntersialReady()
    {
    }

    private void AdFinished()
    {
        Debug.Log("AdFinished() " + actionAfterAd.ToString());
        switch (actionAfterAd)
        {
            case EnumActionsAfterAd.QuitQuiz:
                QuitQuiz();
                break;
            case EnumActionsAfterAd.RetryQuiz:
                RetryQuiz();
                break;
            default:
                QuitQuiz();
                break;
        }
    }

    #region Quiz End functions
    private void QuitQuiz()
    {
        GameDataStore.Get().Save();
        Loader.LoadQuizList();
    }

    private void RetryQuiz()
    {
        resultView.ResetView();
        StartQuiz();
        SlideToNextView();
    }
    #endregion
    #endregion
}

