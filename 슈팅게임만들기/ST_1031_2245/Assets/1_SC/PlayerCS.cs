using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCS : MonoBehaviour
{
    public float speed;
    public float power;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public GameObject BulletObjA;
    public GameObject BulletObjB;

    public float maxShotDelay;
    public float curShotDelay;

    void Start()
    {
        speed = 10;
        power = 1;
        maxShotDelay = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Fire();
        ReLoadyBullet();
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
                GameObject bullet = Instantiate(BulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 2:
                GameObject bulletR = Instantiate(BulletObjA, transform.position + Vector3.right * 0.2F, transform.rotation);
                GameObject bulletL = Instantiate(BulletObjA, transform.position + Vector3.left  * 0.2F, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 3:
                GameObject bulletRR = Instantiate(BulletObjA, transform.position + Vector3.right * 0.2F, transform.rotation);
                GameObject bulletCC = Instantiate(BulletObjA, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(BulletObjA, transform.position + Vector3.left * 0.2F, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

        }
        curShotDelay = 0;
    }

    void ReLoadyBullet()
    {
        curShotDelay += Time.deltaTime;
    }



    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "borderPlayer")
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;

                case "Bottom":
                    isTouchBottom = true;
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
        if (collision.gameObject.tag == "borderPlayer")
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;

                case "Bottom":
                    isTouchBottom = false;
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
