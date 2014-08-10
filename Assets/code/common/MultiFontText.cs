using UnityEngine;
using System.Collections;

public class MultiFontText : MonoBehaviour 
{
	[System.Serializable]
	public class FontSizeProperties {
		public int maxCharsInNormal = 50;
		public float smallFontSize = 0.5f;
		public float normalFontSize = 0.5f;
	}

	[SerializeField] bool dynamicFontSize = false;
	[SerializeField] FontSizeProperties fontSizeProperties;

	private tk2dTextMesh[] texts;
	private string text;

	private void TryInit()
	{
		if (texts == null)
		{
			texts = GetComponentsInChildren<tk2dTextMesh>();
		}
	}
	
	public void SetText(string newText)
	{
		TryInit();
		foreach (tk2dTextMesh tmesh in texts)
		{
			tmesh.text = newText;
		}
		if (dynamicFontSize)
		{
			DefineFontSize(newText);
		}
	}

	public string GetText()
	{
		return text;
	}

	public void SetColor(Color newColor)
	{
		foreach (tk2dTextMesh tmesh in texts)
		{
			tmesh.color = newColor;
		}
	}

	private void DefineFontSize(string text)
	{
		float fontSize = fontSizeProperties.normalFontSize;
		if (text.Length > fontSizeProperties.maxCharsInNormal)
		{
			fontSize = fontSizeProperties.smallFontSize;
		}

		foreach (tk2dTextMesh tmesh in texts)
		{
			tmesh.scale = new Vector3(fontSize, fontSize, fontSize);
		}
	}
}
