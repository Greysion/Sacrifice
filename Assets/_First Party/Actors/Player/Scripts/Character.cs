
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
	[SerializeField] private float weaponDamage;
	[SerializeField] private float weaponCooldown;

	[SerializeField] private float acceleration;
	[SerializeField] private float jumpPower;

	private float maxHealth;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Awake() {

		ComponentGrab();		

	}

	private void Start() {

		Declarations();

	}

	// ---------------------------------------------------------------------------- */

	private void ComponentGrab() {

		movement = GetComponent<CharacterMovement>();
		actions = GetComponent<CharacterActions>();
		visuals = GetComponent<CharacterVisuals>();

	}

	private void Declarations() {

		maxHealth = health;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Hotkeys
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Initialise a new struct for holding our player Hotkeys.
	private void CheckHotkeys() {

		input = new PlayerInput();
		input.Declaration();

		Axis();
		Keys();		

	}

	// Check keys that tween between -1f and 1f.
	private void Axis() {

		if (Input.GetKey(KeyCode.D))
			input.axis["Movement"] += 1f;

		if (Input.GetKey(KeyCode.A))
			input.axis["Movement"] -= 1f;

	}

	// Check boolean input keys that are either true or false.
	private void Keys() {

		if (Input.GetKeyDown(KeyCode.Space))
			input.keys["Jump"] = true;

		if (Input.GetKeyUp(KeyCode.Space))
			input.keys["Fall"] = true;

		if (Input.GetMouseButtonDown(0))
			input.keys["Shoot"] = true;

	}

	// Pass all relevant inputs to their required components.
	private void PassHotkeys() {

		movement.Direction(input.axis["Movement"], acceleration);

		if (input.keys["Jump"] == true)
			movement.Jump(jumpPower);

		if (input.keys["Fall"] == true)
			movement.JumpEnd();

		if (input.keys["Shoot"] == true)
			actions.Shoot(weaponDamage, weaponCooldown);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Hotkeys
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void DamagePlayer(float damage) {

		Debug.Log($"The player has taken {damage} Damage.");
		health -= damage;

		visuals.PostProcessingHealth(Dragontale.MathFable.Remap(health, 0f, maxHealth, 1f, 0f));

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