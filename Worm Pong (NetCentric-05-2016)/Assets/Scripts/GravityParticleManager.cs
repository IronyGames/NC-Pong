using UnityEngine;
using System.Collections;

public class GravityParticleManager : MonoBehaviour
{

	public int maximumParticles;
	public float secondsBetweenParticles;
	public float secondsToGrowParticle;
	public float maximumParticleCreationRadius;
	public float maximumParticleDestructionRadius;
	public float particleSpeed;
	public GameObject particle;

	private Vector2 center;
	private float particleGenerationDuration;
	private bool loop;
	private float timeOfLastParticle, timeGenerationStarted;

	void Start ()
	{
		stop ();
	}

	void Update ()
	{
		center = transform.position;
		float currentTime = Time.time;
		if (loop || ((particleGenerationDuration + timeGenerationStarted) > currentTime)) {
			if (timeGenerationStarted == 0.0f) {
				timeGenerationStarted = currentTime;
			}
				

			evaluateGeneratingNewParticles ();
		}
	
	}

	private void evaluateGeneratingNewParticles ()
	{
		int existingParticles = transform.childCount;


		float currentTime = Time.time;
		if (currentTime - timeOfLastParticle > secondsBetweenParticles && existingParticles < maximumParticles) {
			timeOfLastParticle = currentTime;
			generateNewParticle ();
		}
	}

	private void generateNewParticle ()
	{
		Vector2 position = (Random.insideUnitCircle * maximumParticleCreationRadius) + center;

		GameObject p = Instantiate (particle, position, Quaternion.identity) as GameObject;
		p.transform.parent = gameObject.transform;
		p.GetComponent<GravityParticle> ().setParameters (center, maximumParticleDestructionRadius);
		Vector2 s = center - position;
		s.Normalize ();
		p.GetComponent<Rigidbody2D> ().AddForce (s * particleSpeed);
		p.GetComponent<Rigidbody2D> ().AddTorque (Random.value * particleSpeed);
	}

	public void generateIndefinitely ()
	{
		loop = true;
	}

	public void stop ()
	{
		particleGenerationDuration = 0.0f;
		loop = false;
	}

	public void generateForAPeriod (float duration)
	{
		particleGenerationDuration = duration;
	}

	private void restartParticleGenerationCounter ()
	{
		particleGenerationDuration = 0.0f;
		timeGenerationStarted = 0.0f;
	}

}
