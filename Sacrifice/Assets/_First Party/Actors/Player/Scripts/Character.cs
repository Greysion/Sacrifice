
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Character.cs
   Function:		Controls input handling, player statistics, and calling of other functions related to player condition such as shooting.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

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

		public float[] axis;
		public bool[] keys;

		public PlayerInput(int numOfAxis = 0, int numOfKeys = 0) {

			axis = new float[numOfAxis];
			keys = new bool[numOfKeys];

		}

	}

	private PlayerInput input;

	// ---------------------------------------------------------------------------- */

	[SerializeField] private float speed;
	[SerializeField] private float health;

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

		input = new PlayerInput(1, 1);

		if (Input.GetKey(KeyCode.D))
			input.axis[0] += 1f;

		if (Input.GetKey(KeyCode.A))
			input.axis[0] -= 1f;

		if (Input.GetKey(KeyCode.Space))
			input.keys[0] = true;

	}

	private void PassHotkeys() {

		movement.Direction(input.axis[0], speed);

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