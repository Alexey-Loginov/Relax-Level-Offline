using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    //***
    private MenuPlay menuPlay;

    //***
    [Header("Objects")]
    public RectTransform menuRt;

    public RectTransform panelLeft;
    public RectTransform panelRight;
    public RectTransform panelUp;
    public RectTransform panelDown;

    //***
    [Header("Images")]
    public Image imgMenuBG;

    public Image imgBtnPlay;
    public Image imgBtnSound;

    //***
    public Sprite imgSoundOn;
    public Sprite imgSoundOff;

    //***
    [Header("Text")]
    public Text txtEasy;
    public Text txtMedium;
    public Text txtHard;

    //***
    [Header("Variables")]
    private Vector2 pos;
    
    //***
    private Color color;

    //***
    private float deltaTime;

    //***
    public bool isShowed;

    //****************************************************************************************************
    void Awake()
    {
        menuPlay = Camera.main.GetComponent<MenuPlay>();

        isShowed = false;
    }

    //****************************************************************************************************
    public void MenuShow()
    {
        if (PlayerPrefs.GetInt("LessonEnd") == 1)
        {
            panelRight.gameObject.SetActive(true);
        }
        else
        {
            panelRight.gameObject.SetActive(false);
        }

        color = imgBtnPlay.color;
        color.a = 0;
        imgBtnPlay.color = color;

        imgMenuBG.sprite = menuPlay.skins.GetSkinBG(menuPlay.skins.skinBGNum);

        if (menuPlay.soundPlay.isSound) imgBtnSound.sprite = imgSoundOn;
        else imgBtnSound.sprite = imgSoundOff;

        color = txtEasy.color;
        color.a = 0.5f;
        txtEasy.color = color;
        txtMedium.color = color;
        txtHard.color = color;

        color.r = 1;
        color.g = 1;
        color.b = 1;
        color.a = 1;
        if (menuPlay.gameDifficulty == 1) txtEasy.color = color;
        else if (menuPlay.gameDifficulty == 2) txtMedium.color = color;
        else if (menuPlay.gameDifficulty == 3) txtHard.color = color;

        menuRt.anchoredPosition = Vector2.zero;
        menuRt.localScale = Vector2.one;
        isShowed = true;

        pos.x = -500;
        pos.y = 0;
        panelLeft.anchoredPosition = pos;
        pos.x = 500;
        pos.y = 0;
        panelRight.anchoredPosition = pos;
        pos.x = 0;
        pos.y = 500;
        panelUp.anchoredPosition = pos;
        pos.x = 0;
        pos.y = -500;
        panelDown.anchoredPosition = pos;

        StartCoroutine(AnimateMenuShow());
    }

    //****************************************************************************************************
    public void MenuRemove()
    {
        pos.x = 10000;
        pos.y = 0;
        menuRt.anchoredPosition = pos;
        isShowed = false;
    }

    //****************************************************************************************************
    public void BtnCancelClick()
    {
        MenuRemove();
    }

    //****************************************************************************************************
    public void BtnPlayClick()
    {
        isShowed = false;

        StartCoroutine(AnimateMenuRemove());
    }

    //****************************************************************************************************
    public void BtnPanelMainEasyClick()
    {
        menuPlay.gameDifficulty = 1;
        color.r = 1; color.g = 1; color.b = 1; color.a = 1;
        txtEasy.color = color;
        color.r = 1; color.g = 1; color.b = 1; color.a = 0.5f;
        txtMedium.color = color;
        color.r = 1; color.g = 1; color.b = 1; color.a = 0.5f;
        txtHard.color = color;
        PlayerPrefs.SetInt("GameDifficulty", 1);
    }

    public void BtnPanelMainMediumClick()
    {
        menuPlay.gameDifficulty = 2;
        color.r = 1; color.g = 1; color.b = 1; color.a = 0.5f;
        txtEasy.color = color;
        color.r = 1; color.g = 1; color.b = 1; color.a = 1;
        txtMedium.color = color;
        color.r = 1; color.g = 1; color.b = 1; color.a = 0.5f;
        txtHard.color = color;
        PlayerPrefs.SetInt("GameDifficulty", 2);
    }

    public void BtnPanelMainHardClick()
    {
        menuPlay.gameDifficulty = 3;
        color.r = 1; color.g = 1; color.b = 1; color.a = 0.5f;
        txtEasy.color = color;
        color.r = 1; color.g = 1; color.b = 1; color.a = 0.5f;
        txtMedium.color = color;
        color.r = 1; color.g = 1; color.b = 1; color.a = 1;
        txtHard.color = color;
        PlayerPrefs.SetInt("GameDifficulty", 3);
    }

    //****************************************************************************************************
    public void SoundOn()
    {
        int intS = 0;

        if (menuPlay.soundPlay.isSound)
        {
            menuPlay.soundPlay.isSound = false;
            intS = 1;
            imgBtnSound.sprite = imgSoundOff;
            menuPlay.soundPlay.audioSource.Stop();
        }
        else
        {
            menuPlay.soundPlay.isSound = true;
            intS = 0;
            imgBtnSound.sprite = imgSoundOn;
        }

        PlayerPrefs.SetInt("SoundOn", intS);
    }

    //****************************************************************************************************
    IEnumerator AnimateMenuShow()
    {
        while ((imgBtnPlay.color.a < 1f) && (isShowed))
        {
            deltaTime = Time.deltaTime;

            color = imgBtnPlay.color;
            color.a += deltaTime * 0.5f;
            imgBtnPlay.color = color;

            if (panelLeft.anchoredPosition.x >= 0) { panelLeft.anchoredPosition = Vector2.zero; }
            else { pos = panelLeft.anchoredPosition; pos.x += deltaTime * 300f; panelLeft.anchoredPosition = pos; }

            if (panelRight.anchoredPosition.x <= 0) { panelRight.anchoredPosition = Vector2.zero; }
            else { pos = panelRight.anchoredPosition; pos.x -= deltaTime * 300f; panelRight.anchoredPosition = pos; }

            if (panelUp.anchoredPosition.y <= 0) { panelUp.anchoredPosition = Vector2.zero; }
            else { pos = panelUp.anchoredPosition; pos.y -= deltaTime * 300f; panelUp.anchoredPosition = pos; }

            if (panelDown.anchoredPosition.y >= 0) { panelDown.anchoredPosition = Vector2.zero; }
            else { pos = panelDown.anchoredPosition; pos.y += deltaTime * 300f; panelDown.anchoredPosition = pos; }

            yield return null;
        }

        if (isShowed)
        {
            color = imgBtnPlay.color;
            color.a = 1f;
            imgBtnPlay.color = color;

            panelLeft.anchoredPosition = Vector2.zero;
            panelRight.anchoredPosition = Vector2.zero;
            panelUp.anchoredPosition = Vector2.zero;
            panelDown.anchoredPosition = Vector2.zero;
        }
    }

    //****************************************************************************************************
    IEnumerator AnimateMenuRemove()
    {
        while (imgBtnPlay.color.a > 0.1f)
        {
            deltaTime = Time.deltaTime;

            color = imgBtnPlay.color;
            color.a -= deltaTime * 0.3f;
            imgBtnPlay.color = color;

            pos = panelLeft.anchoredPosition; pos.x -= deltaTime * 300f; panelLeft.anchoredPosition = pos;
            pos = panelRight.anchoredPosition; pos.x += deltaTime * 300f; panelRight.anchoredPosition = pos;
            pos = panelUp.anchoredPosition; pos.y += deltaTime * 300f; panelUp.anchoredPosition = pos;
            pos = panelDown.anchoredPosition; pos.y -= deltaTime * 300f; panelDown.anchoredPosition = pos;

            yield return null;
        }

        color = imgBtnPlay.color;
        color.a = 0;
        imgBtnPlay.color = color;

        pos.x = -500; pos.y = 0; panelLeft.anchoredPosition = pos;
        pos.x = 500; pos.y = 0; panelRight.anchoredPosition = pos;
        pos.x = 0; pos.y = 500; panelUp.anchoredPosition = pos;
        pos.x = 0; pos.y = -500; panelDown.anchoredPosition = pos;

        if (PlayerPrefs.GetInt("LessonEnd") == 1) { AsyncOperation loading = SceneManager.LoadSceneAsync(2); }
        else { AsyncOperation loading = SceneManager.LoadSceneAsync(4); }
    }

    //****************************************************************************************************
}
