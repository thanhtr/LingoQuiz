using UnityEngine;
using System.Collections;

public class SpriteFadeOut : MonoBehaviour 
{
	public string onCompleteMessage = "";
	private tk2dSprite sprite;
	private Color origSpriteColor;

	public void FadeOut(tk2dSprite _sprite, float time)
	{
		sprite = _sprite;
		origSpriteColor = sprite.color;

		iTweenParams p = new iTweenParams();
		p.time = time;
		p.from = 1f;
		p.to = 0f;
		p.onUpdate = "OnSpriteFadeOutUpdate";
		p.onUpdateTarget = gameObject;
		if (onCompleteMessage != "")
		{
			p.onComplete = onCompleteMessage;
			p.onCompleteTarget = gameObject;
		}

		iTween.ValueTo(gameObject, p.ToHash());
	}

	private void OnSpriteFadeOutUpdate(float newValue)
	{
		sprite.color = new Color(origSpriteColor.r * newValue, 
		                         origSpriteColor.g * newValue, 
		                         origSpriteColor.b * newValue);
	}
}
