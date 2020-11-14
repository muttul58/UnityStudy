using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 이동 관련
    public float speed;
    public float power;
    public int life;
    
    public bool isNoDeath;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    // 미사일 관련
    public float maxShotLoad;
    public float curShotLoad;
    public float maxBoomLoad;
    public float curBoomLoad;
    public bool isItem;

    public GameObject bolletObjA;
    public GameObject bolletObjB;

    public GameManager manager;

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
        if (curShotLoad < maxShotLoad)
            return;
        if (power == 1)
        {
            GameObject bullet= Instantiate(bolletObjA, transform.position + Vector3.up * 0.5f, transform.rotation);
            Rigidbody2D rigid = bullet.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        }
        else if (power == 2)
        {
            GameObject bullet_r = Instantiate(bolletObjA, transform.position + Vector3.up * 0.5f + Vector3.right * 0.2f, transform.rotation);
            GameObject bullet_l = Instantiate(bolletObjA, transform.position + Vector3.up * 0.5f + Vector3.left * 0.2f, transform.rotation);
            Rigidbody2D rigid_r = bullet_r.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid_l = bullet_l.gameObject.GetComponent<Rigidbody2D>();
            rigid_r.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            rigid_l.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        }
        else if (power == 3)
        {
            GameObject bullet_rr = Instantiate(bolletObjA, transform.position + Vector3.up * 0.5f + Vector3.right * 0.2f, transform.rotation);
            GameObject bulletc = Instantiate(bolletObjB, transform.position + Vector3.up * 0.5f, transform.rotation);
            GameObject bullet_ll = Instantiate(bolletObjA, transform.position + Vector3.up * 0.5f + Vector3.left * 0.2f, transform.rotation);
            Rigidbody2D rigid_rr = bullet_rr.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidc = bulletc.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigid_ll = bullet_ll.gameObject.GetComponent<Rigidbody2D>();
            rigid_rr.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            rigidc.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            rigid_ll.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        }
        curShotLoad = 0;
    }

    void ReLoad()
    {
        curShotLoad += Time.deltaTime;
        curBoomLoad += Time.deltaTime;
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
        if (collision.gameObject.tag == "BorderPlayer")
        {
            switch (collision.gameObject.name)
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
        else if(collision.gameObject.tag =="EnemyBullet"  || collision.gameObject.tag == "Enemy")
        {

            if (isNoDeath == true)
            {
                Destroy(collision.gameObject);
            }
            else if (gameObject.activeSelf == true )
            {
                life--;
                gameObject.SetActive(false);
                Destroy(collision.gameObject);
                if(life > 0) Invoke("PlayerReload", 2.0f);

                manager.PlayerLifeIcon(life);

                if (life < 1)
                {
                    manager.GameOver();
                }
            }
        }
        else if(collision.gameObject.tag == "Item")
        {
            switch(collision.gameObject.name)
            {
                case "ItemPlayer(Clone)":
                    Destroy(collision.gameObject);
                    if(life < 3) life++;
                    manager.PlayerLifeIcon(life);
                    break;

                case "ItemPower(Clone)":
                    Destroy(collision.gameObject);
                    if(power < 3) power++;
                    break;

                case "ItemBoom(Clone)":
                    Destroy(collision.gameObject);
                    FierBoom();
                    break;
            }
        }

    }

    void FierBoom()
    {
        if (curBoomLoad < maxBoomLoad)
            return;
        for (int i=1; i<=3; i++)
        {
            float tab = 0.5f;
            for (int j=1; j<=15; j++)
            {
                tab += 0.5f;
                Vector3 ops = new Vector3(-5.0f+tab, -2.0f+(i*0.5f * -1), 0);
                GameObject bullet = Instantiate(bolletObjB, ops, transform.rotation);
                Rigidbody2D rigid = bullet.gameObject.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
        }
        curBoomLoad = 0;
    }

    void PlayerReload()
    {
        gameObject.SetActive(true);
    }

    void HotKey()
    {
        if (Input.GetKey(KeyCode.F1)) power = 1;
        if (Input.GetKey(KeyCode.F2)) power = 2;
        if (Input.GetKey(KeyCode.F3)) power = 3;
        if (Input.GetKey(KeyCode.F4))
        {
            if (curBoomLoad < maxBoomLoad)
                return;
            FierBoom();
            curBoomLoad = 0;
        }
        if (Input.GetKey(KeyCode.F10)) ShieldOnOff();


    }

    void ShieldOnOff()
    {
        if (isNoDeath == true)
        {
            isNoDeath = false;
            if (gameObject.tag == "Shield")
                gameObject.SetActive(false);
        }
        else 
        {
            isNoDeath = true; 
            if (gameObject.tag == "Shield")
                gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderPlayer")
        {
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
}

