using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class InitializationLine : MonoBehaviour
{
    public bool isStart = false;

    public Transform RealLineList;//显示判定线父对象
    public Transform WaitLineList;//预等判定线父对象

    public GameObject RealLine;//显示判定线
    public GameObject WaitLine;//预等判定线

    //note预制体
    public GameObject Tap;
    public GameObject Hold;
    public GameObject Drag;

    public int[] noteIndex;

    public void ScriptStart()
    {
        noteIndex = new int[chart.judgelineList.Count];
        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            noteIndex[i] = 0;
        }

        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            var line = chart.judgelineList[i];

            GameObject realLine = Instantiate(RealLine, new Vector3(line.pos[0], line.pos[1], line.pos[2]), Quaternion.identity, RealLineList);//创建显示判定线
            GameObject waitLine = Instantiate(WaitLine, new Vector3(line.pos[0], line.pos[1], line.pos[2]), Quaternion.identity, WaitLineList);//创建预等判定线

            SpriteRenderer sprite = realLine.GetComponent<SpriteRenderer>();
            sprite.color =line.color;

            if (line.noteList.Count <= 0) continue;//判定判定线是否有note

            for (int m = 0; m < chart.judgelineList[i].noteList.Count; m++)
            {
                var note = chart.judgelineList[i].noteList[m];

                switch (note.type)
                {
                    case 0://tap
                        GameObject tap = Instantiate(Tap, new Vector3(0, 1000, 0), Quaternion.identity, waitLine.transform);
                        tap.GetComponent<SpriteRenderer>().color = note.color;
                        //tap.SetActive(false);
                        break;
                    case 1://drag
                        GameObject drag = Instantiate(Drag, new Vector3(0, 1000, 0), Quaternion.identity, waitLine.transform);
                        drag.GetComponent<SpriteRenderer>().color = note.color;
                        break;
                    case 2://hold
                        GameObject hold = Instantiate(Hold, new Vector3(0, 1000, 0), Quaternion.identity, waitLine.transform);
                        hold.GetComponent<SpriteRenderer>().color = note.color;
                        hold.transform.GetChild(0).GetComponent<SpriteRenderer>().color = note.color;
                        break;
                    default:
                        break;
                }
            }
        }
        isStart = !isStart;
    }

    Vector2 stLocalPosition;
    Vector2 edLocalPosition;
    void Update()
    {
        if (!isStart) return;

        for (int i = 0; i < chart.judgelineList.Count; i++)
        {
            List<Note> notelist = chart.judgelineList[i].noteList;

            if (chart.judgelineList[i].noteList.Count <= 0) continue;

            int m = noteIndex[i];

            Note note = notelist[m];

            int lineSide = note.lineSide;
            lineSide = (lineSide == 0) ? 1 : -1;

            float sttime = note.st;
            float lvtime = note.livingTime / 1000f;
            float nty = note.speed * ns * note.st * lineSide;

            //stLocalPosition = new Vector2(0, nty);

            //float scale = 2f;

            //bool stPosition = false;
            //bool edPosition = false;

            if ((realTime > sttime - lvtime) && !note.isAdd)
            {
                if (WaitLineList.GetChild(i).childCount == 0) continue;

                Transform noteTsf = WaitLineList.GetChild(i).GetChild(0);
                noteTsf.parent = RealLineList.GetChild(i);

                note.isAdd = true;

                if (m < chart.judgelineList[i].noteList.Count - 1) noteIndex[i]++;
            }
        }
    }
}
