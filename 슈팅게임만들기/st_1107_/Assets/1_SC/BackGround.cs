using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize*2*2;
    }

    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if (sprites[0].position.y < viewHeight * (-1))
        {
            sprites[0].transform.localPosition = sprites[2].transform.localPosition + Vector3.up * viewHeight;
        }
        if (sprites[1].position.y < viewHeight * (-1))
        {
            sprites[1].transform.localPosition = sprites[0].transform.localPosition + Vector3.up * viewHeight;
        }
        if (sprites[2].position.y < viewHeight * (-1))
        {
            sprites[2].transform.localPosition = sprites[0].transform.localPosition + Vector3.up * viewHeight;
        }

        /*
        if (sprites[endIndex].position.y < viewHeight*(-1))
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;

            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;

            int temp = startIndex;
            startIndex = endIndex;
            endIndex = (temp-1 == -1) ? sprites.Length-1 : temp-1;
        }*/
    }
}
