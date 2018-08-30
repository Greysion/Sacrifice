using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject enemy;
    public Transform[] spawnPoints;
    public GameObject[] stagePoints;
    public GameObject startRoom;
    public Transform initialSpawn;

	
	void Start () {

        SpawnStage();

	}
	
	
	/*public void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
      
    }
    */
    public void SpawnStage()
    {
        
        float stageLength = stagePoints[0].GetComponent<BoxCollider>().size.x;
        Debug.Log(stageLength);
        int room = Random.Range(3, 6);

        for (int i = 0; i < room; i++)
        {

            GameObject gO = Instantiate(stagePoints[Random.Range(0, stagePoints.Length)], initialSpawn.position + new Vector3(i * stageLength,0f,0f), Quaternion.identity, initialSpawn);
            
        }
       
     

    }
}
