using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RelativeUI : MonoBehaviour
{
	public Vector2 positionPercent;

	private Vector2 positionUnary;
	private static float percentToUnitRate = 100.0f;

	public int fontSize;

	void Start ()
	{
		
		positionUnary = new Vector2 (evaluateRatio (positionPercent.x / percentToUnitRate), evaluateRatio (positionPercent.y / percentToUnitRate));
	}

	void Update ()
	{
		Vector2 screenSize = new Vector2 (Screen.width, Screen.height);


		Vector2 actualPosition = calculateActualVector (positionUnary, screenSize);
		int actualFontSize = (int)screenSize.x / fontSize;
	
		GetComponent<RectTransform> ().anchoredPosition.Set (actualPosition.x, actualPosition.y);

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
