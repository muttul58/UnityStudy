using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed=10;

    void Update()
    {
        PlayerMove();   // 플레이어 이동 설정
        //Bollot();       // 총알 설정
        //Fire();         // 총알 발사
    }

    void PlayerMove()
    {
        float h=Input.GetAxisRaw("Horizontal");
        float v=Input.GetAxisRaw("Vertical");
        
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos+nextPos;

    }
}


