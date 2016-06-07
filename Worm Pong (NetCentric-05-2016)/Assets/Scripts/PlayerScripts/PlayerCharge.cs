using UnityEngine;
using System.Collections;

public class PlayerCharge : MonoBehaviour
{
	private bool isCurrentlyCharging;
	private float chargeTimeStart, throwTimeStart;

	public float timeToCharge, timeChargedThrowLasts;

	void Start ()
	{
		resetFlags ();
		resetTimers ();
	}

	void resetFlags ()
	{
		isCurrentlyCharging = false;
	}

	void resetTimers ()
	{
		chargeTimeStart = throwTimeStart = 0.0f;
	}

	public void setInput (float axis)
	{
		resetFlags ();
		if (axis != 0) {
			isCurrentlyCharging = true;
		}
	}

	public void asyncUpdate ()
	{
		float currentTime = Time.time;
		bool currentChargedTime = (Time.time - chargeTimeStart) >= timeToCharge;
		bool currentThrownTime = (Time.time - throwTimeStart) >= timeChargedThrowLasts;
		
		if (isCurrentlyCharging) {
			if (chargeTimeStart == 0.0f) { //first frame we're charging
				chargeTimeStart = currentTime;
			} else if (currentThrownTime) {
				//is throwing, but throw hasn't ended
				//no charging
			}
		} else { //is not charging
			if (currentChargedTime) { //charging is complete
				if (throwTimeStart == 0.0f) {
					throwTimeStart = currentTime;
				} else {
					if (currentThrownTime) { //thrown has ended
						resetTimers ();
					}
				}
			} else { //charging is not complete, reset
				resetTimers ();
			}
		}
	}

	public bool isCharging ()
	{
		return chargeTimeStart != 0.0f;
	}

	public bool isThrowingCharge ()
	{
		return throwTimeStart != 0.0f;
	}
}
