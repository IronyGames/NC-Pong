using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	private Vector2 direction;
	public float speed;
	public float bounceSpeedIncrement;
	public float maxSpeed;
	public int scoreToWin;
	// Use this for initialization

	private bool DEBUG = false;

	void Start ()
	{
		resetBall ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.DEBUG == true) {
			//print (", Speed: " + direction.ToString ());
		
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

			direction.y = -direction.y;
			if (direction.y < 0) {
				direction.y -= bounceSpeedIncrement;
			} else {
				direction.y += bounceSpeedIncrement;
			}

			if (direction.y > maxSpeed) {
				direction.y = maxSpeed;
			}

		} else if (coll.gameObject.tag.Equals ("paddle")) {
			if (this.DEBUG == true) {
				print ("Hit worm!");
			}

			direction.x = -direction.x;
			if (direction.x < 0) {
				direction.x -= bounceSpeedIncrement;
			} else {
				direction.x += bounceSpeedIncrement;
			}

			if (direction.x > maxSpeed) {
				direction.x = maxSpeed;
			}

			float paddlePosition = coll.transform.position.y;
			float paddleHeight = coll.transform.lossyScale.y;
			float ballPosition = transform.position.y;

			float paddlePart = (ballPosition - paddlePosition) / paddleHeight;

			direction.y = paddlePart * Mathf.Abs (direction.x) * 10;
		}

		setDirection ();
			
	}

	private void resetBall ()
	{
		GetComponent<Rigidbody2D> ().transform.position = new Vector2 (0, 0);
		direction = new Vector2 (-speed, 0);
		setDirection ();
		//scoreBoard.reset ();
	}

	private void addPointToLeft ()
	{
		//scoreBoard.scoreLeft ();
	}

	private void addPointToRight ()
	{
		//scoreBoard.scoreRight ();
	}

	private void setDirection ()
	{
		GetComponent<Rigidbody2D> ().velocity = direction;
	}
		
}