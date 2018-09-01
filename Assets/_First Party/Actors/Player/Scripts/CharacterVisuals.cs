
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharacterVisuals.cs
   Function:		Responsibility over any character visual elements, such as animator controlling, visual effects, etc.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class CharacterVisuals : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Declaration
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Animator an;

	// ---------------------------------------------------------------------------- //
	


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

		an = GetComponentInChildren<Animator>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Visual Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/// <summary>
	/// Update the animator velocity to our current movement speed.
	/// </summary>
	/// <param name="move">The player's current movement speed. Please equalise this value between -1 and 1 for Left to Right.</param>
	public void ShowMovement(float move) {

		an.SetFloat("movementSpeed", move);

	}

	/// <summary>
	/// Update the animator as to where we are currently in our player jump.
	/// </summary>
	/// <param name="isJumping">Jumpstatus equates to: 0 is Grounded. 1 is Button Pressed. 2 is Falling.</param>
	public void Jumping(bool isJumping) {

		an.SetBool("isJumping", isJumping);

	}

	// ---------------------------------------------------------------------------- */





}