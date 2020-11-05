using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float power;

    
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public bool isThouchTop;
    public bool isThouchBottom;
    public bool isThouchRight;
    public bool isThouchLeft;

    public float maxShotDelay;
    public float curShotDelay;




    void Update()
    {
        PlayerMove();
        Fire();
        ReloadBullet();
    }


    void Fire()
    {
        if (!Input.GetKey("space"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curShotDelay = 0;

    }

    void ReloadBullet()
    {
        curShotDelay += Time.deltaTime;
    }

    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isThouchRight && h == 1) || (isThouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isThouchTop && v == 1) || (isThouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderPlayer")
            switch (collision.gameObject.name)
            {
                case "Top":
                    isThouchTop = true;
                    break;
                case "Bottom":
                    isThouchBottom = true;
                    break;
                case "Right":
                    isThouchRight = true;
                    break;
                case "Left":
                    isThouchLeft = true;
                    break;
            }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderPlayer")
            switch (collision.gameObject.name)
            {
                case "Top":
                    isThouchTop = false;
                    break;
                case "Bottom":
                    isThouchBottom = false;
                    break;
                case "Right":
                    isThouchRight = false;
                    break;
                case "Left":
                    isThouchLeft = false;
                    break;
            }
    }
}
