﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float power;

    public bool isTuochTop;
    public bool isTuochBottom;
    public bool isTuochRight;
    public bool isTuochLeft;

    public float maxShotDelad;
    public float curShotDelad;

    public GameObject buttetObjA;
    public GameObject buttetObjB;
    public GameObject buttetObjC;


    void Start()
    {
        speed = 10;
        power = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Fier();
        ReLoad();

    }

    void Fier()
    {
        if (!Input.GetKey("space"))
            return;
        if (curShotDelad < maxShotDelad)
            return;

        GameObject bullet =  Instantiate(buttetObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        curShotDelad = 0;

    }

    void ReLoad()
    {
        curShotDelad += Time.deltaTime;
    }

    void PlayerMove()
    {

        float h = Input.GetAxisRaw("Horizontal");
        if( (isTuochRight && h==1) || (isTuochLeft && h==-1) )
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTuochTop && v == 1) || (isTuochBottom && v == -1))
            v = 0;

        Vector3 cutPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = cutPos + nextPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderPlayer")
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTuochTop = true;
                    break;

                case "Bottom":
                    isTuochBottom = true;
                    break;

                case "Right":
                    isTuochRight = true;
                    break;

                case "Left":
                    isTuochLeft = true;
                    break;
            }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderPlayer")
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTuochTop = false;
                    break;

                case "Bottom":
                    isTuochBottom = false;
                    break;

                case "Right":
                    isTuochRight = false;
                    break;

                case "Left":
                    isTuochLeft = false;
                    break;
            }

    }

}

