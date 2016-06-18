using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	private float speed, acceleration;
	public float accelerationIncrement, friction, maxSpeed, maxAcceleration, minSpeedThreshold;
	private bool isUp, isDown;

	void Start ()
	{
		reset ();
		if (friction > 1) { //it's a percentage!
			friction = 0.3f;
		}
		speed = 0;
	}

	public void reset ()
	{
		isUp = isDown = false;
	}

	public void setInput (float axis)
	{
		reset ();

		if (axis == 1) {
			isUp = true;
		} else if (axis == -1) {
			isDown = true;
		}
	}

	public void asyncUpdate ()
	{
		//take care of input
		if (isUp) {
			acceleration = accelerationIncrement;
		} else if (isDown) {
			acceleration = -accelerationIncrement;
		} else {
			acceleration = 0;
		}
		if (acceleration > maxAcceleration) {
			acceleration = maxAcceleration;
		} else if (acceleration < -maxAcceleration) {
			acceleration = -maxAcceleration;
		}

		//process into speed.
		speed += (acceleration);
		speed *= (1 - friction);

		if (speed > maxSpeed) {
			speed = maxSpeed;
		} else if (speed < -maxSpeed) {
			speed = -maxSpeed;
		}

		if (speed < minSpeedThreshold && speed > -minSpeedThreshold) {
			speed = 0;
		}
	}

	public Vector2 getResults ()
	{
		return new Vector2 (0, speed);
	}
}
