using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class ControlNote : MonoBehaviour
{
    public bool isStart = false;

    public Transform RealLineList;//显示判定线父对象
    public Transform WaitLineList;//预等判定线父对象

    public GameObject Tail;

    float tailHeight;
    public void ScriptStart()
    {
        SpriteRenderer spriteRenderer = Tail.GetComponent<SpriteRenderer>();
        tailHeight = spriteRenderer.sprite.bounds.size.y * transform.lossyScale.y;//hold尾所占y方向单位长度

        isStart = !isStart;
    }

    void Update()
    {
        if (!isStart) return;

        for (int i = 0; i < RealLineList.childCount; i++)
        {
            if (RealLineList.GetChild(i).childCount <= 0) continue;

            for (int m = 0; m < RealLineList.GetChild(i).childCount; m++)
            {
                Note note = chart.judgelineList[i].noteList[m];

                if (note.isPlay) continue;

                Transform noteTsf = RealLineList.GetChild(i).GetChild(m);

                int lineSide = note.lineSide;
                lineSide = (lineSide == 0) ? 1 : -1;

                float sttime = note.st;
                float edtime = note.et;
                float lgtime = note.longt;

                float resTime = sttime - realTime;

                Vector2 localPosition;
                Vector3 localScale;

                float ny = (resTime > 0) ? note.speed * ns * resTime * lineSide : 0;
                localPosition = new Vector2(0, ny);

                if (note.type == 2)
                {
                    if (resTime > 0)
                    {
                        noteTsf.GetChild(0).localScale = new Vector2(1, lgtime * ns * note.speed / tailHeight);
                    }
                    else
                    {
                        float tailTime = edtime - realTime;
                        noteTsf.GetChild(0).localScale = new Vector2(1, tailTime * ns * note.speed / tailHeight);
                    }  
                }

                noteTsf.localPosition = localPosition;
            }
        }
    }
}
