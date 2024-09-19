using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{

    public Transform spawnPoint;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.tag == "Player"){
            collision.gameObject.transform.position= spawnPoint.position;
        }
    }
}
