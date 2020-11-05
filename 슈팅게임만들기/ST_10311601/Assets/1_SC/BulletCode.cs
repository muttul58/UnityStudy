using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCode : MonoBehaviour
{
    public int dmg;     // 총알 데미지 총알 마다 다르게 설정
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알이 바깥쪽 경계에 다으면 소멸
        if (collision.gameObject.tag == "BulletBorder")
            Destroy(gameObject);
    }
}
