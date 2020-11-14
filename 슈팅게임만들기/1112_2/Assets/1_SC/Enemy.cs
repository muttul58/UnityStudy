using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public string name;
    public int hp;
    public int enemyScore;
    public float maxShotReload;
    public float curShotReload;
    public float maxShotReloadBos;
    public float curShotReloadBos;
    public bool isBosPlay;

    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    public GameObject player;

    public GameObject bolletObjA;
    public GameObject bolletObjB;
    public GameObject[] itemObjs;

    // 보스 패턴 관련 
    public int patternIndex;
    public int[] maxPatternCount;
    public int curPatternCount;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.down * speed;

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (gameObject.tag != "EnemyBos")
        {
            Fire();
        }
        if (gameObject.tag == "EnemyBos" && isBosPlay==false)
        {
            isBosPlay = true;
            Invoke("StopBos", 3);
        }

        Reload();
    }

    void StopBos()
    {

        Debug.Log("보스 스탑 로직 진입");
        if (gameObject.tag == "EnemyBos")
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.zero;
        }
        Invoke("Think", 3);
    }

    void Think()
    {

        if (player.gameObject.activeSelf == false)
            return;

        curPatternCount = 0;
        patternIndex = (patternIndex == 3) ? 0 : patternIndex + 1;

        switch (patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                //FireAround();
                break;
        }
    }

    void FireFoward()
    {
        if (player.gameObject.activeSelf == false)
            return;

        GameObject bulletRR = Instantiate(bolletObjA, transform.position + Vector3.right * 1.5f, transform.rotation);
        GameObject bulletR  = Instantiate(bolletObjA, transform.position + Vector3.right * 0.5f, transform.rotation);
        GameObject bulletL  = Instantiate(bolletObjA, transform.position + Vector3.left * 0.5f, transform.rotation);
        GameObject bulletLL = Instantiate(bolletObjA, transform.position + Vector3.left * 1.5f, transform.rotation);
        Rigidbody2D rigidRR = bulletRR.gameObject.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR  = bulletR.gameObject.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL  = bulletL.gameObject.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.gameObject.GetComponent<Rigidbody2D>();
        Vector3 playerPosRR = player.transform.position - transform.position;
        Vector3 playerPosR  = player.transform.position - transform.position;
        Vector3 playerPosL  = player.transform.position - transform.position;
        Vector3 playerPosLL = player.transform.position - transform.position;
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidR .AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL .AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        Debug.Log("앞으로 4발 발사");
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 1);
        else
            Invoke("Think", 2);
    }

    void FireShot()
    {
        Debug.Log("플레이어 방향으로 샷건.");

        if (player.gameObject.activeSelf == false)
            return;

        GameObject bullet = Instantiate(bolletObjA, transform.position + Vector3.down * 0.5f, transform.rotation);
        Rigidbody2D rigid = bullet.gameObject.GetComponent<Rigidbody2D>();
        Vector3 playerPos = player.transform.position - transform.position;
        rigid.AddForce(playerPos.normalized * 5, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 0.2f);
        else
            Invoke("Think", 2);
    }

    void FireArc()
    {
        Debug.Log("부채모양 발사");

        if (player.gameObject.activeSelf == false)
            return;

        for (int i = 1; i <= 10; i++)
        {

            GameObject bulletRR = Instantiate(bolletObjA, transform.position + Vector3.right * 1.5f, transform.rotation);
            GameObject bulletR = Instantiate(bolletObjA, transform.position + Vector3.right * 0.5f, transform.rotation);
            GameObject bulletL = Instantiate(bolletObjA, transform.position + Vector3.left * 0.5f, transform.rotation);
            GameObject bulletLL = Instantiate(bolletObjA, transform.position + Vector3.left * 1.5f, transform.rotation);
            Rigidbody2D rigidRR = bulletRR.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = bulletR.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidLL = bulletLL.gameObject.GetComponent<Rigidbody2D>();
            Vector3 playerPosRR = player.transform.position - transform.position;
            Vector3 playerPosR = player.transform.position - transform.position;
            Vector3 playerPosL = player.transform.position - transform.position;
            Vector3 playerPosLL = player.transform.position - transform.position;
            rigidRR.AddForce(playerPosRR.normalized * 5, ForceMode2D.Impulse);
            rigidR.AddForce(playerPosR.normalized * 5, ForceMode2D.Impulse);
            rigidL.AddForce(playerPosL.normalized * 5, ForceMode2D.Impulse);
            rigidLL.AddForce(playerPosLL.normalized * 5, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 2);
    }
    /*
    void FireAround()
    {
        Debug.Log("원형으로 전체 공격");
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 2);
    }
    */

    void Fire()
    {
        if (curShotReload < maxShotReload)
            return;
        if (player.gameObject.activeSelf == false)
            return;

        GameObject bullet = Instantiate(bolletObjA, transform.position + Vector3.down * 0.5f, transform.rotation);
        Rigidbody2D rigid = bullet.gameObject.GetComponent<Rigidbody2D>();
        Vector3 playerPos = player.transform.position - transform.position;
        rigid.AddForce(playerPos.normalized  * 4, ForceMode2D.Impulse);

        maxShotReload = Random.Range(2.0f, 4.0f);

        curShotReload = 0;
    }

    void Reload()
    {
        curShotReload += Time.deltaTime;
        curShotReloadBos += Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (hp <= 0) return;

        if (collision.gameObject.tag == "BorderOut")
            Destroy(gameObject);
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            hp -= bullet.dmg;
            if (hp < 0)
            {
                Destroy(gameObject);
                GameManager.GameScoreUp(enemyScore);
                ItemDrop();
            }
            Destroy(collision.gameObject); 
            spriteRenderer.sprite = sprites[1];
            Invoke("EnemySpriteReturn", 0.1f);
        }
    }

    void ItemDrop()
    {
        int ran = Random.Range(0, 10);
        int selectItem=0;
        if (ran <= 2) ;
        else if (ran <= 4) selectItem = 0;
        else if (ran <= 7) selectItem = 1;
        else if (ran <= 10) selectItem = 2;

        GameObject itemName = Instantiate(itemObjs[selectItem], transform.position, transform.rotation);
        Rigidbody2D rigid = itemName.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.down * 2, ForceMode2D.Impulse);
    }

    void EnemySpriteReturn()
    {
        spriteRenderer.sprite = sprites[0];
    }
}
