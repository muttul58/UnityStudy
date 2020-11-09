using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
//using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemyObjs;

    public float maxSpwanDelay;
    public float curSpwanDelay;

    public static int gameScore;
    public Text gameScoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;






    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("DefaultObject"));
        gameScore = 0;
    }

    void Update()
    {

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

    


    public static void ScoreUp(int enemyScore)
    {
        gameScore += enemyScore;
    }


    public void updateLifeIncon(int life)
    {
        for (int i = 0; i < 3; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i<life; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void gameOver()
    {
        gameOverSet.SetActive(true);

    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

}
