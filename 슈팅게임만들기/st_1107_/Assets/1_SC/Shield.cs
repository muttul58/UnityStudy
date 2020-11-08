using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor.U2D.Path;

public class Shield : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;
   
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position;
        ShieldOnOff();
    }

    void ShieldOnOff()
    {
        if (GameManager.isShield == true) 
        {
            gameObject.SetActive(true);

        }
        else if(GameManager.isShield )
        {
            gameObject.SetActive(false);
        }

    }
}
