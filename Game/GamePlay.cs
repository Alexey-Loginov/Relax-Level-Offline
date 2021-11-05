using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    //***
    private Skins skins;
    private SoundPlay soundPlay;

    private GameMenuAsk menuAsk;

    //***
    [Header("Objects")]
    public RectTransform player;

    public RectTransform panelTouch;
    public RectTransform panelTouchBG;

    public RectTransform canvasPoints;
    public RectTransform prefabPoint;

    public RectTransform gameLifes;
    public RectTransform gameLifesBG;

    //***
    [Header("Images")]
    public Image imgBG;
    public Image imgPanelTouch;
    public Image imgPanelTouchBG;
    public Image imgPlayer;
    public Image imgBtnSound;

    //***
    public Sprite imgSoundOn;
    public Sprite imgSoundOff;

    //***
    [Header("Variables")]
    private Color color;

    //***
    private Vector2 pos;

    private Vector2 touchDeltaPosition;
    private Vector2 beginPosition;

    //***
    private float deltaTime;

    private float newPointTime;
    private float newPointTimeNow;

    private float speedMove;
    public float pointSpeed;

    //***
    public int score = 0;
    private int pointNum = 0;
    private int winNum = 50;

    public int gameDifficulty;

    //***
    private bool isGameStarted;

    //****************************************************************************************************
    void Awake()
    {
        skins = Camera.main.GetComponent<Skins>();
        soundPlay = Camera.main.GetComponent<SoundPlay>();

        menuAsk = Camera.main.GetComponent<GameMenuAsk>();

        gameDifficulty = PlayerPrefs.GetInt("GameDifficulty");

        isGameStarted = false;
    }

    //****************************************************************************************************
    void Start()
    {
        StartGame();
 
        menuAsk.MenuRemove();

        if (soundPlay.isSound) imgBtnSound.sprite = imgSoundOn;
        else imgBtnSound.sprite = imgSoundOff;

        imgBG.sprite = skins.GetSkinBG(skins.skinBGNum);
        imgPlayer.sprite = skins.GetSkinPlayer(skins.skinPointNum);

        color = imgBG.color; color.a = 0; imgBG.color = color;
        color = imgPanelTouch.color; color.a = 0; imgPanelTouch.color = color;
        color = imgPanelTouchBG.color; color.a = 0; imgPanelTouchBG.color = color;
        color = imgPlayer.color; color.a = 0; imgPlayer.color = color;
        color = imgBtnSound.color; color.a = 0; imgBtnSound.color = color;

        pos.x = 0;
        pos.y = 100;
        gameLifesBG.anchoredPosition = pos;

        StartCoroutine(AnimateGameShow());
    }

    //****************************************************************************************************
    public void StartGame()
    {
        score = 0;
        pointNum = 0;

        pos.x = panelTouchBG.anchoredPosition.x;
        pos.y = panelTouchBG.anchoredPosition.y;
        player.anchoredPosition = pos;

        pos.x = Screen.width * 0.5f;
        pos.y = panelTouchBG.anchoredPosition.y;
        beginPosition = pos;

        if (gameDifficulty == 1)
        {
            pointSpeed = 60f;
            speedMove = 0.03f;
            newPointTime = 3.75f;
            winNum = 50;
        }
        else if (gameDifficulty == 2)
        {
            pointSpeed = 80f;
            speedMove = 0.04f;
            newPointTime = 2f;
            winNum = 75;
        }
        else if (gameDifficulty == 3)
        {
            pointSpeed = 90f;
            speedMove = 0.045f;
            newPointTime = 1.25f;
            winNum = 100;
        }
    }

    //****************************************************************************************************
    public void GameWin()
    {
        PlayerPrefs.SetInt("StatsWin", 1);
        PlayerPrefs.SetInt("StatsScore", score);

        PlayerPrefs.SetInt("GameWin", PlayerPrefs.GetInt("GameWin") + 1);
        PlayerPrefs.SetInt("GetPoint", PlayerPrefs.GetInt("GetPoint") + score);

        StartCoroutine(AnimateGameRemove());
    }

    public void GameQuit()
    {
        PlayerPrefs.SetInt("StatsWin", 1);
        PlayerPrefs.SetInt("StatsScore", score);

        PlayerPrefs.SetInt("GameWin", PlayerPrefs.GetInt("GameWin") + 1);
        PlayerPrefs.SetInt("GetPoint", PlayerPrefs.GetInt("GetPoint") + score);

        StartCoroutine(AnimateGameRemove());
    }

    //****************************************************************************************************
    public void GetPoint()
    {
        pointNum--;
        score++;

        pos.x = panelTouchBG.anchoredPosition.x;
        pos.y = panelTouchBG.anchoredPosition.y;
        player.anchoredPosition = pos;

        if (gameDifficulty == 3)
        {
            pointSpeed += 0.25f;
            newPointTime -= 0.003f;
        }

        if ((score >= winNum) && (pointNum == 0))
        {
            GameWin();
        }
    }

    //****************************************************************************************************
    public void BreakPoint()
    {
        pointNum--;

        pos.x = gameLifes.localScale.x + 1f / winNum;
        pos.y = 1;
        gameLifes.localScale = pos;
    }

    //****************************************************************************************************
    public void AddPoint()
    {
        newPointTimeNow = 0;
        pointNum++;

        RectTransform point = Instantiate(prefabPoint) as RectTransform;
        point.SetParent(canvasPoints);

        pos.x = Random.Range(canvasPoints.rect.x + 50, canvasPoints.rect.x + canvasPoints.rect.width - 50);
        pos.y = Random.Range(-canvasPoints.rect.y - 100, -canvasPoints.rect.y + 200);
        point.anchoredPosition = pos;
        point.localScale = Vector2.one;

        point.GetComponent<Point>().speed = pointSpeed + Random.Range(-35, 35);
        point.GetComponent<Image>().sprite = skins.GetSkinPoint(skins.skinPointNum);

        pos.x = gameLifes.localScale.x - 1f / winNum;
        pos.y = 1;
        gameLifes.localScale = pos;
    }

    //****************************************************************************************************
    void OnGUI()
    {
        if (!isGameStarted) return;

        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
        {
            if (menuAsk.isShowed)
            {
                menuAsk.MenuRemove();
            }
            else
            {
                menuAsk.MenuShow();
            }
        }
    }
     
    //****************************************************************************************************
    void FixedUpdate()
    {
        if (!isGameStarted) return;

        deltaTime = Time.deltaTime;

        if (!menuAsk.isShowed)
        {
            newPointTimeNow += deltaTime;

            if ((newPointTimeNow >= newPointTime) && (score + pointNum < winNum))
            {
                AddPoint(); 
            }
            
            if (Input.touchCount == 1)
            {
                pos.x = beginPosition.x - Input.GetTouch(0).position.x;
                pos.y = beginPosition.y - canvasPoints.rect.height * Input.GetTouch(0).position.y / Screen.height;
                touchDeltaPosition = pos;

                if (touchDeltaPosition.x > 50) { touchDeltaPosition.x = 50; }
                else if (touchDeltaPosition.x < -50) { touchDeltaPosition.x = -50; }
                if (touchDeltaPosition.y > 50) { touchDeltaPosition.y = 50; }
                else if (touchDeltaPosition.y < -50) { touchDeltaPosition.y = -50; }

                player.transform.Translate(-touchDeltaPosition.x * speedMove * deltaTime, -touchDeltaPosition.y * speedMove * deltaTime, 0);

                if (player.anchoredPosition.y > canvasPoints.rect.height - 100) { pos.x = player.anchoredPosition.x; pos.y = canvasPoints.rect.height - 100; player.anchoredPosition = pos; }
                if (player.anchoredPosition.y < 50) { pos.x = player.anchoredPosition.x; pos.y = 50; player.anchoredPosition = pos; }

                if (player.anchoredPosition.x > canvasPoints.rect.x + canvasPoints.rect.width - 50) { pos.x = canvasPoints.rect.x + canvasPoints.rect.width - 50; pos.y = player.anchoredPosition.y; player.anchoredPosition = pos; }
                if (player.anchoredPosition.x < canvasPoints.rect.x + 50) { pos.x = canvasPoints.rect.x + 50; pos.y = player.anchoredPosition.y; player.anchoredPosition = pos; }

                pos.x = -touchDeltaPosition.x;
                pos.y = -touchDeltaPosition.y;
                panelTouch.anchoredPosition = pos;
            }
        }
    }

    //****************************************************************************************************
    public void SoundOn()
    {
        int intS = 0;

        if (soundPlay.isSound)
        {
            soundPlay.isSound = false;
            intS = 1;
            imgBtnSound.sprite = imgSoundOff;
            soundPlay.audioSource.Stop();
        }
        else
        {
            soundPlay.isSound = true;
            intS = 0;
            imgBtnSound.sprite = imgSoundOn;
        }

        PlayerPrefs.SetInt("SoundOn", intS);
    }

    //****************************************************************************************************
    IEnumerator AnimateGameShow()
    {
        while (imgBG.color.a < 1f)
        {
            deltaTime = Time.deltaTime;

            color = imgBG.color; color.a += deltaTime * 0.3f; imgBG.color = color;
            if (imgPanelTouch.color.a < 0.3f) { color = imgPanelTouch.color; color.a += deltaTime * 0.1f; imgPanelTouch.color = color; }
            if (imgPanelTouchBG.color.a < 0.3f) { color = imgPanelTouchBG.color; color.a += deltaTime * 0.1f; imgPanelTouchBG.color = color; }
            color = imgPlayer.color; color.a += deltaTime * 0.4f; imgPlayer.color = color;
            color = imgBtnSound.color; color.a += deltaTime * 0.4f; imgBtnSound.color = color;

            if (gameLifesBG.anchoredPosition.y > -25) { pos.x = 0; pos.y = gameLifesBG.anchoredPosition.y - deltaTime * 50f; gameLifesBG.anchoredPosition = pos; }

            yield return null;
        }

        color = imgBG.color; color.a = 1; imgBG.color = color;
        color = imgPanelTouch.color; color.a = 0.3f; imgPanelTouch.color = color;
        color = imgPanelTouchBG.color; color.a = 0.3f; imgPanelTouchBG.color = color;
        color = imgPlayer.color; color.a = 1; imgPlayer.color = color;
        color = imgBtnSound.color; color.a = 1; imgBtnSound.color = color;

        pos.x = 0;
        pos.y = -25;
        gameLifesBG.anchoredPosition = pos;

        isGameStarted = true;
    }

    //****************************************************************************************************
    IEnumerator AnimateGameRemove()
    {
        deltaTime = Time.deltaTime;

        while (imgBG.color.a > 0.1f)
        {
            deltaTime = Time.deltaTime;

            color = imgBG.color; color.a -= deltaTime * 0.3f; imgBG.color = color;
            color = imgPanelTouch.color; color.a -= deltaTime * 0.4f; imgPanelTouch.color = color;
            color = imgPanelTouchBG.color; color.a -= deltaTime * 0.4f; imgPanelTouchBG.color = color;
            color = imgPlayer.color; color.a -= deltaTime * 0.4f; imgPlayer.color = color;
            color = imgBtnSound.color; color.a -= deltaTime * 0.4f; imgBtnSound.color = color;

            pos.x = 0;
            pos.y = gameLifesBG.anchoredPosition.y + deltaTime * 300f;
            gameLifesBG.anchoredPosition = pos;

            yield return null;
        }

        AsyncOperation loading = SceneManager.LoadSceneAsync(3);
    }

    //****************************************************************************************************
}
