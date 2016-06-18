using UnityEngine;
using System.Collections;

public class GravityParticle : MonoBehaviour
{
	private Vector2 center;
	private float destructionDistance;
	private float originalDistanceToCenter;
	private float currentDistanceToCenter;
	private Vector3 originalScale;


	void Update ()
	{
		currentDistanceToCenter = Vector2.Distance (transform.position, center);
		evaluateRadius ();
		evaluateDestruction ();

	}

	private void evaluateRadius ()
	{
		float currentScale = currentDistanceToCenter / originalDistanceToCenter;
		transform.localScale = originalScale * currentScale;
	}

	private void evaluateDestruction ()
	{
		if (currentDistanceToCenter < destructionDistance) {
			Destroy (gameObject);
		}
	}

	public void setParameters (Vector2 particleCenter, float distanceFromCenterToDestruction)
	{
		center = particleCenter;
		originalScale = transform.localScale;
		destructionDistance = distanceFromCenterToDestruction;
		originalDistanceToCenter = Vector2.Distance (transform.position, center);
	}
}
