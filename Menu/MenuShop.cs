using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class MenuShop : MonoBehaviour
{
    //***
    private MenuPlay menuPlay;

    //***
    [Header("Objects")]
    public RectTransform menu;

    public RectTransform btnLeft;
    public RectTransform btnRight;
    public RectTransform btnShopPoint;
    public RectTransform btnShopBG;

    public RectTransform btnBuyBG_003;
    public RectTransform btnBuyBG_006;
    public RectTransform btnBuyPoint_003;
    public RectTransform btnBuyPoint_006;
    public RectTransform btnSet;
    public RectTransform imgLock;

    public RectTransform chooses;

    public RectTransform chooseSmall_01;
    public RectTransform chooseSmall_02;
    public RectTransform chooseSmall_03;
    public RectTransform chooseSmall_04;
    public RectTransform chooseSmall_05;
    public RectTransform chooseSmall_06;

    //***
    [Header("Images")]
    public Image imgChoose;

    public Image imgChooseSmall_01;
    public Image imgChooseSmall_02;
    public Image imgChooseSmall_03;
    public Image imgChooseSmall_04;
    public Image imgChooseSmall_05;
    public Image imgChooseSmall_06;

    //***
    [Header("Variables")]
    private Color color;

    //***
    private Vector2 pos;

    //***
    private float deltaTime;
    private float timeMove;

    private float moveBeginPosition;
    private float moveEndPosition;

    //***
    public int newSkin;
    public int idShop;

    //***
    public bool isShowed;
    private bool isMoved;

    //****************************************************************************************************
    void Awake()
    {
        menuPlay = Camera.main.GetComponent<MenuPlay>();

        MenuRemove();
    }

    //****************************************************************************************************
    void Start()
    {
        if (PlayerPrefs.GetInt("SetNewSkin") == 1)
        {
            if (PlayerPrefs.GetInt("SetNewSkinList") == 1)
            {
                MenuShow();
                GetShopPoint();
                newSkin = PlayerPrefs.GetInt("SetNewSkinNumber");
                UpdateButtons(newSkin);
                StartCoroutine(MoveImgChooseSmall());
            }
            else
            {
                MenuShow();
                GetShopBG();
                newSkin = PlayerPrefs.GetInt("SetNewSkinNumber");
                UpdateButtons(newSkin);
                StartCoroutine(MoveImgChooseSmall());
            }
        }

        PlayerPrefs.SetInt("SetNewSkin", 0);
    }

    //****************************************************************************************************
    public void MenuShow()
    {
        menu.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("SetNewSkin") == 0)
        {
            newSkin = menuPlay.skins.skinPointNum;
        }
        
        GetShopPoint();

        btnLeft.gameObject.SetActive(true);
        btnRight.gameObject.SetActive(true);
        
        if (newSkin <= 1)
        {
            btnLeft.gameObject.SetActive(false);
        }
        else if (newSkin >= 6)
        {
            btnRight.gameObject.SetActive(false);
        }

        SetSkin();
        UpdateButtons(newSkin);

        isShowed = true;
    }

    //****************************************************************************************************
    public void MenuRemove()
    {
        menu.gameObject.SetActive(false);

        isShowed = false;
    }

    //****************************************************************************************************
    public void GetShopPoint()
    {
        color = btnShopPoint.GetComponent<Image>().color;
        color.a = 0.5f;
        btnShopPoint.GetComponent<Image>().color = color;

        color = btnShopBG.GetComponent<Image>().color;
        color.a = 1;
        btnShopBG.GetComponent<Image>().color = color;

        newSkin = menuPlay.skins.skinPointNum;
        imgChoose.sprite = menuPlay.skins.GetSkinBGPoint(newSkin);

        imgChooseSmall_01.sprite = menuPlay.skins.GetSkinBGPoint(1);
        imgChooseSmall_02.sprite = menuPlay.skins.GetSkinBGPoint(2);
        imgChooseSmall_03.sprite = menuPlay.skins.GetSkinBGPoint(3);
        imgChooseSmall_04.sprite = menuPlay.skins.GetSkinBGPoint(4);
        imgChooseSmall_05.sprite = menuPlay.skins.GetSkinBGPoint(5);
        imgChooseSmall_06.sprite = menuPlay.skins.GetSkinBGPoint(6);

        idShop = 1;
        UpdateButtons(newSkin);
        StartCoroutine(MoveImgChooseSmall());
    }

    public void GetShopBG()
    {
        color = btnShopBG.GetComponent<Image>().color;
        color.a = 0.5f;
        btnShopBG.GetComponent<Image>().color = color;

        color = btnShopPoint.GetComponent<Image>().color;
        color.a = 1;
        btnShopPoint.GetComponent<Image>().color = color;

        newSkin = menuPlay.skins.skinBGNum;
        imgChoose.sprite = menuPlay.skins.GetSkinBG(newSkin);

        imgChooseSmall_01.sprite = menuPlay.skins.GetSkinBG(1);
        imgChooseSmall_02.sprite = menuPlay.skins.GetSkinBG(2);
        imgChooseSmall_03.sprite = menuPlay.skins.GetSkinBG(3);
        imgChooseSmall_04.sprite = menuPlay.skins.GetSkinBG(4);
        imgChooseSmall_05.sprite = menuPlay.skins.GetSkinBG(5);
        imgChooseSmall_06.sprite = menuPlay.skins.GetSkinBG(6);

        idShop = 2;
        UpdateButtons(newSkin);
        StartCoroutine(MoveImgChooseSmall());
    }

    //****************************************************************************************************
    public void BtnLeftClick()
    {
        newSkin--;

        if (idShop == 1)
        {
            imgChoose.sprite = menuPlay.skins.GetSkinBGPoint(newSkin);
        }
        else if (idShop == 2)
        {
            imgChoose.sprite = menuPlay.skins.GetSkinBG(newSkin);
        }

        btnRight.gameObject.SetActive(true);

        if (newSkin <= 1)
        {
            btnLeft.gameObject.SetActive(false);
        }
        else
        {
            btnLeft.gameObject.SetActive(true);
        }

        UpdateButtons(newSkin);
        StartCoroutine(MoveImgChooseSmall());
    }

    public void BtnRightClick()
    {
        newSkin++;

        if (idShop == 1)
        {
            imgChoose.sprite = menuPlay.skins.GetSkinBGPoint(newSkin);
        }
        else if (idShop == 2)
        {
            imgChoose.sprite = menuPlay.skins.GetSkinBG(newSkin);
        }

        btnLeft.gameObject.SetActive(true);

        if (newSkin >= 6)
        {
            btnRight.gameObject.SetActive(false);
        }
        else if (newSkin <= 1)
        {
            btnRight.gameObject.SetActive(true);
        }

        UpdateButtons(newSkin);
        StartCoroutine(MoveImgChooseSmall());
    }

    //****************************************************************************************************
    public void UpdateButtons(int idSkin)
    {
        btnBuyPoint_003.gameObject.SetActive(false);
        btnBuyPoint_006.gameObject.SetActive(false);
        btnBuyBG_003.gameObject.SetActive(false);
        btnBuyBG_006.gameObject.SetActive(false);
        btnSet.gameObject.SetActive(false);
        imgLock.gameObject.SetActive(false);

        if (idSkin >= 6)
        {
            btnLeft.gameObject.SetActive(true);
            btnRight.gameObject.SetActive(false);
        }
        else if (idSkin <= 1)
        {
            btnLeft.gameObject.SetActive(false);
            btnRight.gameObject.SetActive(true);
        }

        if (idShop == 1)
        {
            if (menuPlay.skins.skinPointNum == idSkin)
            {
                return;
            }

            if (!menuPlay.skins.skinsPoint[idSkin - 1].isBought)
            {
                if (newSkin == 3) btnBuyPoint_003.gameObject.SetActive(true);
                else if (newSkin == 6) btnBuyPoint_006.gameObject.SetActive(true);
                return;
            }

            if (!menuPlay.skins.skinsPoint[idSkin - 1].isClose)
            {
                btnSet.gameObject.SetActive(true);
                return;
            }

            imgLock.gameObject.SetActive(true);
        }
        else if (idShop == 2)
        {
            if (menuPlay.skins.skinBGNum == idSkin)
            {
                return;
            }

            if (!menuPlay.skins.skinsBG[idSkin - 1].isBought)
            {
                if (newSkin == 3) btnBuyBG_003.gameObject.SetActive(true);
                else if (newSkin == 6) btnBuyBG_006.gameObject.SetActive(true);
                return;
            }

            if (!menuPlay.skins.skinsBG[idSkin - 1].isClose)
            {
                btnSet.gameObject.SetActive(true);
                return;
            }

            imgLock.gameObject.SetActive(true);
        }
    }

    //****************************************************************************************************
    public void OnPurchaseComplete(Product product)
    {
        BuySkin();
        
        Debug.Log("Purchase done");
    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of product failed");
    }

    //****************************************************************************************************
    public void BuySkin()
    {
        if (idShop == 1)
        {
            menuPlay.skins.BuySkinPoint(newSkin);
            menuPlay.skins.SetSkinPoint(newSkin);
        }
        else if (idShop == 2)
        {
            menuPlay.skins.BuySkinBG(newSkin);
            menuPlay.skins.SetSkinBG(newSkin);
        }

        btnShopPoint.GetComponent<Image>().sprite = menuPlay.skins.GetSkinPlayer(menuPlay.skins.skinPointNum);
        btnShopBG.GetComponent<Image>().sprite = menuPlay.skins.GetSkinBG(menuPlay.skins.skinBGNum);
        
        UpdateButtons(newSkin);
    }

    public void SetSkin()
    {
        if (idShop == 1)
        {
            menuPlay.skins.SetSkinPoint(newSkin);
        }
        else if (idShop == 2)
        {
            menuPlay.skins.SetSkinBG(newSkin);
        }

        btnShopPoint.GetComponent<Image>().sprite = menuPlay.skins.GetSkinPlayer(menuPlay.skins.skinPointNum);
        btnShopBG.GetComponent<Image>().sprite = menuPlay.skins.GetSkinBG(menuPlay.skins.skinBGNum);

        UpdateButtons(newSkin);
    }

    //****************************************************************************************************
    IEnumerator MoveImgChooseSmall()
    {
        isMoved = false;
        deltaTime = Time.deltaTime;
        timeMove = 0;
        moveBeginPosition = chooses.anchoredPosition.x;
        moveEndPosition = (newSkin - 1) * -250;

        while (!isMoved)
        {
            deltaTime = Time.deltaTime;
            timeMove += deltaTime;

            pos.x = Mathf.Lerp(moveBeginPosition, moveEndPosition, timeMove);
            pos.y = chooses.anchoredPosition.y;
            chooses.anchoredPosition = pos;

            if (newSkin == 1)
            {
                MaximazeImage(chooseSmall_01, deltaTime);

                MinimazeImage(chooseSmall_02, deltaTime);
                MinimazeImage(chooseSmall_03, deltaTime);
                MinimazeImage(chooseSmall_04, deltaTime);
                MinimazeImage(chooseSmall_05, deltaTime);
                MinimazeImage(chooseSmall_06, deltaTime);
            }
            else if (newSkin == 2)
            {
                MaximazeImage(chooseSmall_02, deltaTime);

                MinimazeImage(chooseSmall_01, deltaTime);
                MinimazeImage(chooseSmall_03, deltaTime);
                MinimazeImage(chooseSmall_04, deltaTime);
                MinimazeImage(chooseSmall_05, deltaTime);
                MinimazeImage(chooseSmall_06, deltaTime);
            }
            else if (newSkin == 3)
            {
                MaximazeImage(chooseSmall_03, deltaTime);

                MinimazeImage(chooseSmall_01, deltaTime);
                MinimazeImage(chooseSmall_02, deltaTime);
                MinimazeImage(chooseSmall_04, deltaTime);
                MinimazeImage(chooseSmall_05, deltaTime);
                MinimazeImage(chooseSmall_06, deltaTime);
            }
            else if (newSkin == 4)
            {
                MaximazeImage(chooseSmall_04, deltaTime);

                MinimazeImage(chooseSmall_01, deltaTime);
                MinimazeImage(chooseSmall_02, deltaTime);
                MinimazeImage(chooseSmall_03, deltaTime);
                MinimazeImage(chooseSmall_05, deltaTime);
                MinimazeImage(chooseSmall_06, deltaTime);
            }
            else if (newSkin == 5)
            {
                MaximazeImage(chooseSmall_05, deltaTime);

                MinimazeImage(chooseSmall_01, deltaTime);
                MinimazeImage(chooseSmall_02, deltaTime);
                MinimazeImage(chooseSmall_03, deltaTime);
                MinimazeImage(chooseSmall_04, deltaTime);
                MinimazeImage(chooseSmall_06, deltaTime);
            }
            else if (newSkin == 6)
            {
                MaximazeImage(chooseSmall_06, deltaTime);

                MinimazeImage(chooseSmall_01, deltaTime);
                MinimazeImage(chooseSmall_02, deltaTime);
                MinimazeImage(chooseSmall_03, deltaTime);
                MinimazeImage(chooseSmall_04, deltaTime);
                MinimazeImage(chooseSmall_05, deltaTime);
            }

            if (chooses.anchoredPosition.x == moveEndPosition)
            {
                isMoved = true;
            }

            yield return null;
        }
    }

    private void MinimazeImage(RectTransform img, float dt)
    {
        pos.x = img.localScale.x - dt * 0.5f;
        pos.y = img.localScale.y - dt * 0.5f;
        img.localScale = pos;

        if (img.localScale.x < 1)
        {
            img.localScale = Vector2.one;
        }
    }

    private void MaximazeImage(RectTransform img, float dt)
    {
        pos.x = img.localScale.x + dt * 0.5f;
        pos.y = img.localScale.y + dt * 0.5f;
        img.localScale = pos;

        if (img.localScale.x > 1.5f)
        {
            pos.x = 1.5f;
            pos.y = 1.5f;
            img.localScale = pos;
        }
    }

    //****************************************************************************************************

    //****************************************************************************************************
}
