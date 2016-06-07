using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private bool isUp, isDown, isCharging;
	private float speed, acceleration;
	public float accelerationIncrement, friction, maxSpeed, maxAcceleration, minSpeedThreshold;
	public KeyCode upKey, downKey;
	public string inputAxis;
	public string inputChargeButton;


	// Use this for initialization

	private bool DEBUG = false;

	void Start ()
	{
		isUp = false;
		isDown = false;
		speed = 0;
		if (friction > 1) { //it's a percentage!
			friction = 0.3f;
		}
	}

	private void evauluateInput ()
	{
		float axis = Input.GetAxisRaw (inputAxis);
		int charge = (int)Input.GetAxisRaw (inputChargeButton);

		if (DEBUG == true) {
			print ("Axis: " + axis);
		}

		isUp = false;
		isDown = false;

		if (axis == 1) {
			isUp = true;
		} else if (axis == -1) {
			isDown = true;
		}

		if (charge == 0) {
			isCharging = false;
		} else {
			isCharging = true;
		}


	}

	private void calculateKinematics ()
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

		if (this.DEBUG == true) {
			print ("Acceleration: " + acceleration + ", Speed: " + speed + ", Position: " + transform.position.y);
		}
	}

	void Update ()
	{
		evauluateInput ();
		calculateKinematics ();
	}

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed);
	}

}
