using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    GameObject[,] b;
    public GameObject boardPrefab;

    public float spaceX;
    public float spaceY;

    // Start is called before the first frame update
    void Start()
    {
        b = new GameObject[7, 7];

        for(int i = 0; i < 7; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                GameObject obj = Instantiate(boardPrefab, new Vector3(i*spaceX, -j*spaceX, 0), Quaternion.identity);
                b[i, j] = obj;
            }
        }

        b[0,0].GetComponent<BoardState>().ChangeGraphic(GraphicState.GS_BLUE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
