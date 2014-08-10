using UnityEngine;
using System.Collections;

public class AnimationHelper 
{
	
	public static void StopAnimation(Animation animation)
	{
		foreach (AnimationState state in animation) 
		{
	    	//state.normalizedSpeed = 0F;
			state.speed = 0f;
		}	
	}	
	
	public static void PlayAnimation(Animation animation)
	{
		foreach (AnimationState state in animation) 
		{
	    	//state.normalizedSpeed = 1F;
			state.speed = 1f;
		}	
	}
	
	public static void SetToBeginning(Animation animation)
	{
		animation.Play();
		
		foreach (AnimationState state in animation) 
		{
	    	state.time = 0F;
		}
		
		StopAnimation(animation);	
	}
	
	public static void SetToPercentageAndPlay(Animation animation, float percentage)
	{
		animation.Play();
		
		foreach (AnimationState state in animation) 
		{
	    	state.normalizedTime = percentage;
			state.speed = 1F;
		}
	}	
	
	public static void SetToEnd(Animation animation)
	{
		animation.Play();
		
		foreach (AnimationState state in animation) 
		{
	    	state.normalizedTime = 1F;
			state.speed = 0F;
		}
		animation.Sample();
	}	
	
	public static void SetToBeginningAndPlay(Animation animation)
	{
		SetToBeginning(animation);
		PlayAnimation(animation);
	}	
	
	public static void SetPlaySpeed(Animation animation, float speed)
	{
		foreach (AnimationState state in animation) 
		{
			state.speed = speed;
		}
	}	
	
	public static bool IsAnimationPlaying(Animation animation)
	{
		bool isPlaying = false;
		foreach (AnimationState state in animation) 
		{
			if (state.speed != 0f)
			{
				isPlaying = true;
			}
		}
		return isPlaying;
	}
}
