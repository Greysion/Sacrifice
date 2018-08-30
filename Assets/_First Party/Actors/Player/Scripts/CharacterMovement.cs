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
	private CapsuleCollider col;

	private CharacterVisuals visuals;

	// ---------------------------------------------------------------------------- */

	[SerializeField] private LayerMask ground;

	// ---------------------------------------------------------------------------- */

	private Vector3 moveDirection;
	private bool isJumping;
	private bool hasDoubled;

	[SerializeField] private float gravityFalling;
	[SerializeField] private float gravityJumping;

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
		col = GetComponent<Collider>() as CapsuleCollider;

		visuals = GetComponent<CharacterVisuals>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Movement
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/// <summary>
	/// Should always be called first when assigning player movement commands, even if the value is null.
	/// </summary>
	/// <param name="direction">The direction (left / right) that the player is moving.</param>
	/// <param name="speed">The speed at which the player is going to move.</param>
	public void Direction(float direction = 0f, float speed = 1f) {

		moveDirection = new Vector3(direction * speed, 0f, moveDirection.z);

	}

	// ---------------------------------------------------------------------------- */

	/// <summary>
	/// Calls for the player to jump into the air based off their current location.
	/// </summary>
	/// <param name="force">The speed at which the player is launched.</param>
	public void Jump(float force = 0f) {

		// Check if we are grounded and if we have double jumped yet.
		if (!Grounded())
			if (hasDoubled)
				return;

			else
				hasDoubled = true;
		else
			hasDoubled = false;

		// Inform the controller that we have started jumping, reset our velocity, and apply our jump force.
		isJumping = true;
		rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y);
		rb.AddForce(Vector3.up * force, ForceMode.Impulse);

	}

	/// <summary>
	/// Communication that the player is no longer pressing jump.
	/// </summary>
	/// <param name="falling">Whether we are still jumping.</param>
	public void JumpEnd() {

		isJumping = false;

	}

	// ---------------------------------------------------------------------------- */

	// We're using our own Capsule Collider to check the bounds of our collision detection.
	private bool Grounded() => Physics.CheckCapsule(
		transform.position + col.center, 
		transform.position + col.center + new Vector3(0f,-(col.height/2f),0f), 
		col.radius, 
		ground, 
		QueryTriggerInteraction.Ignore);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Apply new Vector3 to velocity.
	private void FixedUpdate() {

		rb.AddForce(moveDirection, ForceMode.VelocityChange);
		ApplyGravityModifier();

	}

	// ---------------------------------------------------------------------------- */

	private void ApplyGravityModifier() {

		// If we're ascending and we have stopped jumping.
		if (rb.velocity.y > 1f && !isJumping)
			rb.velocity += Vector3.up * Physics.gravity.y * (gravityFalling * 3) * Time.deltaTime;

		// If we're decending.
		else if (rb.velocity.y < 1f)
			rb.velocity += Vector3.up * Physics.gravity.y * (gravityFalling - 1) * Time.deltaTime;

		// If we're currently jumping, standing, or any other scenario.
		else
			rb.velocity += Vector3.up * Physics.gravity.y * (gravityJumping - 1) * Time.deltaTime;

	}

	// ---------------------------------------------------------------------------- */

}
