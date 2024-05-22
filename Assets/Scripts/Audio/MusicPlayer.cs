using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Updater;
using static LoadChart;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource music;

    float delayInSeconds = 0.9f;
    float time = 0f;
    float musicTime = 0f;

    public float offset;
    
    public static float musicLength = 1000f;
    bool isPlay = false;
    bool isStart = false;

    public void ScriptStart()
    {
        if (isStart) return;
        offset = chart.offset;
        musicLength = music.clip.length;
        isStart = true;
        if (delayInSeconds < 0) realTime += delayInSeconds / 1000f;
        if (offset < 0) realTime += offset / 1000f; ;
    }

    public void PauseMusic()
    {
        music.Pause();
        musicTime = music.time;
    }

    public void PlayMusic()
    {
        music.time = musicTime;
        music.Play();
    }

    public void changeMusicCondition()
    {
        if (music.isPlaying)
        {
            PauseMusic();
        }
        else
        {
            PlayMusic();
        }
    }

    void Update()
    {
        if (isPlay || !isStart) return;
        time += Time.deltaTime;
        if (time >= delayInSeconds + offset / 1000f)
        {
            music.Play();
            isPlay = true;
        }
    }
}
