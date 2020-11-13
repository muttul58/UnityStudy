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

    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    public GameObject player;

    public GameObject bolletObjA;
    public GameObject bolletObjB;
    public GameObject[] itemObjs;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.down * speed;

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Fire();
        Reload();
    }

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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hp <= 0) return;
        if (collision.gameObject.tag == "BorderOut")
            Destroy(gameObject);
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject); 
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            hp -= bullet.dmg;
            if (hp < 0)
            {
                Destroy(gameObject);
                GameManager.GameScoreUp(enemyScore);
                ItemDrop();
            }
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
