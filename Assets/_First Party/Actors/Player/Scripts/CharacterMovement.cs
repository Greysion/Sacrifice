
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharacterMovement.cs
   Function:		Responsibility over all movement mechanics within the player controller.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
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

	private Coroutine forcedAirborn;

	// ---------------------------------------------------------------------------- */

	[SerializeField] private float maxSpeed;

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
		col = GetComponent<CapsuleCollider>();

		visuals = GetComponent<CharacterVisuals>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Movement
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/// <summary>
	/// Should always be called first when assigning player movement commands, even if the value is null.
	/// </summary>
	/// <param name="direction">The direction (left / right) that the player is moving.</param>
	/// <param name="acceleration">The speed at which the player is going to move.</param>
	public void Direction(float direction = 0f, float acceleration = 1f) {

		if (!Grounded())
			return;

		visuals.Jumping(false);
		moveDirection = new Vector3(direction * acceleration, 0f, moveDirection.z);		

	}

	// ---------------------------------------------------------------------------- */

	/// <summary>
	/// Calls for the player to jump into the air based off their current location.
	/// </summary>
	/// <param name="force">The speed at which the player is launched.</param>
	public void DoubleJump(float force = 0f) {

		// Check if we are grounded and if we have double jumped yet.
		if (!Grounded())
			if (hasDoubled)
				return;

			else
				hasDoubled = true;
		else
			hasDoubled = false;

		ResolveJump(force);

	}

	/// <summary>
	/// Calls for the player to jump into the air based off their current location.
	/// </summary>
	/// <param name="force">The speed at which the player is launched.</param>
	public void Jump(float force = 0f) {

		// Check if we are grounded and if we have double jumped yet.
		if (Grounded())
			ResolveJump(force);
	
	}

	// Whatever jump we've chosen, we're still resolving it in the same way after we've checked eligability.
	private void ResolveJump(float force = 0f) {

		// Inform the controller that we have started jumping, reset our velocity, and apply our jump force.
		isJumping = true;
		rb.velocity += Vector3.up * force;
		
		// Force our animator into a jumping state.
		visuals.Jumping(true);
		forcedAirborn = StartCoroutine(ForceAirborn());

	}

	/// <summary>
	/// Communication that the player is no longer pressing jump.
	/// </summary>
	/// <param name="falling">Whether we are still jumping.</param>
	public void JumpEnd() {

		isJumping = false;

	}

	// ---------------------------------------------------------------------------- */

	// Make sure we haven't -just- pressed jump before checking if we're grounded or not.
	private bool Grounded() => forcedAirborn == null ? CheckGround() : false;

	// We're using our own Capsule Collider to check the bounds of our collision detection.
	private bool CheckGround() => Physics.CheckCapsule(
		transform.position + col.center + new Vector3(0f, (col.height / 2f), 0f),
		transform.position + col.center - new Vector3(0f, (col.height / 2f), 0f),
		col.radius,
		ground,
		QueryTriggerInteraction.Ignore);

	// "Invulnerability period" that stops us from triggering "Grounded" for a period after we've jumped.
	private IEnumerator ForceAirborn() {

		yield return new WaitForSeconds(0.25f);
		forcedAirborn = null;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Apply new Vector3 to velocity.
	private void FixedUpdate() {

		ApplyToAnimations();

		if (rb.velocity.magnitude < maxSpeed)
			rb.velocity += moveDirection;

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

	void ApplyToAnimations() {

		visuals.ShowMovement(Dragontale.MathFable.Remap(rb.velocity.x, -maxSpeed, maxSpeed, -1, 1));

	}

	// ---------------------------------------------------------------------------- */

}
