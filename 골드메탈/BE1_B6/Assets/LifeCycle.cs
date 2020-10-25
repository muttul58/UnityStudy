using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    // Update is called once per frame

    private void Start()
    {
       //VectorXYZ_1();        
    }


    void Update()
    {
        //InputKey();   // 키보드 입력
        //InputMouse(); // 마우스 입력
        //InputBotton();  // 버튼 입력 
        VectorXYZ_2();   
    }

    // Object 이동
    void VectorXYZ_1()
    {
        // 백터 변수 선언
        Vector3 vec = new Vector3(5, 0, 0); // 백터 값

        // 3차원 = 스칼라 값 + 방향
        transform.Translate(vec); 
        //int number = 4;  // 스칼라: 순수한 값

    }

    void VectorXYZ_2()
    {
        // 백터 변수 선언
        Vector3 vec = new Vector3(
            Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"), 
            0); // 백터 값

        // 3차원 = 스칼라 값 + 방향
        transform.Translate(vec);
        //int number = 4;  // 스칼라: 순수한 값

    }

    void InputKey()
    {
        // 마무키나 눌렀을 때
        if (Input.anyKey)
            Debug.Log("아무 키를 누르고 있습니다.");

        // Input.GetKeyDown 키보드 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Return))
            Debug.Log("엔터키를 눌러 아이템을 구입했습니다.");

        // Input.GetKey 키보드를 누르고 있을 때
        if (Input.GetKey(KeyCode.LeftArrow))
            Debug.Log("왼쪽 방향키로 왼쪽으로 이동 중");

        // Input.GetKeyUp 키보드를 눌렀다 땔 때 
        if (Input.GetKeyUp(KeyCode.RightArrow))
            Debug.Log("오른쪽 이동을 멈추었습니다.");

        // Esc 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
            Debug.Log("Esc 키로 취소 하였습니다.");
    }


    void InputMouse()
    {
        // 마우스 왼쪽 버튼 눌렀을 때
        if (Input.GetMouseButtonDown(0))
            Debug.Log("미사일 발사");

        // 마우스 왼쪽 버튼 누르고 있을 때
        if (Input.GetMouseButton(0))
            Debug.Log("미사일 모으는 중.....");

        // 마우스 온쪽 눌렸다 땔 때
        if (Input.GetMouseButtonUp(0))
            Debug.Log("슈퍼 미사일 발사!!!!!");
    }

    void InputBotton()
    {
        // 메뉴 > Edit > Project Settings... > Input Manager
        // 스페이스키 눌렸을 때
        if (Input.GetButtonDown("Jump"))
            Debug.Log("점프!");

        // 스페이스키 누르고 있을 때
        if (Input.GetButton("Jump"))
            Debug.Log("점프 모으는 중.....");

        // 스페이스키를 누르고 있다 땔 때3
        if (Input.GetButtonUp("Jump"))
            Debug.Log("슈퍼 점프!!");

        // 좌우 방향키 눌렸을 때
        if (Input.GetButtonDown("Horizontal"))
        {
            // Input.GetAxis() 0.??? 소수점 이하 정밀하게 반환
            Debug.Log("횡(좌우) 이동 중..." + Input.GetAxis("Horizontal"));
        }

        // 상하 방향키 눌렸을 때
        if (Input.GetButtonDown("Vertical"))
        {
            // Input.GetAxisRaw() -1, 0, 1 정수로 반환
            Debug.Log("종(상하) 이동 중..." + Input.GetAxisRaw("Vertical"));
        }
    }
}


