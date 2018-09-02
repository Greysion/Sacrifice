using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Runner : Enemy {

    public Transform playerPos;
    public NavMeshAgent agent;

	void Update () {
       playerPos =  GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.LookAt(playerPos);
        agent.SetDestination(playerPos.position);
	}



    public void TakeDamage(float damage)
    {

    }
}
