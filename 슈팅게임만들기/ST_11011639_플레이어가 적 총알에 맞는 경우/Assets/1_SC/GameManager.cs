using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score;

    public int currentStage = 0;
    
    public GameObject[] enemyObjs;      // 적 생성 용

    public float maxSpwanDelay;            // 적 생성 시간 설정
    public float curSpwanDelay;            

    public GameObject player;           // 적(Enemy)코드로 플레이어 위치 넘겨주기 위함

    public Image stageTitleImage;
    public Text stageTitleText;

    public Text scoreText;


    bool gamePlay = true;

    private void Start()
    {
        StartMessage();

        // 씬 이동 시 파괴되지 않는 오브젝트를 설정
        DontDestroyOnLoad(GameObject.Find("Default Object"));
    }

    void Update()
    {
        scoreText.text = score.ToString();

        if (currentStage == 1 && score >= 10)
        {
            SceneManager.LoadScene("Stage_2");
            StartMessage();
        }
        else if (currentStage == 2 && score >= 30)
        {
            SceneManager.LoadScene("Stage_3");
            StartMessage();
        }



        if (gamePlay)
        {
            // 스폰
            curSpwanDelay += Time.deltaTime;    // 적 스폰 시간 계산

            if (curSpwanDelay > maxSpwanDelay)   // 적 스폰 시간 이면
            {
                EnamySpwan();                   // 적 스폰 함숨 호출
                maxSpwanDelay = Random.Range(0.5f, 3f); // 다음 스폰될 시간 랜덤 설정
                curSpwanDelay = 0;              // 적 스폰시간 초기화
            }
        }

        void EnamySpwan()
        {
            int ranEnamy = Random.Range(0, 3);  // 적 L, M, S 중 랜덤으로 1개 선택
            Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), 6.5f, 0);

            Instantiate(enemyObjs[ranEnamy], pos, transform.rotation);
            /*
            // 적 생성해서 게임 오프젝트 enemy 에 저장
            GameObject enemy = Instantiate(enemyObjs[ranEnamy], 
                                            spPoints[ranPoint].position, 
                                            spPoints[ranPoint].rotation
                                            );
            // enemy 오브젝트의 위치, 방향, 속도 값 사용 초기화
            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
            Enemy enamyLogic = enemy.GetComponent <Enemy>();

            // 적이 생성된 이후기 때문에 플레이어의 값을 적에게 넘겨 줌
            enamyLogic.player = player;

            // 생성된 적이 아래로 이동
            rigid.velocity = new Vector2(0, enamyLogic.speed * (-1));
            */
        }
    }

    public static void ScoreUp(int amount)
    {
        score += amount;
    }

    void StartMessage()
    {
        gamePlay = false;

        currentStage += 1;
        stageTitleImage.gameObject.SetActive(true);
        stageTitleText.text = (string)("Stage " + currentStage);

        Invoke("StageTitleInvoke", 1.0f);
    }

    void StageTitleInvoke()
    {
        stageTitleImage.gameObject.SetActive(false);
        gamePlay = true;
    }
}
