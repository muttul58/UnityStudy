using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 이동 속도
    public float speed=10;

    // 플레이어가 벽에 닫는지 확인 용
    bool isTouchTop;
    bool isTouchBottom;
    bool isTouchRight;
    bool isLeft;

    // 총알 발사 용 변수
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public float power=1;
    public float maxShotDelay;
    public float curShotDelay;
    


    // Update is called once per frame
    void Update()
    {
        PlayerMove();   // 플레이어 이동
        Fire();         // 총알발사
        Reload();       // 총알 장전
    }

    // 플레이어 이동
    void PlayerMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((isTouchRight && h==1) || (isLeft && h==-1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if((isTouchTop && v==1) || (isTouchBottom && v==-1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    // 총알 생성
    void Fire()
    {
        if(!Input.GetKey("space"))    // 스페이스바 눌렀을 때 총알 나감
            return;
        
        if(curShotDelay < maxShotDelay)    // 총알 장전시간을 설정
            return;

        switch(power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            break;

            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right*0.1f , transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left*0.1f , transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            break;

            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right*0.25f , transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left*0.25f , transform.rotation);
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

    // 총알 장전
    void Reload()
    {
        curShotDelay+=Time.deltaTime;
    }

    // 플레이어 벽에 닫는지 확인
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Border")
        {
            switch(other.gameObject.name)
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
                    isLeft = true;
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
                    isLeft = false;
                break;
            }
        }
    }

}
