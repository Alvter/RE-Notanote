using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class HitAudioPlayer : MonoBehaviour
{
    public AudioSource noteAudio;

    public bool isStart = false;

    public int[] noteIndex;

    public float audioTime;
    public void ScriptStart()
    {
        noteIndex = new int[chart.judgelineList.Count];
        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            noteIndex[i] = 0;
        }

        isStart = !isStart;
    }

    void Update()
    {
        if (!isStart) return;

        audioTime = realTime + 0.05f;
        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            int k = noteIndex[i];
            if (chart.judgelineList[i].noteList.Count <= 0) continue;
            var note = chart.judgelineList[i].noteList[k];
            if (audioTime - note.st < 0 || note.isPlayAudio) continue;
            noteAudio.PlayOneShot(noteAudio.clip);
            note.isPlayAudio = true;
            if (k < chart.judgelineList[i].noteList.Count - 1) noteIndex[i]++;
        }
    }
}
