using UnityEngine;
using System.Collections;

public class QuizHundredPoints : MonoBehaviour {

	[SerializeField] private ParticleSystem nutParticles;
	
	void Awake () 
	{
		nutParticles.playOnAwake = false;
		AnimationHelper.SetToBeginning(animation);
	}
	
	private void PlayNutParticles()
	{
		nutParticles.Play();
	}
}
