using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public string inputAxis;
	public string inputChargeButton;

	private PlayerMovement movementModule;
	private PlayerCharge chargeModule;

	private Vector2 originalPosition;

	void Start ()
	{
		originalPosition = transform.position;
		movementModule = GetComponent<PlayerMovement> ();
		chargeModule = GetComponent<PlayerCharge> ();
	}

	private void evaluateInput ()
	{
		float axis = Input.GetAxisRaw (inputAxis);
		float charge = Input.GetAxisRaw (inputChargeButton);

		movementModule.setInput (axis);
		chargeModule.setInput (charge);
	}

	private void updateModules ()
	{
		movementModule.asyncUpdate ();
		chargeModule.asyncUpdate ();
	}

	void Update ()
	{
		evaluateInput ();
		updateModules ();
	}

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D> ().velocity = movementModule.getResults () * chargeModule.getPaddleResults ();
	}

	private void resetModules ()
	{
		GetComponent<PlayerCharge> ().reset ();
		GetComponent<PlayerMovement> ().reset ();
	}

	public void resetPaddle ()
	{
		
		GetComponent<Rigidbody2D> ().position = originalPosition;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		resetModules ();
	}

	public bool isCharging ()
	{
		return chargeModule.isCharging ();
	}

	public bool isThrowingCharge ()
	{
		return chargeModule.isThrowingCharge ();
	}

	public float getChargingModifier ()
	{
		return chargeModule.ballSpeedChargingIncrement;
	}

	public float getThrowingModifier ()
	{
		return chargeModule.ballSpeedThrowingIncrement;
	}
}
