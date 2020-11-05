using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;             // 플레이어 속도
    public float power;             // 총알 종류

    public bool isTouchTop;         // 경계 선에 다았는 확인용
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public GameObject bulletObjA;   // 총알 종류 A : 작은 것
    public GameObject bulletObjB;   //           B : 큰 것

    public float maxShotDelay;      // 총알 장전 시간 계산용
    public float curShotDelay;


    private void Start()            // 변수 초기화
    {
        speed = 10;
        power = 3;
        maxShotDelay = 0.15f;
    }

    void Update()
    {
        PlayerMove();   // 플레이어 이동 함수
        Fire();         // 총알 발사 함수
        Reload();       // 총알 장전 시간 계산 함수
        PowerSet();     // 총알 단축키 1,2,3
    }

    void Fire() // 총알 발사 함수
    {
        // space 키 누르면 발사
        if (!Input.GetKey("space"))         // 스페이스키가 아니면 return
            return;
        
        if (curShotDelay < maxShotDelay)    // 총알 장전 시간보다 작으면
            return;

        switch(power)                       // power 1:작,  2:작작,  3:작큰작 
        {
            case 1:
                // 총알생성
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>(); // 총알 방향, 속도 제어용
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);   // 총알 위쪽 방향 설정
                break;

            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position+Vector3.right *0.3f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position+Vector3.left * 0.3f, transform.rotation);
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
        }
        curShotDelay = 0;   // 총알 장전 시간 초기화
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime; // 총알 장전 시간 계산
    }

    void PowerSet() // 총알 종류 선택 단축키
    {
        if (Input.GetKey("1")) power = 1;
        if (Input.GetKey("2")) power = 2;
        if (Input.GetKey("3")) power = 3;
    }

    void PlayerMove()   // 플레이어 이동
    {
        // 방향키 좌우 키 사용
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        // 방향키 상하 키 사용
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        // 플레이어 현재 위치 
        Vector3 curPos = transform.position;
        // 플레이어 다음 위치
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        // 이동할 위치
        transform.position = curPos + nextPos;
    }


    void OnTriggerEnter2D(Collider2D collision) // 플레이어가 화면 바같으로 나지 않도록 함
    {
        if(collision.gameObject.tag =="Border")
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
        if (collision.gameObject.tag == "Border")
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
