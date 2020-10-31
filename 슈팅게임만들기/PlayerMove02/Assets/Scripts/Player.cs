using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed=10;
    public bool isTouchTop;
    public bool isTouchBootom;
    public bool isTouchRight;
    public bool isTouchLeft;


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v ==  1) || (isTouchBootom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    // 위 아래 벽에 다으면
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.tag == "Border")
        //{
            switch (collision.gameObject.name)
            {
                case ("Top"):
                    isTouchTop = true;
                    break;

                case ("Bootom"):
                    isTouchBootom = true;
                    break;

                case ("Right"):
                    isTouchRight = true;
                    break;

                case ("Left"):
                    isTouchLeft = true;
                    break;
            }

        //}
    }

    // 좌우 벽에 다으면
    void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Border")
        //{
            switch (collision.gameObject.name)
            {
                case ("Top"):
                    isTouchTop = false;
                    break;

                case ("Bootom"):
                    isTouchBootom = false;
                    break;

                case ("Right"):
                    isTouchRight = false;
                    break;

                case ("Left"):
                    isTouchLeft = false;
                    break;
            }
        //}
    }

}
