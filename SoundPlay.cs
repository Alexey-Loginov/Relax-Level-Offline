using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    //***
    [Header("Audio")]
    public AudioSource audioSource;

    //***
    public AudioClip music_01;
    public AudioClip music_02;
    public AudioClip music_03;
    public AudioClip music_04;
    public AudioClip music_05;
    public AudioClip music_06;
    public AudioClip music_07;
    public AudioClip music_08;
    public AudioClip music_09;

    //***
    [Header("Variables")]
    private int soundNum = 1;

    //***
    public bool isSound;

    //****************************************************************************************************
    void Awake()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();

        soundNum = Random.Range(1, 10);
        
        int intS = PlayerPrefs.GetInt("SoundOn");

        if (intS == 0)
        {
            isSound = true;
        }
        else
        {
            isSound = false;
            audioSource.Stop();
        }
    }

    //****************************************************************************************************
    void FixedUpdate()
    {
        if (!audioSource.isPlaying && isSound)
        {
            PlaySound();
        }
    }

    //****************************************************************************************************
    public void PlaySound()
    {
        if (isSound)
        {
            if (soundNum == 1)
            {
                music_01 = Resources.Load<AudioClip>("Music/112071__cheesepuff__piano-remix");
                audioSource.PlayOneShot(music_01);
            }
            else if (soundNum == 2)
            {
                music_02 = Resources.Load<AudioClip>("Music/132546__theworkingbamboo__etude");
                audioSource.PlayOneShot(music_02);
            }
            else if (soundNum == 3)
            {
                music_03 = Resources.Load<AudioClip>("Music/384697__satrianizado__under-all-mystery");
                audioSource.PlayOneShot(music_03);
            }
            else if (soundNum == 4)
            {
                music_04 = Resources.Load<AudioClip>("Music/391972__maxlandergard__den-blomstertid-nu-kommer");
                audioSource.PlayOneShot(music_04);
            }
            else if (soundNum == 5)
            {
                music_05 = Resources.Load<AudioClip>("Music/393520__frankum__ambient-guitar-x1-loop-mode");
                audioSource.PlayOneShot(music_05);
            }
            else if (soundNum == 6)
            {
                music_06 = Resources.Load<AudioClip>("Music/415186__psovod__sad-heaven-piano-3");
                audioSource.PlayOneShot(music_06);
            }
            else if (soundNum == 7)
            {
                music_07 = Resources.Load<AudioClip>("Music/417928__psovod__hope-music-sad-melody");
                audioSource.PlayOneShot(music_07);
            }
            else if (soundNum == 8)
            {
                music_08 = Resources.Load<AudioClip>("Music/557273__bigvegie__space-between-3");
                audioSource.PlayOneShot(music_08);
            }
            else if (soundNum == 9)
            {
                music_09 = Resources.Load<AudioClip>("Music/560894__bigvegie__timeflow");
                audioSource.PlayOneShot(music_09);
            }
            
            audioSource.volume = 1f;
        }
    }

    //****************************************************************************************************
}
