using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour
{

	private int score;

	// Use this for initialization
	void Start ()
	{
		reset ();
	}

	public void addScore ()
	{
		score++;
	}

	public void reset ()
	{
		score = 0;
	}
}
