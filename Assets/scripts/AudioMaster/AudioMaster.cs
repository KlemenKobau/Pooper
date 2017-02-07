﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Audio; //NEOBVEZNO - Dodaj kdaj kasneje

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster audMast;

    public AudioClip[] audioClips = new AudioClip[9];
    public AudioClip[] sfxClips = new AudioClip[5];
    //public AudioMixer audioMixer; //NEOBVEZNO - Dodaj kdaj kasneje
    //public AudioMixerGroup musicGroup; //NEOBVEZNO - Dodaj kdaj kasneje

    private List<AudioSource> sourceList;
    private List<AudioSource> sfxSourceList;
    private int currentlyPlaying;
    private int currentBirdsfx;

    void Start()
    {
        if (audMast == null)
        {
            DontDestroyOnLoad(gameObject);
            audMast = this;
        }
        else if (audMast != this)
        {
            Destroy(gameObject);
        }

        //audioMixer.FindSnapshot("SlowSnap").TransitionTo(10f); //NEOBVEZNO -Dodaj kdaj kasneje

        sourceList = new List<AudioSource>();
        sfxSourceList = new List<AudioSource>();

        //SESTAVLJANJE sourceList
        for (int i = 0; i < 9; i++)
        {
            sourceList.Add(gameObject.AddComponent<AudioSource>() as AudioSource);
            sourceList[i].clip = audioClips[i];
            //sourceList [i].outputAudioMixerGroup = musicGroup; //NEOBVEZNO -Dodaj kdaj kasneje
        }
        for (int i = 0; i < 9; i++)
        {
            sourceList[i].loop = true;
            sourceList[i].Play();
            currentlyPlaying += 1;
            if (i > 0)
            {
                sourceList[i].mute = true;
                currentlyPlaying -= 1;
            }
        }

        //SESTAVLJANJE sfxSourceList
        for (int i = 0; i < 5; i++)
        {
            sfxSourceList.Add(gameObject.AddComponent<AudioSource>() as AudioSource);
            sfxSourceList[i].clip = sfxClips[i];
        }

        //gameObject.AddComponent<AudioDistortionFilter> (); //(IDEJA - filtri)
    }

    public void AddLayer()
    {
        currentlyPlaying += 1;
        for (int i = 0; i < currentlyPlaying; i++)
        {
            sourceList[i].mute = false;
        }
    }

    public void RemoveLayer()
    {
        currentlyPlaying -= 1;
        for (int i = currentlyPlaying; i < 9; i++)
        {
            sourceList[i].mute = true;
        }
    }

    public void Default()
    {
        currentlyPlaying = 2;
        for (int i = 0; i < currentlyPlaying; i++)
        {
            sourceList[i].mute = false;
        }
        for (int i = currentlyPlaying; i < 9; i++)
        {
            sourceList[i].mute = true;
        }
    }

    public void Custom(int layerAmount)
    {
        currentlyPlaying = layerAmount;
        for (int i = 0; i < currentlyPlaying; i++)
        {
            sourceList[i].mute = false;
        }
        for (int i = currentlyPlaying; i < 9; i++)
        {
            sourceList[i].mute = true;
        }
    }

    public void PlaySFX(int soundEffect)
    {
        sfxSourceList[soundEffect].Stop();
        sfxSourceList[soundEffect].Play();
    }
    public void PlayBirdSFX()
    {
        sfxSourceList[currentBirdsfx].Play();
        sfxSourceList[currentBirdsfx].Play();

        currentBirdsfx++;

        if (currentBirdsfx >= 3)
        {
            currentBirdsfx = 0;
        }
    }
}
