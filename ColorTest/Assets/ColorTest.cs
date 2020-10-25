using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{
    public GameObject game;
    bool check;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(check == true)
        {
            if (Input.GetMouseButton(0))
            {
                ChangeColor();
            }
        }
    }

    private void OnMouseOver()
    {
        check = true;
    }

    private void OnMouseExit()
    {
        check = false;
    }

    public void ChangeColor() 
    {
        game.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        Debug.Log("hello");
    }
}
