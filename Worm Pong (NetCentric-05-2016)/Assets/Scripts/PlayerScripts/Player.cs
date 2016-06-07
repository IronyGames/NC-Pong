using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public string inputAxis;
	public string inputChargeButton;

	private PlayerMovement movementModule;
	private PlayerCharge chargeModule;

	void Start ()
	{
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
