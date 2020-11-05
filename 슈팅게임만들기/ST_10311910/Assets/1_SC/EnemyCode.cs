using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCode : MonoBehaviour
{
    /*
        Awake(), Start() 차이점
        유니티에서 지정한 함수로서 초기화시 사용되는 함수들이다.단 한번만 호출된다.
 
        1.    Awake : 인스펙터창에서 스크립트요소를 비활성화 해도 실행된다.스크립트와 초기화 사이의 모든 레퍼런스 설정에 이용
        2.   Start : Awake다음으로 첫 업데이트 직전에 호출되지만 스크립트 요소가 활성화 상태여야 합니다.

        예))
        Awake: 적들에게 총의 총알을 초기화한다.(Set)
        Start: 적들에게 총을 쏳을 능력을 부여할때
    */

    public Sprite[] sprites;    // 적 비행기 여러 종류
    public float enemySpeed;    // 적 스피드
    public float enemyHealth;   // 적 체력

    SpriteRenderer spriteRenderer;  // 적 비행기 총알 맞았을 때 번쩍임 구현
    Rigidbody2D rigid;              // 적 비행기 아래쪽으로 이동 구현

    void Awake()
    {
        // 변수 초기화
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * enemySpeed; // 적 비행기 방향 아래로 스피드 속도로 이동
    }

    // 플레이어 총알에 적이 맞았을 때 
    void OnHit(int dmg)
    {
        enemyHealth -= dmg;                 // 플레이어 데이지 만큼 힘을 뺀다
        spriteRenderer.sprite = sprites[1]; // 배열의 두번째 스프라이트(밝은색)로 변경
        Invoke("ReTurnSprite", 0.1f);       // Invoke() 함수로 변경지연시간 설정 0.1초

        if (enemyHealth <= 0)               // 적의 체력이 0보다 작거나 같으면 파괴
        {
            Destroy(gameObject); // 적비행기 파괴
        }
    }

    // 적이 총알에 맞았을 때 뻔쩍이기 위해 밝은 색으로 변경 후 다시 짙은색으로 변경
    void ReTurnSprite()
    {
         spriteRenderer.sprite = sprites[0];
    }

    // 적이 바같쪽 경계와, 플레이어 총알에 맞았을 때
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletBorder")         // 바같쪽 벽에 다음
            Destroy(gameObject);                                // 적 파괴
        else if (collision.gameObject.tag == "PlayerBullet")    // 플레이어 총알에 맞음
        {
            // BulletCode 코드의 dmg 변수 값을 가져오기 위해 선언
            BulletCode bullet = collision.gameObject.GetComponent<BulletCode>();
            OnHit(bullet.dmg);  // 가져온 dmg 값 OnHit() 함수로 전달
            Destroy(collision.gameObject); // 플레이어 총알 이 적에게 다으면 파괴
        }
    }

}
