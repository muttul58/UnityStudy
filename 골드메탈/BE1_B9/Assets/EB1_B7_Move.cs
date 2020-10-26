using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB1_B7_Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // 목표 위치
    Vector3 target = new Vector3(-8, 0.5f, 0);
        
    // Update is called once per frame
    void Update()
    {
        // MoveTowards_();
         SmoothDamp_();
        // Lerp_();
        // Slerp_();
    }

    // 1. MoveTowards() >> 매개변수( 현재위치, 목표위치, 속도)로 구성
    void MoveTowards_()
    {
        transform.position = 
            Vector3.MoveTowards(transform.position
                                , target
                                , 0.2f);
    }

    // 2. SmoothDamp() >> 목표 위치에 가까워 질수록 천천히 이동
    void SmoothDamp_()
    {
        Vector3 velo = Vector3.zero;

        // 마지막 매개변수에 반비례하여 속도 증가
        transform.position = 
            Vector3.SmoothDamp(transform.position
                               , target
                               , ref velo
                               , 0.1f); 
    }

    // 3. Lerp(선형 보간)
    void Lerp_()
    {
        transform.position = 
            Vector3.Lerp(transform.position
                        , target, 0.02f);
    }

    // 4. SLerp(구면 선형 보간)
    void Slerp_()
    {
        transform.position =
            Vector3.Slerp(transform.position
                        , target, 0.01f);
    }
}
