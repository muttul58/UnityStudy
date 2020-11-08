using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemyObjs;

    public float maxSpwanDelay;
    public float curSpwanDelay;

    public static int gameScore;
    public Text gameScoreText;


    // 기능키
    public static bool isShield;


    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("DefaultObject"));
        gameScore = 0;
        isShield = false;
    }

    void Update()
    {
        HotKey();
        gameScoreText.text = string.Format("{0:n0}", gameScore);

        curSpwanDelay += Time.deltaTime;

        if(curSpwanDelay > maxSpwanDelay )
        {
            EnemySpwan();
            maxSpwanDelay = Random.Range(1.0f, 3.0f);
            curSpwanDelay = 0;
        }

    }


    void EnemySpwan()
    {
        if (player.activeSelf == true)
        {
            Vector3 pos = new Vector3(Random.Range(-5.5f, 5.5f), 6f, 0);
            Instantiate(enemyObjs[Random.Range(0, 3)], pos, transform.rotation);
        }
    }

    void HotKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            if (isShield == false)
            {
                isShield = true;
            }
            else
            {
                isShield = false;
            }

    }

    public static void ScoreUp(int enemyScore)
    {
        gameScore += enemyScore;
    } 

}
