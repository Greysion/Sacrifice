using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Runner : Enemies {

    public Transform playerPos;

	void Update () {
        transform.LookAt(playerPos);
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
	}
}
