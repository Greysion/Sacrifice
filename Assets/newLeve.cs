using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newLeve : MonoBehaviour {

     GameController gameController;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameController.SpawnStage();
        }
    }
}
