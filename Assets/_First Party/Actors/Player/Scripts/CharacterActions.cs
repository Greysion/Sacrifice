
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharacterActions.cs
   Function:		Responsibility over any user-actions the player can perform, such as shooting, collecting items, etc.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using UnityEngine;

public class CharacterActions : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Declarations
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private CharacterVisuals visuals;

	// ---------------------------------------------------------------------------- */

	private Coroutine firing;

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

		visuals = GetComponent<CharacterVisuals>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void LateUpdate() {

		AimAtCursor();

	}

	// ---------------------------------------------------------------------------- */

	private Vector3 CursorLocation() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

	private void AimAtCursor() {

		Vector3 aimingLocation = CursorLocation() - transform.position;
		aimingLocation = new Vector3(aimingLocation.x, aimingLocation.y, transform.position.z);

		visuals.Aim(aimingLocation);

	}

	public void Shoot(float weaponDamage, float weaponCooldown) {

		if (firing == null)
			firing = StartCoroutine(FireRateCooldown(weaponCooldown));
		else
			return;

		Debug.Log("I fired my weapon.");

	}

	private IEnumerator FireRateCooldown(float fireRate) {

		yield return new WaitForSeconds(fireRate);
		firing = null;

	}

	// ---------------------------------------------------------------------------- */

}
