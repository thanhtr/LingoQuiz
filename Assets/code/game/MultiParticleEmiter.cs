using UnityEngine;
using System.Collections;

public class MultiParticleEmiter : MonoBehaviour {

	private bool initialized = false;
	private ParticleSystem[] particles;

	void Start()
	{
		Init();
	}

	private void Init()
	{
		if (!initialized)
		{
			initialized = true;
			particles = GetComponentsInChildren<ParticleSystem>();
		}
	}

	public void PlayParticles()
	{
		foreach (ParticleSystem part in particles)
		{
			part.Play();
		}
	}

	public void PlayParticlesTimed(float time)
	{
		foreach (ParticleSystem part in particles)
		{
			part.Play();
		}
		CancelInvoke("StopParticles");
		Invoke("StopParticles", time);
	}

	public void StopParticles()
	{
		foreach (ParticleSystem part in particles)
		{
			part.Stop();
		}
	}
}
