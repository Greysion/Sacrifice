using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Runner : Enemy {

    public Transform playerPos;

	void Update () {
        transform.LookAt(playerPos);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
	}
}
