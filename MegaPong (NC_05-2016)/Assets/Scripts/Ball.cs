﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	private Vector2 direction;
	public float speed;
	public float bounceSpeedIncrement;
	public float maxSpeed;
	public int scoreToWin;

	public ScoreBoard leftScoreboard, rightScoreboard;
	public Player leftPaddle, rightPaddle;
	public GlobalVariables globalVariables;
	public GameObject winText;
	public SpriteRenderer leftArrow, rightArrow;
	public float secondsBeforeBallStartsMoving;
	private float timeSinceBallWasReset;

	private bool hasGameFinished;

	void Start ()
	{
		resetBall ("left");
		finishGame ();
	}


	void Update ()
	{
		if (Input.anyKeyDown && hasGameFinished) {
			hasGameFinished = false;
			winText.SetActive (false);
			resetBall ("left");
		}

		if (Input.GetAxisRaw ("Quit") != 0) {
			Application.Quit ();
		}
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (!hasGameFinished) {
			if (other.gameObject.name.Equals ("leftWall")) {
				addPointToRight ();
				resetBall ("left");
			} else if (other.gameObject.name.Equals ("rightWall")) {
				addPointToLeft ();
				resetBall ("right");

			}
			resetPaddles ();
			GetComponent<AudioSource> ().Play ();
			bool leftWon = leftScoreboard.getScore () == globalVariables.scoreToWin;
			bool rightWon = rightScoreboard.getScore () == globalVariables.scoreToWin;

			if (leftWon) {
				winText.GetComponent<Text> ().text = "Left player wins! \nPress any key to \nrestart";
				finishGame ();
			} else if (rightWon) {
				winText.GetComponent<Text> ().text = "Right player wins! \nPress any key to \nrestart";
				finishGame ();
			}
		}
	}

	private void OnCollisionEnter2D (Collision2D coll)
	{
		if (!hasGameFinished) {
			if (coll.gameObject.name.Equals ("upWall") || coll.gameObject.name.Equals ("downWall")) {
				direction.y = calculateBounceIncrementAndDirectionChange (direction.y, bounceSpeedIncrement);
				direction.y = evaluateMaxDirectionSpeed (direction.y, maxSpeed);

			} else if (coll.gameObject.tag.Equals ("paddle")) {
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
					playerHit.playFastBounce ();
				} else if (playerHit.isCharging ()) { //player is charging. ball will go slower.
					direction *= playerHit.getChargingModifier ();
					playerHit.playSlowBounce ();
				} else {
					playerHit.playNormalBounce ();
				}

			}
		}
	}

	private void resetPaddles ()
	{
		leftPaddle.resetPaddle ();
		rightPaddle.resetPaddle ();
	}

	private void hideArrows ()
	{
		leftArrow.enabled = false;
		rightArrow.enabled = false;
	}

	private void finishGame ()
	{
		winText.SetActive (true);
		direction = new Vector2 (0, 0);
		hasGameFinished = true;
		leftScoreboard.reset ();
		rightScoreboard.reset ();
		resetPaddles ();
		hideArrows ();
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

	private void resetBall (string toWhom)
	{
		GetComponent<Rigidbody2D> ().transform.position = new Vector2 (0, 0);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		timeSinceBallWasReset = Time.time;
		if (toWhom.Equals ("left")) {
			direction = new Vector2 (-speed, 0);
			leftArrow.enabled = true;
		} else {
			direction = new Vector2 (speed, 0);
			rightArrow.enabled = true;
		}

	}

	private void addPointToLeft ()
	{
		leftScoreboard.addScore ();
	}

	private void addPointToRight ()
	{
		rightScoreboard.addScore ();
	}

	void FixedUpdate ()
	{
		if (timeSinceBallWasReset + secondsBeforeBallStartsMoving < Time.time) {
			GetComponent<Rigidbody2D> ().velocity = direction;
			hideArrows ();
		}
	}
		
}