using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Runner : Enemy {

    public Transform playerPos;
    public NavMeshAgent agent;
    Animator enemyAnim;



    private void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
    }

    void Update () {
       playerPos =  GameObject.FindObjectOfType<Character>().GetComponent<Transform>();
        
        agent.SetDestination(playerPos.position);
        enemyAnim.SetFloat("MovementSpeedEnemy", agent.speed);
        enemyAnim.SetFloat("Health", health);
	}



    public void TakeDamage(float damage)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = GameObject.FindObjectOfType<Character>();
        if (collision.gameObject == player)
        {
            player.DamagePlayer(damage);
        }
       
    }
}
