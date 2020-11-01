using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCS : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletBordet")
            Destroy(gameObject);
    }
}
