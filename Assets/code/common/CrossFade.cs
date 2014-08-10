using UnityEngine;
using System.Collections;
/// <summary>
/// Cross fade. Uses CrossFade.shader to blend texture to another
/// </summary>
public class CrossFade : MonoBehaviour
{
	public float blendSpeed = 3.0f;

	private Texture newTexture;
	private Vector2 newOffset;
	private Vector2 newTiling;

	private bool trigger = false;
	private float fader = 0f;
	
	void Start ()
	{
		//renderer.material.SetFloat( "_Blend", 0f);
	}
	
	void Update ()
	{
		if (trigger)
		{
			fader += Time.deltaTime * blendSpeed;
			
			renderer.material.SetFloat( "_Blend", fader );
			
			if ( fader >= 1.0f )
			{
				trigger = false;
				fader = 0f;
				
				renderer.material.SetTexture ("_MainTex", newTexture );
				renderer.material.SetTextureOffset ( "_MainTex", newOffset );
				renderer.material.SetTextureScale ( "_MainTex", newTiling );
				renderer.material.SetFloat( "_Blend", 0f );
			}
		}
	}
	
	public void CrossFadeTo(Texture curTexture, Vector2 offset, Vector2 tiling)
	{
		newOffset = offset;
		newTiling = tiling;
		newTexture = curTexture;
		renderer.material.SetTexture( "_Texture2", curTexture );
		renderer.material.SetTextureOffset ( "_Texture2", newOffset );
		renderer.material.SetTextureScale ( "_Texture2", newTiling );
		trigger = true;
	}
}
