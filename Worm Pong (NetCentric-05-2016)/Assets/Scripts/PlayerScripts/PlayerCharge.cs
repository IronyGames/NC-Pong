using UnityEngine;
using System.Collections;

public class PlayerCharge : MonoBehaviour
{
	private bool isCurrentlyCharging;
	private float chargeTimeStart, throwTimeStart;

	public float ballSpeedChargingIncrement, ballSpeedThrowingIncrement;

	public float timeToCharge, timeChargedThrowLasts;
	public float paddleChargingSpeedModifier;
	private const float paddleNoChargeModifier = 1;

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
		bool firstFrameCharging = chargeTimeStart == 0.0f;
		bool firstFrameThrowing = throwTimeStart == 0.0f;
		bool chargeIsFinished = ((Time.time - chargeTimeStart) >= timeToCharge) && !firstFrameCharging;
		bool throwIsFinished = ((Time.time - throwTimeStart) >= timeChargedThrowLasts) && !firstFrameThrowing;
		
		if (isCurrentlyCharging) {
			if (firstFrameCharging) {
				chargeTimeStart = currentTime;
			}
		} else { //is not charging
			if (chargeIsFinished) { //charging is complete
				if (firstFrameThrowing) {
					throwTimeStart = currentTime;
				} else if (throwIsFinished) { //thrown has ended
					resetTimers ();
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

	public float getPaddleResults ()
	{
		if (isCurrentlyCharging) {
			return paddleChargingSpeedModifier;
		}
		return paddleNoChargeModifier;
	}
}
