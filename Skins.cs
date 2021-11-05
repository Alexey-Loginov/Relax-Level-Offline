using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : MonoBehaviour
{
    //***
    [Header("Images")]
    public Sprite skinBG_001;
    public Sprite skinBG_002;
    public Sprite skinBG_003;
    public Sprite skinBG_004;
    public Sprite skinBG_005;
    public Sprite skinBG_006;

    //***
    public Sprite skinPlayer_001;
    public Sprite skinPlayer_002;
    public Sprite skinPlayer_003;
    public Sprite skinPlayer_004;
    public Sprite skinPlayer_005;
    public Sprite skinPlayer_006;

    //***
    public Sprite skinBGPoint_001;
    public Sprite skinPoint_001_01;
    public Sprite skinPoint_001_02;
    public Sprite skinPoint_001_03;
    public Sprite skinPoint_001_04;
    public Sprite skinPoint_001_05;

    public Sprite skinBGPoint_002;
    public Sprite skinPoint_002_01;
    public Sprite skinPoint_002_02;
    public Sprite skinPoint_002_03;
    public Sprite skinPoint_002_04;
    public Sprite skinPoint_002_05;

    public Sprite skinBGPoint_003;
    public Sprite skinPoint_003_01;
    public Sprite skinPoint_003_02;
    public Sprite skinPoint_003_03;
    public Sprite skinPoint_003_04;
    public Sprite skinPoint_003_05;

    public Sprite skinBGPoint_004;
    public Sprite skinPoint_004_01;
    public Sprite skinPoint_004_02;
    public Sprite skinPoint_004_03;
    public Sprite skinPoint_004_04;
    public Sprite skinPoint_004_05;

    public Sprite skinBGPoint_005;
    public Sprite skinPoint_005_01;
    public Sprite skinPoint_005_02;
    public Sprite skinPoint_005_03;
    public Sprite skinPoint_005_04;
    public Sprite skinPoint_005_05;

    public Sprite skinBGPoint_006;
    public Sprite skinPoint_006_01;
    public Sprite skinPoint_006_02;
    public Sprite skinPoint_006_03;
    public Sprite skinPoint_006_04;
    public Sprite skinPoint_006_05;

    //***
    [Header("Arrays")]
    public List<int> skinsBGCan = new List<int>();
    public List<int> skinsBGBuy = new List<int>();

    public List<int> skinsPointCan = new List<int>();
    public List<int> skinsPointBuy = new List<int>();

    public List<Skin> skinsBG = new List<Skin>();
    public List<Skin> skinsPoint = new List<Skin>();

    //***
    [Header("Variables")]
    private int id;

    public int skinPointNum;
    public int skinBGNum;

    //****************************************************************************************************
    void Awake()
    {
        skinPointNum = PlayerPrefs.GetInt("SkinPointNum");
        if (skinPointNum == 0) { skinPointNum = 1; }
        skinBGNum = PlayerPrefs.GetInt("SkinBGNum");
        if (skinBGNum == 0) { skinBGNum = 1; }

        LoadSkins();
    }

    //****************************************************************************************************
    public void LoadSkins()
    {
        string strLoad;
        string[] arrStr;
        string[] arrItem;
        Skin item;

        strLoad = PlayerPrefs.GetString("SkinsBG");
        if (strLoad == "")
        {
            DefaultListSkins();
        }
        else
        {
            skinsBG.Clear();
            arrStr = strLoad.Split('\n');

            foreach (string str in arrStr)
            {
                if (str == "") { continue; }

                arrItem = str.Split(';');

                item = new Skin();
                item.id = int.Parse(arrItem[0]);
                item.countToGift = int.Parse(arrItem[1]);
                item.countToGiftNow = int.Parse(arrItem[2]);

                if (arrItem[3] == "1") item.isClose = true;
                else item.isClose = false;

                if (arrItem[4] == "1") item.isBought = true;
                else item.isBought = false;

                skinsBG.Add(item);
            }
        }

        strLoad = PlayerPrefs.GetString("SkinsPoint");
        if (strLoad == "")
        {
            DefaultListSkins();
        }
        else
        {
            skinsPoint.Clear();
            arrStr = strLoad.Split('\n');

            foreach (string str in arrStr)
            {
                if (str == "") { continue; }

                arrItem = str.Split(';');

                item = new Skin();
                item.id = int.Parse(arrItem[0]);
                item.countToGift = int.Parse(arrItem[1]);
                item.countToGiftNow = int.Parse(arrItem[2]);

                if (arrItem[3] == "1") item.isClose = true;
                else item.isClose = false;

                if (arrItem[4] == "1") item.isBought = true;
                else item.isBought = false;

                skinsPoint.Add(item);
            }
        }
    }

    //****************************************************************************************************
    public void SaveSkins()
    {
        string strLoad = "";

        foreach (Skin item in skinsBG)
        {
            strLoad += item.id.ToString() + ";";
            strLoad += item.countToGift.ToString() + ";";
            strLoad += item.countToGiftNow.ToString() + ";";

            if (item.isClose) strLoad += "1;";
            else strLoad += "0;";

            if (item.isBought) strLoad += "1\n";
            else strLoad += "0\n";
        }
        PlayerPrefs.SetString("SkinsBG", strLoad);

        strLoad = "";

        foreach (Skin item in skinsPoint)
        {
            strLoad += item.id.ToString() + ";";
            strLoad += item.countToGift.ToString() + ";";
            strLoad += item.countToGiftNow.ToString() + ";";

            if (item.isClose) strLoad += "1;";
            else strLoad += "0;";

            if (item.isBought) strLoad += "1\n";
            else strLoad += "0\n";
        }
        PlayerPrefs.SetString("SkinsPoint", strLoad);

        PlayerPrefs.SetInt("SkinPointNum", skinPointNum);
        PlayerPrefs.SetInt("SkinBGNum", skinBGNum);
    }

    //****************************************************************************************************
    public void SetSkinBG(int idSkin)
    {
        if (skinsBG[idSkin - 1] == null) return;
        if (skinsBG[idSkin - 1].isClose) return;
        if (!skinsBG[idSkin - 1].isBought) return;

        skinBGNum = idSkin;

        SaveSkins();
    }

    public void BuySkinBG(int idSkin)
    {
        if (skinsBG[idSkin - 1] == null) return;
        if (!skinsBG[idSkin - 1].isClose) return;
        if (skinsBG[idSkin - 1].isBought) return;

        skinsBG[idSkin - 1].isClose = false;
        skinsBG[idSkin - 1].isBought = true;

        skinBGNum = idSkin;

        SaveSkins();
    }

    //****************************************************************************************************
    public void SetSkinPoint(int idSkin)
    {
        if (skinsPoint[idSkin - 1] == null) return;
        if (skinsPoint[idSkin - 1].isClose) return;
        if (!skinsPoint[idSkin - 1].isBought) return;

        skinPointNum = idSkin;

        SaveSkins();
    }

    public void BuySkinPoint(int idSkin)
    {
        if (skinsPoint[idSkin - 1] == null) return;
        if (!skinsPoint[idSkin - 1].isClose) return;
        if (skinsPoint[idSkin - 1].isBought) return;

        skinsPoint[idSkin - 1].isClose = false;
        skinsPoint[idSkin - 1].isBought = true;

        skinPointNum = idSkin;

        SaveSkins();
    }

    //****************************************************************************************************
    public void AddScoreToGift(int iList, int scoreNow)
    {
        int i;

        if (iList == 1)
        {
            i = GetNextSkinNumberPoint();
            skinsPoint[i].countToGiftNow += scoreNow;

            if (skinsPoint[i].countToGiftNow >= skinsPoint[i].countToGift)
            {
                skinsPoint[i].countToGiftNow = 0;
                skinsPoint[i].countToGift = 0;
                skinsPoint[i].isClose = false;
            }
        }
        else if (iList == 2)
        {
            i = GetNextSkinNumberBG();
            skinsBG[i].countToGiftNow += scoreNow;

            if (skinsBG[i].countToGiftNow >= skinsBG[i].countToGift)
            {
                skinsBG[i].countToGiftNow = 0;
                skinsBG[i].countToGift = 0;
                skinsBG[i].isClose = false;
            }
        }

        SaveSkins();
    }

    public int GetCountGift()
    {
        int iList = GetListSkins();

        if (iList == 1)
        {
            return skinsPoint[GetNextSkinNumberPoint()].countToGift - skinsPoint[GetNextSkinNumberPoint()].countToGiftNow;
        }
        else if (iList == 2)
        {
            return skinsBG[GetNextSkinNumberBG()].countToGift - skinsBG[GetNextSkinNumberBG()].countToGiftNow;
        }
        else
        {
            return 0;
        }
    }

    public int GetListSkins()
    {
        int iBG = GetNextSkinNumberBG();
        int iPoint = GetNextSkinNumberPoint();

        if ((iBG == 0) && (iPoint == 0))
        {
            return 0;
        }

        if (iPoint <= iBG)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public int GetNextSkinNumberBG()
    {
        for (int i = 0; i < skinsBG.Count; i++)
        {
            if (!skinsBG[i].isBought) continue;
            if (skinsBG[i].isClose) return i;
        }

        return 0;
    }

    public int GetNextSkinNumberPoint()
    {
        for (int i = 0; i < skinsPoint.Count; i++)
        {
            if (!skinsPoint[i].isBought) continue;
            if (skinsPoint[i].isClose) return i;
        }

        return 0;
    }

    //****************************************************************************************************
    public Sprite GetSkinPoint(int idSkin)
    {
        id = Random.Range(1, 6);

        if (idSkin == 1)
        {
            if (id == 1) { return skinPoint_001_01; }
            else if (id == 2) { return skinPoint_001_02; }
            else if (id == 3) { return skinPoint_001_03; }
            else if (id == 4) { return skinPoint_001_04; }
            else if (id == 5) { return skinPoint_001_05; }
        }
        else if (idSkin == 2)
        {
            if (id == 1) { return skinPoint_002_01; }
            else if (id == 2) { return skinPoint_002_02; }
            else if (id == 3) { return skinPoint_002_03; }
            else if (id == 4) { return skinPoint_002_04; }
            else if (id == 5) { return skinPoint_002_05; }
        }
        else if (idSkin == 3)
        {
            if (id == 1) { return skinPoint_003_01; }
            else if (id == 2) { return skinPoint_003_02; }
            else if (id == 3) { return skinPoint_003_03; }
            else if (id == 4) { return skinPoint_003_04; }
            else if (id == 5) { return skinPoint_003_05; }
        }
        else if (idSkin == 4)
        {
            if (id == 1) { return skinPoint_004_01; }
            else if (id == 2) { return skinPoint_004_02; }
            else if (id == 3) { return skinPoint_004_03; }
            else if (id == 4) { return skinPoint_004_04; }
            else if (id == 5) { return skinPoint_004_05; }
        }
        else if (idSkin == 5)
        {
            if (id == 1) { return skinPoint_005_01; }
            else if (id == 2) { return skinPoint_005_02; }
            else if (id == 3) { return skinPoint_005_03; }
            else if (id == 4) { return skinPoint_005_04; }
            else if (id == 5) { return skinPoint_005_05; }
        }
        else if (idSkin == 6)
        {
            if (id == 1) { return skinPoint_006_01; }
            else if (id == 2) { return skinPoint_006_02; }
            else if (id == 3) { return skinPoint_006_03; }
            else if (id == 4) { return skinPoint_006_04; }
            else if (id == 5) { return skinPoint_006_05; }
        }

        return skinPoint_001_01;
    }

    //****************************************************************************************************
    public Sprite GetSkinPlayer(int idSkin)
    {
        if (idSkin == 1) { return skinPlayer_001; }
        else if (idSkin == 2) { return skinPlayer_002; }
        else if (idSkin == 3) { return skinPlayer_003; }
        else if (idSkin == 4) { return skinPlayer_004; }
        else if (idSkin == 5) { return skinPlayer_005; }
        else if (idSkin == 6) { return skinPlayer_006; }

        return skinPlayer_001;
    }

    //****************************************************************************************************
    public Sprite GetSkinBG(int idSkin)
    {
        if (idSkin == 1) { return skinBG_001; }
        else if (idSkin == 2) { return skinBG_002; }
        else if (idSkin == 3) { return skinBG_003; }
        else if (idSkin == 4) { return skinBG_004; }
        else if (idSkin == 5) { return skinBG_005; }
        else if (idSkin == 6) { return skinBG_006; }

        return skinBG_001;
    }

    //****************************************************************************************************
    public Sprite GetSkinBGPoint(int idSkin)
    {
        if (idSkin == 1) { return skinBGPoint_001; }
        else if (idSkin == 2) { return skinBGPoint_002; }
        else if (idSkin == 3) { return skinBGPoint_003; }
        else if (idSkin == 4) { return skinBGPoint_004; }
        else if (idSkin == 5) { return skinBGPoint_005; }
        else if (idSkin == 6) { return skinBGPoint_006; }

        return skinBGPoint_001;
    }

    //****************************************************************************************************
    public void DefaultListSkins()
    {
        Skin item;

        skinsPoint.Clear();
        skinsBG.Clear();

        item = new Skin(); id = 1; item.countToGift = 0;    item.countToGiftNow = 0; item.isClose = false;  item.isBought = true;   skinsPoint.Add(item);
        item = new Skin(); id = 2; item.countToGift = 50;   item.countToGiftNow = 0; item.isClose = true;   item.isBought = true;   skinsPoint.Add(item);
        item = new Skin(); id = 3; item.countToGift = 0;    item.countToGiftNow = 0; item.isClose = true;   item.isBought = false;  skinsPoint.Add(item);
        item = new Skin(); id = 4; item.countToGift = 750;  item.countToGiftNow = 0; item.isClose = true;   item.isBought = true;   skinsPoint.Add(item);
        item = new Skin(); id = 5; item.countToGift = 1500; item.countToGiftNow = 0; item.isClose = true;   item.isBought = true;   skinsPoint.Add(item);
        item = new Skin(); id = 6; item.countToGift = 0;    item.countToGiftNow = 0; item.isClose = true;   item.isBought = false;  skinsPoint.Add(item);

        item = new Skin(); id = 1; item.countToGift = 0;    item.countToGiftNow = 0; item.isClose = false;  item.isBought = true;   skinsBG.Add(item);
        item = new Skin(); id = 2; item.countToGift = 450;  item.countToGiftNow = 0; item.isClose = true;   item.isBought = true;   skinsBG.Add(item);
        item = new Skin(); id = 3; item.countToGift = 0;    item.countToGiftNow = 0; item.isClose = true;   item.isBought = false;  skinsBG.Add(item);
        item = new Skin(); id = 4; item.countToGift = 1000; item.countToGiftNow = 0; item.isClose = true;   item.isBought = true;   skinsBG.Add(item);
        item = new Skin(); id = 5; item.countToGift = 2000; item.countToGiftNow = 0; item.isClose = true;   item.isBought = true;   skinsBG.Add(item);
        item = new Skin(); id = 6; item.countToGift = 0;    item.countToGiftNow = 0; item.isClose = true;   item.isBought = false;  skinsBG.Add(item);

        SaveSkins();
    }

    //****************************************************************************************************
}
