using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public string inputAxis;
	public string inputChargeButton;

	public PlayerMovement movementModule;
	public PlayerCharge chargeModule;

	void Start ()
	{
	}

	private void evauluateInput ()
	{
		float axis = Input.GetAxisRaw (inputAxis);
		float charge = Input.GetAxisRaw (inputChargeButton);

		movementModule.setInput (axis);
		chargeModule.setInput (charge);
	}

	private void calculateKinematics ()
	{
		movementModule.asyncUpdate ();
	}

	void Update ()
	{
		evauluateInput ();
		calculateKinematics ();
	}

	void FixedUpdate ()
	{
		GetComponent<Rigidbody2D> ().velocity = movementModule.getResults ();
	}

}
