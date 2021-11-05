using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    //***
    [Header("Objects")]
    public RectTransform loadingProgress;

    //***
    [Header("Variables")]
    private Quaternion quat;
    
    //***
    private float progressTime;
    private float timeToStart;

    //***
    private bool isStart = false;

    //****************************************************************************************************
    void Awake()
    {
        PlayerPrefs.SetInt("SetNewSkin", 0);
    }

    //****************************************************************************************************
    void FixedUpdate()
    {
        if (!isStart)
        {
            timeToStart += Time.deltaTime;

            if (timeToStart >= 0.5f)
            {
                isStart = true;
                AsyncOperation loading = SceneManager.LoadSceneAsync(1);
            }
        }

        loadingProgress.Rotate(0, 0, -100f * Time.deltaTime);
    }
    //****************************************************************************************************
}
