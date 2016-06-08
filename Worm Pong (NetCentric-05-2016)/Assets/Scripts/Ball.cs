using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	private Vector2 direction;
	public float speed;
	public float bounceSpeedIncrement;
	public float maxSpeed;
	public int scoreToWin;

	public ScoreBoard left, right;

	private bool DEBUG = false;

	void Start ()
	{
		resetBall ();
	}


	void Update ()
	{
		if (this.DEBUG == true) {
			print (", Speed: " + direction.ToString ());
		
		}
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name.Equals ("leftWall")) {
			if (this.DEBUG == true) {
				print ("Score for right!");
			}
			addPointToRight ();
			resetBall ();
		} else if (other.gameObject.name.Equals ("rightWall")) {
			if (this.DEBUG == true) {
				print ("Score for left!");
			}
			addPointToLeft ();
			resetBall ();
		}
	}

	private void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.name.Equals ("upWall") || coll.gameObject.name.Equals ("downWall")) {
			if (this.DEBUG == true) {
				print ("Hit superior/inferior wall!");
			}

			direction.y = calculateBounceIncrementAndDirectionChange (direction.y, bounceSpeedIncrement);
			direction.y = evaluateMaxDirectionSpeed (direction.y, maxSpeed);

		} else if (coll.gameObject.tag.Equals ("paddle")) {
			if (this.DEBUG == true) {
				print ("Hit worm!");
			}

			direction.x = calculateBounceIncrementAndDirectionChange (direction.x, bounceSpeedIncrement);
			direction.x = evaluateMaxDirectionSpeed (direction.x, maxSpeed);

			//determine ball angle
			float paddlePosition = coll.transform.position.y;
			float paddleHeight = coll.transform.lossyScale.y;
			float ballPosition = transform.position.y;

			float paddlePart = (ballPosition - paddlePosition) / paddleHeight;

			direction.y = paddlePart * Mathf.Abs (direction.x) * 10;

			//determine if paddle is charging
			Player playerHit = (Player)coll.gameObject.GetComponent<Player> ();
			if (playerHit.isThrowingCharge ()) { //player is throwing. ball will go faster.
				direction *= playerHit.getThrowingModifier ();
			} else if (playerHit.isCharging ()) { //player is charging. ball will go slower.
				direction *= playerHit.getChargingModifier ();
			}

		}
	}

	private float calculateBounceIncrementAndDirectionChange (float direction, float increment)
	{
		direction = -direction;
		if (direction < 0) {
			direction -= increment;
		} else {
			direction += increment;
		}
		return direction;
	}

	private float evaluateMaxDirectionSpeed (float direction, float max)
	{
		if (direction > max) {
			direction = max;
		}
		return direction;
	}

	private void resetBall ()
	{
		GetComponent<Rigidbody2D> ().transform.position = new Vector2 (0, 0);
		direction = new Vector2 (-speed, 0);
	}

	private void addPointToLeft ()
	{
		left.addScore ();
	}

	private void addPointToRight ()
	{
		right.addScore ();
	}

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D> ().velocity = direction;
	}
		
}