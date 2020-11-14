using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float maxSpwanTime;
    public float curSpwanTime;
    public bool isBosSpwan;

    public GameObject[] enemyObjs;

    public GameObject player;
    public static int gameScore;

    public Text gameScoreText;

    public Image[] lifeimages;
    public GameObject gameOverSet;

    void Start()
    {

        gameScore = 200;
    }
    // Update is called once per frame
    void Update()
    {
        gameScoreText.text = string.Format("{0:n0}", gameScore);
        GamePlay();
        EnemySpwanTime();
    }

    void GamePlay()
    {
        if (curSpwanTime < maxSpwanTime)
            return;

        if (gameScore >= 300 && !isBosSpwan)
            EnemyBosSpwan();
        else if (!isBosSpwan)
            EnemySpwan();

        maxSpwanTime = Random.Range(1.0f, 3.0f);
        curSpwanTime = 0;
    }

    void EnemyBosSpwan()
    {
        isBosSpwan = true;
        Vector3 pos = new Vector3(0, 6.5f, 0);
        Instantiate(enemyObjs[3], pos, transform.rotation);
    }



    void EnemySpwan()
    {
        int ranEnemy = Random.Range(0, 3);
        Vector3 pos = new Vector3(Random.Range(-5.5f, 5.5f), 6.5f, 0);
        Instantiate(enemyObjs[ranEnemy], pos, transform.rotation);
    }

    void EnemySpwanTime()
    {
        curSpwanTime += Time.deltaTime;
    }
    
    public static void GameScoreUp(int enemyScore)
    {
        gameScore += enemyScore;
        Debug.Log("게임 점수 : " + gameScore);
    }

    public void PlayerLifeIcon(int life)
    {
        if (life > 3) life = 3;

        for (int i = 0; i < 3; i++)
            lifeimages[i].color = new Color(1, 1, 1, 0);

        for (int i = 0; i < life; i++)
            lifeimages[i].color = new Color(1, 1, 1, 1);
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameReStart()
    {
        gameScore = 0;
        SceneManager.LoadScene(0);
    }
}
