using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class HideNote : MonoBehaviour
{
    public Transform realLineList;

    public bool isStart = false;

    public int[] noteIndex;

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

        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            int k = noteIndex[i];
            if (chart.judgelineList[i].noteList.Count <= 0) continue;
            var note = chart.judgelineList[i].noteList[k];
            if (realTime - note.et < 0 || note.isPlay) continue;
            note.isPlay = true;
            realLineList.GetChild(i).GetChild(k).gameObject.SetActive(false);
            if (k < chart.judgelineList[i].noteList.Count - 1) noteIndex[i]++;
        }
    }
}
