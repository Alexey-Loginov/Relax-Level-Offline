using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAsk : MonoBehaviour
{
    //***
    [Header("Objects")]
    public RectTransform menuRt;

    //***
    public Image imgMenuBG;

    //***
    [Header("Variables")]
    private Vector2 pos;
    
    //***
    public bool isShowed;

    //****************************************************************************************************
    void Awake()
    {
        isShowed = false;
    }

    //****************************************************************************************************
    public void MenuShow()
    {
        menuRt.anchoredPosition = Vector2.zero;
        menuRt.localScale = Vector2.one;
        isShowed = true;
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
    public void BtnOkClick()
    {
        Application.Quit();
    }

    //****************************************************************************************************
}
