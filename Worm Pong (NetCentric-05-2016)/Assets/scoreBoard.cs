using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
		GetComponent<Text> ().text = score.ToString ();
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
