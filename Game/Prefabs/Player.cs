using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //***
    private GamePlay gamePlay;
    private LessonPlay lessonPlay;

    //***
    [Header("Objects")]
    public RectTransform rtPoint;

    //***
    private Point point;

    //***
    [Header("Variables")]
    private Vector2 position;

    //****************************************************************************************************
    void Start()
    {
        gamePlay = Camera.main.GetComponent<GamePlay>();
        lessonPlay = Camera.main.GetComponent<LessonPlay>();
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        point = collision.gameObject.GetComponent<Point>();
        if (point.isEnd)
        {
            return;
        }

        point.GetComponent<BoxCollider2D>().enabled = false;

        point.isStart = false;
        point.isEnd = true;
        point = null;

        if (gamePlay == null) lessonPlay.GetPoint();
        else gamePlay.GetPoint();
    }

    //****************************************************************************************************
}
