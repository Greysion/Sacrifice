using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject enemy;
    public Transform[] spawnPoints;
	
	void Start () {
        InvokeRepeating("SpawnEnemy", 1f, 5f);
	}
	
	
	public void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        Debug.Log("Has Spawned");
    }
}
