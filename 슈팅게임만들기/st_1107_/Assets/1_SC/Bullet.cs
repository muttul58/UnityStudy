using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    public GameObject player;
    public bool playerDead;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        
    }
    void Update()
    {
        /*
        if (player.activeSelf == false)
        {
           Destroy(gameObject);
        }
        */

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderDestroy")
            Destroy(gameObject);
    }
}
