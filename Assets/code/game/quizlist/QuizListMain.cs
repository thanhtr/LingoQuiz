using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizListMain : MonoBehaviour {
	
    public GameObject listItemPrefab;
    public float itemsSpacing = 0.1f;

    private List<QuizListItem> listItems;
    private float nextItemPosY = 0f;
    private SwipeScrollerContent scrollerContent;
    private List<Quiz> quizzes;
	private QuizzesManager quizManager;

    void Start()
    {
        listItems = new List<QuizListItem>();
        scrollerContent = GetComponentInChildren<SwipeScrollerContent>();

		quizManager = FindObjectOfType(typeof(QuizzesManager)) as QuizzesManager;

        quizzes = new List<Quiz>(quizManager.GetQuizList());

        CreateQuizzesList();

		ShowAdBanner();
    }

    private void CreateQuizzesList()
    {
		nextItemPosY = -0.175f;
		Debug.Log("nextItemPosY = " + nextItemPosY);

        foreach (Quiz quiz in quizzes)
        {
            AddQuizToList(quiz);
        }

		Debug.Log("total height = " +nextItemPosY);

        scrollerContent.SetColliderHeight(nextItemPosY + 0.1f);
        GetComponentInChildren<SwipeScroller>().AlignContentToBounds();
    }

    private void AddQuizToList(Quiz quiz)
    {
        Vector3 newPosition = new Vector3(0, nextItemPosY, 0);

        GameObject newItem = (GameObject)Instantiate(listItemPrefab);
        newItem.transform.parent = scrollerContent.transform;
        newItem.transform.localPosition = newPosition;

        QuizListItem quizItem = newItem.GetComponent<QuizListItem>();
        quizItem.InitQuizItem(quiz.id, quiz.name, quiz.points, quiz.level);
        quizItem.OnQuizSelected += OnQuizSelected;

		Debug.Log("height: "+newItem.collider.bounds.size.y);

        listItems.Add(quizItem);
        nextItemPosY -= (newItem.collider.bounds.size.y + itemsSpacing);
    }

    private void OnQuizSelected(QuizListItem item)
    {
        string quizId = item.GetQuizId();

        GameDataStore.Get().selectedQuiz = quizId;

        Debug.Log("OnQuizSelected() quizId: " + quizId);

		Loader.LoadQuiz();
    }

	private void ShowAdBanner()
	{
		GoogleAdMobHelper adMob = FindObjectOfType(typeof(GoogleAdMobHelper)) as GoogleAdMobHelper;
		adMob.RequestBanner();
	}
}
