using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    //***
    [Header("Objects")]
    public GamePlay gamePlay;
    public LessonPlay lessonPlay;

    //***
    public RectTransform point;

    //***
    public Image img;

    //***
    [Header("Variables")]
    private Color color;

    //***
    private Vector2 pos;

    //***
    public float speed;
    private float deltaTime;

    //***
    public bool isStart = true;
    public bool isEnd = false;

    //****************************************************************************************************
    void Awake()
    {
        gamePlay = Camera.main.GetComponent<GamePlay>();
        lessonPlay = Camera.main.GetComponent<LessonPlay>();
    }

    //****************************************************************************************************
    void Start()
    {
        point.localScale = Vector2.zero;
        color = img.color;
        color.a = 0.25f;
        img.color = color;
        isStart = true;
    }

    //****************************************************************************************************
    void FixedUpdate()
    {
        deltaTime = Time.deltaTime;

        pos = point.anchoredPosition;
        pos.y -= speed * deltaTime;
        point.anchoredPosition = pos;

        color = img.color;

        if (isStart)
        {
            if (point.localScale.x >= 1)
            {
                point.localScale = Vector2.one;
                color.a = 1;
                isStart = false;
            }
            else
            {
                pos = point.localScale;
                pos.x += 0.5f * deltaTime;
                pos.y += 0.5f * deltaTime;
                point.localScale = pos;
                color.a = color.a + 0.25f * deltaTime;
            }
        }
        else if (!isEnd)
        {
            color.a = Mathf.Sin(Time.time + speed) + 1.65f;
        }
        else if (isEnd)
        {
            if (point.localScale.x <= 0)
            {
                point.localScale = Vector2.zero;
                color.a = 0;
                
                Destroy(point.gameObject);
                isEnd = false;
            }
            else
            {
                pos = point.localScale;
                pos.x -= 0.5f * deltaTime;
                pos.y -= 0.5f * deltaTime;
                point.localScale = pos;
                
                if (color.a > 1) { color.a = 1; }
                color.a = color.a - 0.5f * deltaTime;
            }
        }

        img.color = color;

        if (gamePlay == null)
        {
            if (point.anchoredPosition.y < -lessonPlay.canvasPoints.rect.height * 0.5f - 75)
            {
                lessonPlay.BreakPoint();
                Destroy(point.gameObject);
            }
        }
        else
        {
            if (point.anchoredPosition.y < -gamePlay.canvasPoints.rect.height * 0.5f - 75)
            {
                gamePlay.BreakPoint();
                Destroy(point.gameObject);
            }
        }
    }

    //****************************************************************************************************
}
