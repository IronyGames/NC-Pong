using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RelativeUI : MonoBehaviour
{

	public int fontSize;

	void Start ()
	{
	}

	void Update ()
	{
		Vector2 screenSize = new Vector2 (Screen.width, Screen.height);

		int actualFontSize = (int)screenSize.x / fontSize;

		GetComponent<Text> ().fontSize = actualFontSize;
	}

	private float evaluateRatio (float ratio)
	{
		if (ratio < 0.0f) {
			return 0.0f;
		} else if (ratio > 1.0f) {
			return 1.0f;
		}
		return ratio;
	}

	private Vector2 calculateActualVector (Vector2 relative, Vector2 screen)
	{
		float finalX = screen.x * relative.x, finalY = screen.y * relative.y;
		return new Vector2 (finalX, finalY);
	}
}
