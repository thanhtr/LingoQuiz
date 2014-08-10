using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Background : MonoBehaviour 
{
	public List<Color> colors;
	public List<Texture> textures;
	public Vector2 tiling;
	public Vector2 offset;

	private CrossFade crossFade;
	private MaterialColorsLerp bgColorLerp;
	private bool initialized = false;

	// Use this for initialization
	void Start () 
	{
		Initialize();
	}

	private void Initialize()
	{
		if (!initialized)
		{
			crossFade = GetComponentInChildren<CrossFade>();
			bgColorLerp = GetComponentInChildren<MaterialColorsLerp>();
			initialized = true;
			bgColorLerp.InstantChangeColor(colors[Random.Range(0, colors.Count)]);
		}
	}

	public void ChangeColorTo(int colorIndex)
	{
		if (colorIndex >= 0 || colorIndex < colors.Count)
		{
			Debug.Log("Lerp");
			bgColorLerp.ChangeToColor(colors[colorIndex]);
		}
	}

	public void ChangeTextureTo(int textureIndex)
	{
		if (textureIndex >= 0 || textureIndex < textures.Count)
		{
			crossFade.CrossFadeTo(textures[textureIndex], offset, tiling);
		}
	}
}
