using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEditor.UIElements;
using UnityEngine;

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

        GameObject bullet =  Instantiate(buttetObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        curShotDelad = 0;

    }

    void ReLoad()
    {
        curShotDelad += Time.deltaTime;
        curShieldTime += Time.deltaTime;
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
        else if ((collision.gameObject.tag == "BulletEnemy"  || collision.gameObject.tag == "Enemy") && isShield == false)
        {

            isDead = true;          
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            //spriteRenderer.sprite = sprites[0];

            life--;
            manager.updateLifeIncon(life);
            if(life <= 0)
            {
                manager.gameOver();
            }
            else
            {
                Invoke("RePlay", 2.0f);
                //RePlay();
            }

        }
        else if((collision.gameObject.tag == "BulletEnemy" || collision.gameObject.tag == "Enemy") && isShield == true)
        {
            Destroy(collision.gameObject);
        }
    }

    void RePlay()  // 플레이어 다시 나타남.
    {
        transform.position = new Vector3(0, -4, 0);
        gameObject.SetActive(true);
        isDead = false;
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
        if(isShield == true && curShieldTime >= maxShieldTime)
        {
            isShield = false;
            ShieldOnOff();

        }

        if (Input.GetKeyDown(KeyCode.F1))
        {

            {
                if (isShield == false)
                {
                    isShield = true;
                }
                else if (isShield == true)
                {
                    isShield = false;
                    curShieldTime += Time.deltaTime;
                }
                ShieldOnOff();
            }
        }
    }

    void ShieldOnOff()
    {
        if (isShield == true && shieldObj.gameObject.activeSelf == false)
        {
            Debug.Log("쉴드 완료");
            shieldObj.gameObject.SetActive(true);
            curShieldTime = 0;

        }
        else if (isShield == false && shieldObj.gameObject.activeSelf == true)
        {
            Debug.Log("쉴드 해제");
            shieldObj.gameObject.SetActive(false);
        }

    }

}

