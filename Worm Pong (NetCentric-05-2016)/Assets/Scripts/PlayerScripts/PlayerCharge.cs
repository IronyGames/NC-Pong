using UnityEngine;
using System.Collections;

public class PlayerCharge : MonoBehaviour
{
	private bool isCurrentlyCharging;
	private float chargeTimeStart, throwTimeStart;

	public float ballSpeedChargingIncrement, ballSpeedThrowingIncrement;

	public float timeToCharge, timeChargedThrowLasts;
	public float paddleChargingSpeedModifier;
	private const float paddleNotChargingSpeedModifier = 1;

	public Color colorWhenCharged;

	private GravityParticleManager chargingParticles;

	private Color colorUncharged;

	void Start ()
	{
		colorUncharged = new Color (1, 1, 1);
		chargingParticles = GetComponent<GravityParticleManager> ();
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
		float timeCharging = (Time.time - chargeTimeStart);
		float timeThrowing = (Time.time - throwTimeStart);
		bool chargeIsFinished = (timeCharging >= timeToCharge) && !firstFrameCharging;
		bool throwIsFinished = (timeThrowing >= timeChargedThrowLasts) && !firstFrameThrowing;

		if (chargeIsFinished) {
			GetComponent<SpriteRenderer> ().color = colorWhenCharged;
		} else {
			GetComponent<SpriteRenderer> ().color = colorUncharged;
		}

		if (isCurrentlyCharging) {
			if (firstFrameCharging) {
				chargeTimeStart = currentTime;
			}

			chargingParticles.generateIndefinitely ();

			/*
			float currentColorChange = timeCharging / timeToCharge;
			if (currentColorChange > 1) {
				currentColorChange = 1;
			}
			*/
		} else { //is not charging

			chargingParticles.stop ();

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
		return paddleNotChargingSpeedModifier;
	}
}
