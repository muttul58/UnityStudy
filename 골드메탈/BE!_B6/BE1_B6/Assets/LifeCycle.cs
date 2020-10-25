using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            Debug.Log("몬스터 사냥!!");

        //if (Input.anyKey)
        //    Debug.Log("아무 키를 누르고 있습니다.");
        /*
        if (Input.GetKeyDown(KeyCode.Return))
            Debug.Log("엔터키를 눌러 아이템을 구입했습니다.");
        
        if (Input.GetKey(KeyCode.LeftArrow))
            Debug.Log("왼쪽 방향키로 왼쪽으로 이동 중");

        if (Input.GetKeyUp(KeyCode.RightArrow))
            Debug.Log("오른쪽 이동을 멈추었습니다.");

        if (Input.GetKeyDown(KeyCode.Escape))
            Debug.Log("Esc 키로 취소 하였습니다.");
        */
        if (Input.GetMouseButtonDown(0))
            Debug.Log("미사일 발사");
        
        if (Input.GetMouseButton(0))
            Debug.Log("미사일 모으는 중.....");

        if (Input.GetMouseButtonUp(0))
            Debug.Log("슈퍼 미사일 발사!!!!!");

    }
}
