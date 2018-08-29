﻿
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharacterCamera.cs
   Function:		Controls basic camera functionality.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class CharacterCamera : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Declarations
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Transform focus;

	// ---------------------------------------------------------------------------- */

	[SerializeField] private Vector3 offset;
	[HideInInspector] public Vector3 additionalOffset;

	[SerializeField] private float snap;

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

		focus = FindObjectOfType<Character>().transform;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Positioning
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void UpdatePosition() {

		Vector3 cameraPosition = focus.position + offset + additionalOffset;
		transform.position = Vector3.Lerp(transform.position, cameraPosition, snap);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void FixedUpdate() {

		UpdatePosition();

	}

	// ---------------------------------------------------------------------------- */
	
}