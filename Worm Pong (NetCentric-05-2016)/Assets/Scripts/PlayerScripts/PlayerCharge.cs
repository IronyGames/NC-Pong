using UnityEngine;
using System.Collections;

public class PlayerCharge : MonoBehaviour
{
	private bool isCharging;

	void Start ()
	{
		isCharging = false;
	}

	public void setInput (float button)
	{
		if (button == 0) {
			isCharging = false;
		} else {
			isCharging = true;
		}
	}

}
