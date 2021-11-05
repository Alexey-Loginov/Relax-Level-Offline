using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlay : MonoBehaviour
{
    //***
    [Header("Objects")]
    public Skins skins;
    public SoundPlay soundPlay;

    public MenuStats menuStats;

    //***
    [Header("Variables")]
    public int gameWinNow;
    public int gameScoreNow;

    public int gameWin;
    public int getPoint;

    //****************************************************************************************************
    void Awake()
    {
        skins = Camera.main.GetComponent<Skins>();
        soundPlay = Camera.main.GetComponent<SoundPlay>();

        menuStats = Camera.main.GetComponent<MenuStats>();

        gameWinNow = PlayerPrefs.GetInt("StatsWin");
        gameScoreNow = PlayerPrefs.GetInt("StatsScore");

        gameWin = PlayerPrefs.GetInt("GameWin");
        getPoint = PlayerPrefs.GetInt("GetPoint");
    }

    //****************************************************************************************************
    void Start()
    {
        menuStats.MenuShow();

        menuStats.txtGameWin.text = gameWin.ToString();
        menuStats.txtGetPoint.text = getPoint.ToString();
    }

    //****************************************************************************************************
}
