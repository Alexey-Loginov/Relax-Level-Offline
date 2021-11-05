using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStats : MonoBehaviour
{
    //***
    private StatsPlay statsPlay;

    //***
    [Header("Objects")]
    public RectTransform menuRt;

    public RectTransform btnOk;
    public RectTransform btnSet;

    //***
    [Header("Images")]
    public Image imgMenuBG;

    public Image imgStatsPoint;

    public Image imgBtnOk;
    public Image imgBtnSet;

    //***
    [Header("Text")]
    public Text txtGameWin;
    public Text txtGetPoint;
    public Text txtGetGift;

    public Text txtBtnOk;
    public Text txtBtnSet;

    //***
    [Header("Variables")]
    private Vector2 pos;

    //***
    public Color color;

    //***
    private float deltaTime;

    //***
    public bool isShowed;

    //****************************************************************************************************
    void Awake()
    {
        statsPlay = Camera.main.GetComponent<StatsPlay>();

        isShowed = false;
    }

    //****************************************************************************************************
    public void MenuShow()
    {
        pos.x = 10000;
        pos.y = 0;
        btnOk.anchoredPosition = pos;
        btnSet.anchoredPosition = pos;

        color = imgBtnOk.color; color.a = 0; imgBtnOk.color = color;
        color = imgBtnSet.color; color.a = 0; imgBtnSet.color = color;
        color = txtBtnOk.color; color.a = 0; txtBtnOk.color = color;
        color = txtBtnSet.color; color.a = 0; txtBtnSet.color = color;

        imgStatsPoint.sprite = statsPlay.skins.GetSkinPlayer(statsPlay.skins.skinPointNum);
        txtGetPoint.text = statsPlay.getPoint.ToString();
        txtGameWin.text = statsPlay.gameWin.ToString();

        int countToGift = statsPlay.skins.GetCountGift();

        if (countToGift == 0)
        {
            txtGetGift.text = "";
        }
        else
        {
            txtGetGift.text = countToGift.ToString();
        }

        pos.x = 1000;
        pos.y = 0;
        menuRt.anchoredPosition = pos;

        menuRt.localScale = Vector2.one;
        isShowed = true;

        StartCoroutine(AnimateStatsShow());
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
    public void BtnOkClick()
    {
        StartCoroutine(AnimateStatsRemove());
    }

    //****************************************************************************************************
    IEnumerator AnimateStatsShow()
    {
        deltaTime = 0;

        while (menuRt.anchoredPosition.x > 0)
        {
            deltaTime = Time.deltaTime;

            pos = menuRt.anchoredPosition;
            pos.x -= deltaTime * 750f;
            pos.y = 0;
            menuRt.anchoredPosition = pos;

            yield return null;
        }

        StartCoroutine(AddScoreToGift());
    }

    //****************************************************************************************************
    IEnumerator AnimateStatsRemove()
    {
        deltaTime = 0;

        while (menuRt.anchoredPosition.x > -1000)
        {
            deltaTime = Time.deltaTime;

            pos = menuRt.anchoredPosition;
            pos.x -= deltaTime * 500f;
            pos.y = 0;
            menuRt.anchoredPosition = pos;

            yield return null;
        }

        AsyncOperation loading = SceneManager.LoadSceneAsync(1);
    }

    //****************************************************************************************************
    IEnumerator AddScoreToGift()
    {
        float countToGift = statsPlay.skins.GetCountGift();
        int iList = statsPlay.skins.GetListSkins();
        int scoreNow = statsPlay.gameScoreNow;
        float getPointNow = (float)(statsPlay.getPoint - scoreNow);
        bool isSet = false;

        if ((countToGift > 0) && (countToGift - scoreNow <= 0))
        {
            isSet = true;
        }

        deltaTime = 0;

        while (getPointNow < statsPlay.getPoint)
        {
            deltaTime = Time.deltaTime;

            getPointNow += deltaTime * 25f;
            if (countToGift > 0) { countToGift -= deltaTime * 25f; } else { countToGift = 0; }

            txtGetGift.text = string.Format("{0:0}", countToGift);
            txtGetPoint.text = string.Format("{0:0}", getPointNow);

            yield return null;
        }

        countToGift = statsPlay.skins.GetCountGift() - scoreNow;
        if (countToGift < 0) { countToGift = 0; }
        txtGetGift.text = string.Format("{0:0}", countToGift);

        getPointNow = statsPlay.getPoint;
        txtGetPoint.text = string.Format("{0:0}", getPointNow);

        if (isSet)
        {
            pos.x = 0;
            pos.y = -80;
            btnSet.anchoredPosition = pos;

            while (imgBtnSet.color.a < 1)
            {
                deltaTime = Time.deltaTime;

                color = imgBtnSet.color; color.a += deltaTime * 0.4f; imgBtnSet.color = color;
                color = txtBtnSet.color; color.a += deltaTime * 0.4f; txtBtnSet.color = color;

                yield return null;
            }

            color = imgBtnSet.color; color.a = 1; imgBtnSet.color = color;
            color = txtBtnSet.color; color.a = 1; txtBtnSet.color = color;

            PlayerPrefs.SetInt("SetNewSkin", 1);
            PlayerPrefs.SetInt("SetNewSkinList", iList);
            if (iList == 1) PlayerPrefs.SetInt("SetNewSkinNumber", statsPlay.skins.GetNextSkinNumberPoint() + 1);
            else PlayerPrefs.SetInt("SetNewSkinNumber", statsPlay.skins.GetNextSkinNumberBG() + 1);
        }
        else
        {
            pos.x = 0;
            pos.y = -80;
            btnOk.anchoredPosition = pos;

            while (imgBtnOk.color.a < 1)
            {
                deltaTime = Time.deltaTime;

                color = imgBtnOk.color; color.a += deltaTime * 0.4f; imgBtnOk.color = color;
                color = txtBtnOk.color; color.a += deltaTime * 0.4f; txtBtnOk.color = color;

                yield return null;
            }

            color = imgBtnOk.color; color.a = 1; imgBtnOk.color = color;
            color = txtBtnOk.color; color.a = 1; txtBtnOk.color = color;
            
            PlayerPrefs.SetInt("SetNewSkin", 0);
        }

        statsPlay.skins.AddScoreToGift(iList, scoreNow);
    }

    //****************************************************************************************************
}
