using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Sprite[] sprites;    // 적 비행기 색 2가지 : 짖은색, 밝은색 변경
    public float speed;         // 적 비해기 이동 속도
    public float health;        // 적 체력

    SpriteRenderer spriteRenderer;  // 스프라이스 컨트롤
    Rigidbody2D rigid;              // 리지드바디 컨트롤

    public GameObject bulletObjA;   // 총알 A : 작은 것
    public GameObject bulletObjB;   // 총알 B : 큰 것

    public float maxShotDelay;      // 총알 장전 시간
    public float curShotDelay;      // 총알 장전 시간 초기화

    public string enemyName;        // 적 비행이 이른 L, M, S
    public GameObject player;       // player 현재 위치값 가저오기 위함

    public int enemyScore;
    bool isDead = false;

    void Awake()
    {
        //컨트롤 초기화
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.down * speed;      // 적 비행기 방향과 속도 설정 

        player = GameObject.FindWithTag("Player");  // Tag가 Player 인 오브젝트를 하이어키뷰에서 찾는다.
    }

    void Update()
    {
        Fire();     // 총알 발사
        Reload();   // 총알 장전 시간을 위한 시간 계산
    }

    void Fire()
    {

        if (curShotDelay < maxShotDelay)    // 장전시간이 안된경우 
            return;

        if(enemyName == "S")    // 적 비행이 이름이 "S"인 경우
        {
            // 총알 생성 : 플레이어 총알 복사
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            
            // GameManager 에서 넘겨받은 player 위치값으로 총알 방향 결정
            Vector3 dirVec = player.transform.position - transform.position;

            // dirVec 에서 결정한 방향으로 총알 발사
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")   // 적  비행이 이름이 "L"인 경우
        {
            GameObject bullet = Instantiate(bulletObjB, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;

            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        curShotDelay = 0;   // 장전 시간 초기화
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime; // 장전 시간 계산
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // 외각 경계와 Player 총알에 맞은 경우
        if (collision.gameObject.tag == "BulletBorder") // 경계에 다인 경우
            Destroy(gameObject);
        else if(collision.gameObject.tag == "Bullet")   // player 총알에 맞은 경우
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();  // 총알 데이지 가져오기
            OnHit(bullet.dmg);                                            // OnHit() 함수오 데이지 전달
            Destroy(collision.gameObject);                                // 총알 소멸
        }
    }

    void OnHit(int dmg) // 총알에 맞았을 때
    {
        health -= dmg;                          // 데미지 만큼 체력 감소
        spriteRenderer.sprite = sprites[1];     // 밝은 sprite로 변경
        if(health <= 0 && !isDead)              // 체력이 1보다 작은 경우
        {
            GameManager.ScoreUp(enemyScore);
            isDead = true;
            Destroy(gameObject);                // 총알 맞은 비행기 소멸
        }   
        Invoke("ReturnSp", 0.1f);               // 체력이 남은 경우 다시 원래 색으로 변경 함수 호출
    }
    public static int idx;

    void ReturnSp() // 밝은 색으로 변경 함수
    {
        spriteRenderer.sprite = sprites[0];
    }

}
