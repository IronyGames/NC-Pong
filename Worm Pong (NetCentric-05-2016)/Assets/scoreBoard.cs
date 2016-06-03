using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour
{

	private int score;
	public static int winScore;

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

	public int getScore ()
	{
		return score;
	}
		
}
