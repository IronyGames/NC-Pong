using UnityEngine;
using System.Collections;

public class leftPlayer : MonoBehaviour
{
	private bool isUp, isDown;
	private float speed, acceleration;
	public float accelerationIncrement, friction, maxSpeed, maxAcceleration, minSpeedThreshold;
	public KeyCode upKey, downKey;
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
		if (Input.GetKeyDown (upKey)) {
			isUp = true;
		} else if (Input.GetKeyUp (upKey)) {
			isUp = false;
		}
		if (Input.GetKeyDown (downKey)) {
			isDown = true;
		} else if (Input.GetKeyUp (downKey)) {
			isDown = false;
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
			
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed);

		if (this.DEBUG == true) {
			print ("Acceleration: " + acceleration + ", Speed: " + speed + ", Position: " + transform.position.y);
		}
	}

	private void OnTriggerEnter2D (Collider2D other)
	{

	}

	private void evaluateWallTriggers ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		evauluateInput ();
		calculateKinematics ();
		evaluateWallTriggers ();
	}


}
