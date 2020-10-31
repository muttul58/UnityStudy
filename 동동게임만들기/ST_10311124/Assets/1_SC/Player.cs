using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public float power = 1;

    public float maxShotDelay;
    public float curShotDelay;

    public bool isTouchTop;
    public bool isTouchBorder;
    public bool isTouchRight;
    public bool isTouchLeft;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject bulletObjC;



    void Update()
    {
        PlayerMove();   // 플레이어 이동
        Fire();         // 총알 생성 및 발사
        Reload();       // 총알 장전
        PowerSet();     // 총알 변경
    }

    void PowerSet()
    {
        if (Input.GetKey("1")) power = 1;
        if (Input.GetKey("2")) power = 2;
        if (Input.GetKey("3")) power = 3;
        if (Input.GetKey("4")) power = 4;
        if (Input.GetKey("5")) power = 5;
    }

    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBorder && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }


    void Fire()
    {
        if (!Input.GetKey("space"))
            return;

        if (curShotDelay < maxShotDelay)
            return;
        switch(power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10 , ForceMode2D.Impulse);
                break;

            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.15f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.15f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 4:
                GameObject bulletC = Instantiate(bulletObjC, transform.position, transform.rotation);
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                rigidC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 5:
                GameObject bulletRRR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletCCC = Instantiate(bulletObjC, transform.position, transform.rotation);
                GameObject bulletLLL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);
                Rigidbody2D rigidRRR = bulletRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCC = bulletCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLL = bulletLLL.GetComponent<Rigidbody2D>();
                rigidRRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;

                case "Bottom":
                    isTouchBorder = true;
                    break;

                case "Right":
                    isTouchRight = true;
                    break;

                case "Left":
                    isTouchLeft = true;
                    break;
            }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;

                case "Bottom":
                    isTouchBorder = false;
                    break;

                case "Right":
                    isTouchRight = false;
                    break;

                case "Left":
                    isTouchLeft = false;
                    break;
            }
    }

}
