using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
//	[SerializeField]
//	private List<GameObject> charactersList;
//
//    private QuizMainView quizMainView;
//    public Transform characterSpawnPoint;
//    private ViewSlider viewSlider;
//
//    // Use this for initialization
//    void Start()
//    {
//        viewSlider = FindObjectOfType<ViewSlider>();
//		HideCharacters();
//        quizMainView = FindObjectOfType<QuizMainView>();
//        viewSlider.OnSecondViewSlideAwayComplete += OnSecondViewSlideAwayComplete;
//        RenderCharacter();
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//
//    }
//
//    void RenderCharacter()
//    {
//        for (int i = 0; i < charactersList.Count; i++)
//        {
//            if (i == quizMainView.characterIndex)
//            {
//                charactersList[i].SetActive(true);
//            }
//        }
//    }
//    
//    private void OnSecondViewSlideAwayComplete()
//    {
//		HideCharacters();
//    }
//
//    void OnDestroy()
//    {
//        viewSlider.OnSecondViewSlideAwayComplete -= OnSecondViewSlideAwayComplete;
//    }
//
//	private void HideCharacters()
//	{
//		foreach (GameObject character in charactersList)
//		{
//			character.SetActive(false);
//		}
//	}

}
