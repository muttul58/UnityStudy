using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCS : MonoBehaviour
{
    public Sprite[] sprits;
    public float speed;
    public float health;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletBordet")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Bullet")
        {
            BulletCS bullet = collision.gameObject.GetComponent<BulletCS>();
            OnHit(bullet.dmg);
            Invoke("RoloadBullet", 0.1f);
            Destroy(collision.gameObject);

        }


    }
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprits[1];

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void RoloadBullet()
    {
        spriteRenderer.sprite = sprits[0];
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
