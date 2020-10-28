using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // 플레이어 이동 속도
    public bool isTouchtTop;     // 위쪽 경계에 다았는가?
    public bool isTouchtBottom;  // 아래쪽 경계에 다았는가?
    public bool isTouchtRight;   // 오른쪽 경계에 다았는가?
    public bool isTouchtLeft;    // 왼쪽 경계에 다았는가?

    void Update()
    {

        // 10  플레이어 이동 설정
        float h = Input.GetAxisRaw("Horizontal"); // 좌우 방향키 설정
        if ((isTouchtRight && h == 1) || (isTouchtLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");  // 상하 방향키 설정
        if ((isTouchtTop && v == 1) || (isTouchtBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position; // 플레이어 현재 위치 가져오기
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime; // 플레이어의 다음 위치

        transform.position = curPos + nextPos; // 플레이어 이동하기
    }


    // Add Component > Box Collider 2D (콜라이더->피격 범위로 사용)
    // 상하좌우 동일 하게 만들기

        
    // 20 플레이어 상하좌우 경계처리
    // 경계에 다았을 때

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchtTop = true;
                    break;

                case "Bottom":
                    isTouchtBottom = true;
                    break;

                case "Right":
                    isTouchtRight = true;
                    break;

                case "Left":
                    isTouchtLeft = true;
                    break;
            }
        }
    }

    // 경계에서 떠러졌을 때
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchtTop = false;
                    break;

                case "Bottom":
                    isTouchtBottom = false;
                    break;

                case "Right":
                    isTouchtRight = false;
                    break;

                case "Left":
                    isTouchtLeft = false;
                    break;
            }
        }
    }

}

