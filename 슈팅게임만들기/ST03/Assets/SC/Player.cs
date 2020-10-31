using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed=10;  // 플레이어  이동 속도
    public bool isTouchTop;  // 플레이어 화면 밖으로 나가는 것 방지
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;


    public float maxShotDelay;  // 총알 발사 시간 최대
    public float curShotDelay;  // 총알 발사 시간 현재
    

    // 총알 저장 변수
    public GameObject bulletObjA;  // 작은 총알
    public GameObject bulletObjB;  // 큰 총알

    public float power;  // 1: 작은것 하나 발사, 2: 작은것 두개 발사, 3: 작+큰+작 발사
    

    void Update()
    {
        PlayerMove(); // 플레이어 이동
        Fire();       // 총알 생성
        Reload();     // 총알 발사 시간 계산
    }


    // 총알 생성
    void Fire()
    {
        //if(!Input.GetButton("Fire1"))
        if(!Input.GetButtonDown("Jump"))  // 스페이스키 누르면 발사
            return;

        if(curShotDelay < maxShotDelay)   // 총알 발사 속도 제어 maxShotDelay 보다 커지면 발사
            return;
        
        // 총알 종류 설정
        switch (power)  
        {
            case 1: // 작은것 하나 발사
                                            //생성할 프리팹,      위치,             방향
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            break;

            case 2:                                                             // 총알의 오른쪽, 왼쪽으로 조금 이동
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left  * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            break;

            case 3:                            // 총알의 오른쪽A, 가운데B, 왼쪽A 타입
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left  * 0.3f, transform.rotation);
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

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if((isTouchTop && v==1)|| (isTouchBottom && v==-1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos+nextPos;
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Border")
        {
            switch(other.gameObject.name)
            {
                case("Top"):
                    isTouchTop = true;
                break;

                case("Bottom"):
                    isTouchBottom = true;
                break;

                case("Right"):
                    isTouchRight = true;
                break;

                case("Left"):
                    isTouchLeft = true;
                break;   
            }
        }   
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Border")
        {
            switch(other.gameObject.name)
            {
                case("Top"):
                    isTouchTop = false;
                break;

                case("Bottom"):
                    isTouchBottom= false;
                break;

                case("Right"):
                    isTouchRight = false;
                break;

                case("Left"):
                    isTouchLeft = false;
                break;   
            }
        }
    }

}
