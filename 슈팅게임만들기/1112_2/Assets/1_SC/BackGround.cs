using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    void Update()
    {
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if(sprites[startIndex].position.y < -26)
        {
            Vector3 endPos = sprites[endIndex].localPosition;
            sprites[startIndex].localPosition = endPos + Vector3.up * 26;

            int temp = startIndex;
            startIndex = endIndex;
            endIndex = temp;
        }
    }
}
