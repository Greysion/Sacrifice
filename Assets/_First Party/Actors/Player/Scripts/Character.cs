
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Character.cs
   Function:		Controls input handling, player statistics, and calling of other functions related to player condition such as shooting.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Declarations
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private CharacterMovement movement;
	private CharacterActions actions;
	private CharacterVisuals visuals;

	// ---------------------------------------------------------------------------- */

	private struct PlayerInput {

		public Dictionary<string, float> axis;
		public Dictionary<string, bool> keys;

		public void Declaration() {

			axis = new Dictionary<string, float>();
			keys = new Dictionary<string, bool>();

			axis.Add("Movement", 0f);

			keys.Add("Jump", false);
			keys.Add("Fall", false);
			keys.Add("Shoot", false);

		}

	}

	private PlayerInput input;

	// ---------------------------------------------------------------------------- */

	[SerializeField] private float health;
	[SerializeField] private float acceleration;
	[SerializeField] private float jumpPower;

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

		movement = GetComponent<CharacterMovement>();
		actions = GetComponent<CharacterActions>();
		visuals = GetComponent<CharacterVisuals>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Hotkeys
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void CheckHotkeys() {

		input = new PlayerInput();
		input.Declaration();

		if (Input.GetKey(KeyCode.D))
			input.axis["Movement"] += 1f;

		if (Input.GetKey(KeyCode.A))
			input.axis["Movement"] -= 1f;

		if (Input.GetKeyDown(KeyCode.Space))
			input.keys["Jump"] = true;

		if (Input.GetKeyUp(KeyCode.Space))
			input.keys["Fall"] = true;

	}

	private void PassHotkeys() {

		movement.Direction(input.axis["Movement"], acceleration);

		if (input.keys["Jump"] == true)
			movement.Jump(jumpPower);

		if (input.keys["Fall"] == true)
			movement.JumpEnd();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Update() {

		CheckHotkeys();
		PassHotkeys();

	}

	// ---------------------------------------------------------------------------- */

}