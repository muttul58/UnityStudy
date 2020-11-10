﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float hp;

    public float maxShotDelay;
    public float curShotDelay;


    public Sprite[] sprites;
    public GameObject enemyBulletObjA;
    public GameObject enemyBulletObjB;
    public GameObject player;
    public GameObject PlayerDead;
    public GameObject bulletCode;
    public GameObject PlayerBullet;
    public GameObject[] itemObjs;



    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    public string enemyName;
    public int enemyScore;
    bool isDead = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.down * speed;

        player = GameObject.FindWithTag("Player");

    }   

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy Update() 시작" + player.activeSelf);

        if (player.activeSelf == true)  // 플레이어가 살아 있는 경우만 총알 발사
        {
            Fire();
            Reload();
        }
        else if (player.activeSelf == false)  // 플레이어가 죽은 경우(Active 가 false) 적 소멸
        {
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay  )
            return;

        GameObject bullet = Instantiate(enemyBulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector3 bulletPos = player.transform.position - transform.position;
        rigid.AddForce(bulletPos.normalized * 5, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

     void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "BorderDestroy")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Shield") 
            Destroy(collision.gameObject);

        else if(collision.gameObject.tag == "BulletPlayer")
        {
            Debug.Log("총알 맞음");
            spriteRenderer.sprite = sprites[1];
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            hp -= bullet.dmg;

            Invoke("ReturnSprite", 0.1f);

            if(hp <= 0)
            {
                Destroy(gameObject);
                GameManager.ScoreUp(enemyScore);
                
                int ran = Random.Range(1, 10);
                if (ran < 4) 
                {
                    Debug.Log(" 노 아이템");
                }
                else if (ran < 6)
                {
                    Debug.Log(" 0번 아이템");
                    GameObject itemShield = Instantiate(itemObjs[0], transform.position, transform.rotation);
                    Rigidbody2D rigidShide = itemShield.GetComponent<Rigidbody2D>();
                    //Vector3 itemPos = player.transform.position - transform.position;
                    rigidShide.AddForce(Vector2.down  * 1, ForceMode2D.Impulse);
                }
                else if (ran < 8)
                {
                    GameObject itemPower = Instantiate(itemObjs[1], transform.position, transform.rotation);
                    Rigidbody2D rigidPower = itemPower.GetComponent<Rigidbody2D>();
                   // Vector3 itemPos = player.transform.position - transform.position;
                    rigidPower.AddForce(Vector2.down * 1, ForceMode2D.Impulse);
                }
                else if (ran < 10)
                {
                    GameObject itemLife = Instantiate(itemObjs[2], transform.position, transform.rotation);
                    Rigidbody2D rigidLife = itemLife.GetComponent<Rigidbody2D>();
                    //Vector3 itemPos = player.transform.position - transform.position;
                    rigidLife.AddForce(Vector2.down * 1, ForceMode2D.Impulse);
                }
            }
        }

    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

}
