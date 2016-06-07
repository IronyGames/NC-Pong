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

	private void evauluateInput ()
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
		evauluateInput ();
		updateModules ();
	}

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D> ().velocity = movementModule.getResults ();
	}

	public bool isCharging ()
	{
		return chargeModule.isCharging ();
	}

	public bool isThrowingCharge ()
	{
		return chargeModule.isThrowingCharge ();
	}
}
