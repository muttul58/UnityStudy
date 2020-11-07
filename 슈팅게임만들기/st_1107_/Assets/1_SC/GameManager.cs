using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public GameObject Player;

    public GameObject[] enemyObjs;
    Rigidbody2D rigid;
    
    public float SpwanPoint;
    public float maxSpwanDelay;
    public float curSpwanDelay;
    public string enemyName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curSpwanDelay += Time.deltaTime;

        if(curSpwanDelay > maxSpwanDelay)
        {
            EnemySpwan();
            maxSpwanDelay = Random.Range(0.5f, 3.0f);
            curSpwanDelay = 0;
        }
    }

    void EnemySpwan()
    {
        int ranEnemy = Random.Range(0, 3);
        Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), 7.0f, 0);
        Instantiate(enemyObjs[ranEnemy], transform.position, transform.rotation);
    }
}
