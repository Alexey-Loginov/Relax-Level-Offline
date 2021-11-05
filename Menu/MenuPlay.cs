using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlay : MonoBehaviour
{
    //***
    [Header("Objects")]
    public Skins            skins;
    public SoundPlay        soundPlay;

    private MenuAsk         menuAsk;
    private MenuLicense     menuLicense;
    public  MenuMain        menuMain;
    private MenuShop        menuShop;

    //***
    [Header("Variables")]
    public int gameDifficulty;

    //****************************************************************************************************
    void Awake()
    {
        skins = Camera.main.GetComponent<Skins>();
        soundPlay = Camera.main.GetComponent<SoundPlay>();

        menuMain = Camera.main.GetComponent<MenuMain>();
        menuAsk = Camera.main.GetComponent<MenuAsk>();
        menuLicense = Camera.main.GetComponent<MenuLicense>();
        menuShop = Camera.main.GetComponent<MenuShop>();

        gameDifficulty = PlayerPrefs.GetInt("GameDifficulty");
        if (gameDifficulty == 0)
        {
            gameDifficulty = 1;
            PlayerPrefs.SetInt("GameDifficulty", 1);

            skins.DefaultListSkins();
        }
    }

    //****************************************************************************************************
    void Start()
    {
        skins.LoadSkins();

        menuAsk.MenuRemove();
        menuLicense.MenuRemove();
        
        menuMain.MenuShow();
    }

    //****************************************************************************************************
    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
        {
            if (menuAsk.isShowed)
            {
                menuAsk.MenuRemove();
            }
            else if (menuLicense.isShowed)
            {
                menuLicense.MenuRemove();
            }
            else if (menuShop.isShowed)
            {
                menuShop.MenuRemove();
            }
            else
            {
                menuAsk.MenuShow();
            }
        }
    }
    //****************************************************************************************************
}
