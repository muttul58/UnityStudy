using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;      // 적 오브젝트
    public Transform[] spawnPoints;     // 적 5곳에 스폰

    public float maxSpawnDelay;     // 스폰 시작 시간 변수
    public float curSpawnDelay;     // 스폰 끝 시간 변수

    void Update()
    {
        curSpawnDelay += Time.deltaTime;    // 현재 시간 누적

        // 현재 시간이 스폰 설정 시간보다 크면
        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();                           // 적 스폰
            maxSpawnDelay = Random.Range(0.5f, 3f); // 적 스폰 시간 랜덤(0.5초 ~ 3초)
            curSpawnDelay = 0;                      // 현재 시간 0 초기화
        }

        void SpawnEnemy()
        {
            int ranEnemy = Random.Range(0, 3);  // 적 생성 종류 3개 중 1개 랜덤 선택
            int ranPoint = Random.Range(0, 5);  // 적 생성 위치 5개 중 1개 랜덤 선택

            // 적 생성 ( 적오프젝트[랜덤선택], 스폰위치[랜덤선택].위치, 스폰위치[랜덤선택].회전)
            Instantiate(enemyObjs[ranEnemy], 
                        spawnPoints[ranPoint].position, 
                        spawnPoints[ranPoint].rotation
                        );
        }
    }
}
