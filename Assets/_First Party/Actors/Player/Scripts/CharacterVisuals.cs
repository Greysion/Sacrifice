
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharacterVisuals.cs
   Function:		Responsibility over any character visual elements, such as animator controlling, visual effects, etc.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class CharacterVisuals : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Declaration
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Animator an;

	[Space]
	[SerializeField] private Material[] characterVariations;

	[Space]
	[SerializeField] private Transform grip;
	[SerializeField] private GameObject[] weapons;

	private Transform firePoint;

	[Space]
	[SerializeField] private Transform[] bonesThatLook;

	[Space]
	[SerializeField] private PostProcessVolume postProcessing;

	// ---------------------------------------------------------------------------- //

	private bool aimingLeft;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Awake() {

		ComponentGrab();
		CreateWeapon();

	}

	private void Start() {

		ApplyVariance();

	}

	// ---------------------------------------------------------------------------- */

	private void ComponentGrab() {

		an = GetComponentInChildren<Animator>();

	}

	private void CreateWeapon() {

		int chosenWeapon = Random.Range(0, weapons.Length - 1);
		GameObject weapon = Instantiate(weapons[chosenWeapon],grip);

		weapon.transform.rotation = weapons[chosenWeapon].transform.rotation;
		firePoint = weapon.transform.Find("FirePoint");

	}

	private void ApplyVariance() {

		GetComponentInChildren<Transform>().GetComponentInChildren<SkinnedMeshRenderer>().material = characterVariations[Random.Range(0,characterVariations.Length-1)];

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Visual Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/// <summary>
	/// Update the animator velocity to our current movement speed.
	/// </summary>
	/// <param name="move">The player's current movement speed. Please equalise this value between -1 and 1 for Left to Right.</param>
	public void ShowMovement(float move) {

		if (aimingLeft)
			move *= -1;

		an.SetFloat("movementSpeed", move);

	}

	/// <summary>
	/// Update the animator as to where we are currently in our player jump.
	/// </summary>
	/// <param name="isJumping">Jumpstatus equates to: 0 is Grounded. 1 is Button Pressed. 2 is Falling.</param>
	public void Jumping(bool isJumping) {

		an.SetBool("isJumping", isJumping);

	}

	/// <summary>
	/// Update the animator as to where we should be aiming in world space.
	/// </summary>
	/// <param name="target">The target location in world space.</param>
	public void Aim(Vector3 target) {

		TurnToCursor(target);
		//LookAtCursor(target);		

	}

	private void TurnToCursor(Vector3 target) {

		aimingLeft = target.x < 0;

		if (aimingLeft)
			transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
		else
			transform.rotation = Quaternion.identity;

	}

	private void LookAtCursor(Vector3 target) {

		foreach (Transform bone in bonesThatLook)
			bone.LookAt(bone.position + target, Vector3.up);

	}

	// ---------------------------------------------------------------------------- */
	
	/// <summary>
	/// Updates the Post Processing effects that apply on the player's health value.
	/// </summary>
	/// <param name="healthRemap">The health value of the player converted into a 01 percentage.</param>
	public void PostProcessingHealth(float healthRemap) {

		if (postProcessing != null)
			postProcessing.weight = Mathf.Clamp01(healthRemap);
		else
			Debug.LogError("Please assign the PlayerHealth post processing in _CONTROLLERS to the PlayerVisuals script.");

	}

	// ---------------------------------------------------------------------------- */


}