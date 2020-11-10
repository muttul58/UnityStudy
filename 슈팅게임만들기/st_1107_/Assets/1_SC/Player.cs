using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    public float speed;
    public float power;
    public bool isDead;


    public int life;

    public float maxShieldTime;
    public float curShieldTime;


    public bool isTuochTop;
    public bool isTuochBottom;
    public bool isTuochRight;
    public bool isTuochLeft;

    public float maxShotDelad;
    public float curShotDelad;

    public GameObject buttetObjA;
    public GameObject buttetObjB;
    public GameObject buttetObjC;
    public GameObject shieldObj;
    

    public GameManager manager;

    public Sprite[] sprites;


    // 기능키
    public static bool isShield;

    void Start()
    {
        speed = 10;
        power = 1;
        isDead = false;
        isShield = false;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Fier();
        ReLoad();
        HotKey();
    }

    void Fier()
    {
        if (!Input.GetKey("space"))
            return;
        if (curShotDelad < maxShotDelad)
            return;

        if (power == 1)
        {
            GameObject bulletA =  Instantiate(buttetObjA, transform.position, transform.rotation);
            Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
            rigidA.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
        else if (power == 2)
        {
            GameObject bulletB = Instantiate(buttetObjB, transform.position, transform.rotation);
            Rigidbody2D rigidB = bulletB.GetComponent<Rigidbody2D>();
            rigidB.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }
        else if (power == 3)
        {
            GameObject bulletR = Instantiate(buttetObjA, transform.position+Vector3.right*0.3f, transform.rotation);
            GameObject bulletC = Instantiate(buttetObjB, transform.position, transform.rotation);
            GameObject bulletL = Instantiate(buttetObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            rigidR.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            rigidC.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            rigidL.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }


        curShotDelad = 0;

    }

    void ReLoad()
    {
        curShotDelad += Time.deltaTime;
    }

    void PlayerMove()
    {

        float h = Input.GetAxisRaw("Horizontal");
        if( (isTuochRight && h==1) || (isTuochLeft && h==-1) )
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTuochTop && v == 1) || (isTuochBottom && v == -1))
            v = 0;

        Vector3 cutPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = cutPos + nextPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderPlayer")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTuochTop = true;
                    break;

                case "Bottom":
                    isTuochBottom = true;
                    break;

                case "Right":
                    isTuochRight = true;
                    break;

                case "Left":
                    isTuochLeft = true;
                    break;
            }
        }
        else if ((collision.gameObject.tag == "BulletEnemy"  || collision.gameObject.tag == "Enemy") && isShield == false)
        {

            isDead = true;          
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            //spriteRenderer.sprite = sprites[0];

            life--;
            Debug.Log(" 생명 -1");
            manager.updateLifeIncon(life);
            Debug.Log(" 생명 아이콘 처리");
            if (life <= 0)
            {
                Debug.Log(" 게임끝");
                manager.gameOver();
            }
            else
            {
                Invoke("RePlay", 2.0f);
                Debug.Log(" 다시시작");
                //RePlay();
            }
        }
        else if((collision.gameObject.tag == "BulletEnemy" || collision.gameObject.tag == "Enemy") && isShield == true)
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Item")
        {
            Debug.Log("아이템 먹음 : " + collision.gameObject.name);
            switch (collision.gameObject.name)
            {
                case "ItemLife(Clone)":
                    Debug.Log("BBBBBBBBBB !!");
                    if (life < 3)
                    {
                        life++;
                        manager.updateLifeIncon(life);
                    }
                    else
                        GameManager.ScoreUp(500);
                    Destroy(collision.gameObject);
                    break;

                case "ItemShield(Clone)":
                    Debug.Log("ssssssssss!!");
                    ShieldOn();
                    Destroy(collision.gameObject);
                    break;

                case "ItemPower(Clone)":
                    Debug.Log("PPPPPP !!");
                    if (power < 3)
                        power++;
                    else
                        GameManager.ScoreUp(500);
                    Destroy(collision.gameObject);

                    break;
            }
        }
    }
    
    void RePlay()  // 플레이어 다시 나타남.
    {
        Debug.Log("RePlay 함수 시작");
        transform.position = new Vector3(0, -4, 0);
        gameObject.SetActive(true);
        isDead = false;
        Debug.Log("RePlay 함수 끝");

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderPlayer")
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTuochTop = false;
                    break;

                case "Bottom":
                    isTuochBottom = false;
                    break;

                case "Right":
                    isTuochRight = false;
                    break;

                case "Left":
                    isTuochLeft = false;
                    break;
            }

    }

    void HotKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            power = 1;
        else if (Input.GetKeyDown(KeyCode.F2))
            power = 2;
        else if (Input.GetKeyDown(KeyCode.F3))
            power = 3;
        else if (Input.GetKeyDown(KeyCode.F4))  // 쉴드 On 
            ShieldOn();
        else if(Input.GetKeyDown(KeyCode.F5))
            if(life<3) manager.updateLifeIncon(life++);
    }

    void ShieldOn()
    {
        if (isShield == true)
            return;

        isShield = true;
        shieldObj.gameObject.SetActive(true);
        Invoke("ShieldOff", 5.0f);
    }

    void ShieldOff()
    {
        isShield = false;
        shieldObj.gameObject.SetActive(false);
    }
}

