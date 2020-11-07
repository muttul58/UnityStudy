using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public int dmg;     // 총알 데미지 변수

    // 총알이 바같쪽 경계에 다으면 소멸
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletBorder")
            Destroy(gameObject);
    }
}
