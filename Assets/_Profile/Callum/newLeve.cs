using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newLeve : MonoBehaviour {

     GameController gameController;

    public void OnTriggerEnter(Collider other)
    {
        var player = GameObject.FindObjectOfType<Character>();
        if(other.gameObject == player)
        {
            gameController.SpawnStage();
        }
    }
}
