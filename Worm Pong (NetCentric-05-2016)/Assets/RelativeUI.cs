using UnityEngine;
using System.Collections;

public class RelativeUI : MonoBehaviour
{
	public Vector2 positionPercent;

	private Vector2 positionUnary;
	private static float percentToUnitRate = 100.0f;

	void Start ()
	{
		positionUnary = new Vector2 (evaluateRatio (positionPercent.x / percentToUnitRate), evaluateRatio (positionPercent.y / percentToUnitRate));
	}

	void Update ()
	{
		float screenX = Screen.width, screenY = Screen.height;
		float finalX = screenX / positionUnary.x, finalY = screenY / positionUnary.y;
		GetComponent<RectTransform> ().position.Set (finalX, finalY, 0);
		print (GetComponent<RectTransform> ().position.ToString ());
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
}
