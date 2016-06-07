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
		updateText ();
	}

	public void reset ()
	{
		score = 0;
		updateText ();
	}

	public int getScore ()
	{
		return score;
	}

	private void updateText ()
	{
		GetComponent<Text> ().text = score.ToString ();
	}
		
}
