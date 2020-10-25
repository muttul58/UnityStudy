using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GraphicState{ GS_WHITE, GS_BLUE, GS_BLUESELECT, GS_RED, GS_REDSELECT, GS_MOVERANGE }

public class BoardState : MonoBehaviour
{
    public GraphicState graphicState;

    void Start()
    {
        graphicState = GraphicState.GS_WHITE;
    }

    void OnMouseDown()
    {
        
    }

    public void ChangeGraphic(GraphicState state)
    {
        graphicState = state;

        if (state == GraphicState.GS_WHITE)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board_Background");
        }

        if (state == GraphicState.GS_BLUE)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("V_Blue_0");
        }

        if (state == GraphicState.GS_BLUESELECT)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("V_Blue_1");
        }

        if (state == GraphicState.GS_RED)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("V_Red_0");
        }

        if (state == GraphicState.GS_REDSELECT)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("V_Red_1");
        }

        if (state == GraphicState.GS_MOVERANGE)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Move");
        }
    }
}
