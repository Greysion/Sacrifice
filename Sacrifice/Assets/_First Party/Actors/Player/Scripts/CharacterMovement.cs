﻿
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharacterMovement.cs
   Function:		Responsibility over all movement mechanics within the player controller.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Declaration
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Rigidbody rb;
	private CharacterVisuals visuals;

	// ---------------------------------------------------------------------------- */

	Vector3 moveDirection;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
	Initialisation
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Awake() {

		ComponentGrab();

	}

	private void Start() {



	}

	// ---------------------------------------------------------------------------- */

	private void ComponentGrab() {

		rb = GetComponent<Rigidbody>();
		visuals = GetComponent<CharacterVisuals>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Movement
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public void Direction(float direction = 0f, float speed = 1f) {

		moveDirection = new Vector3(direction * speed, moveDirection.y, moveDirection.z);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Apply new Vector3 to velocity.
	private void FixedUpdate() {

		rb.velocity += moveDirection.normalized;

	}

	// ---------------------------------------------------------------------------- */

}
