using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.Serialization;



public class MoveCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject game;
    public GameObject[] Objt;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 10 * Time.deltaTime);
        }

         else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 10 * Time.deltaTime);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * 10 * Time.deltaTime);
        }
    }

    public void ClickCube() 
    {   //                                              r , g, b, a = alpha
        game.GetComponent<Renderer> ().material.color = new Color(1,0,0);

        Debug.Log("hello");
    }


    [System.Serializable]  //  MonoBehaviour가 아닌 클래스에 대해 Inspector에 나타내기.
    public class MapArray
    {
        public GameObject[] data;
    }

    public class MyScript : MonoBehaviour
    {
        public MapArray[] map;
    }
}
