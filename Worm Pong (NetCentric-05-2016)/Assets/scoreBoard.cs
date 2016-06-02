using UnityEngine;
using System.Collections;

public class scoreBoard : MonoBehaviour
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
