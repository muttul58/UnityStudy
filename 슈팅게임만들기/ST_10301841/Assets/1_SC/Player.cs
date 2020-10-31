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


    // Update is called once per frame
    void Update()
    {
        PlayerMove();   // 플레이어 이동
        Fire();         // 총알발사
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
        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);    // *10 : 총알 속도 


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
