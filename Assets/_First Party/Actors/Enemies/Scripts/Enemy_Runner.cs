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
        enemyAnim.SetFloat("MovementSpeedEnemy", agent.speed);
    }

    void Update () {
       playerPos =  GameObject.FindObjectOfType<Character>().GetComponent<Transform>();
        
        agent.SetDestination(playerPos.position);

       
        
        enemyAnim.SetFloat("Health", health);

        if(health <= 0)
        {
            enemyAnim.SetFloat("MovementSpeedEnemy", 0f);
            enemyAnim.Play("Death");
        }
	}



    public void TakeDamage(float damage)
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        var player = GameObject.FindObjectOfType<Character>();
        if (collision.gameObject == player)
        {
            enemyAnim.SetFloat("MovementSpeedEnemy", 0f);
            enemyAnim.SetBool("IsInAttackRange", true);
            player.DamagePlayer(damage);
        }
        else
        {
            enemyAnim.SetBool("IsInAttackRange", false);
            enemyAnim.SetFloat("MovementSpeedEnemy", agent.speed);
        }
       
    }
}
